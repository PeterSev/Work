 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _7600
{
    public delegate void Received(Command com_in);
    public enum STATE_RX { ADDR, CNT, DATA};
    public enum CommandResult { Success, Timeout};
    public class Udp:iConnect
    {
        private Multimedia.Timer com_timer;
        protected Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        int portTo;
        //int portFrom; //порты. На который отправляем и с которого получаем.
        IPAddress addrTo;
        //IPAddress addrFrom;  //Ай-пи адреса на который шлем и с которого получаем
        private STATE_RX state_rx = STATE_RX.ADDR;
        private long time;
        private Command command_in;
        //byte[] buf;
        IPEndPoint endPointTo, endPointFrom; //конечные точки. На которую отправляем и с которой получаем
        bool active = false;
        Queue<byte> rx_queue = new Queue<byte>();

        public event Received received;

        //public Udp(IPAddress _addrTo, int _portTo, IPAddress _addrFrom, int _portFrom)
        public Udp(IPAddress _addrTo, int _portTo)
        {
            addrTo = _addrTo;
            //addrFrom = _addrFrom;
            portTo = _portTo;
            //portFrom = _portFrom;

            endPointTo = new IPEndPoint(addrTo, portTo);
            //endPointFrom = new IPEndPoint(addrFrom, portFrom);
            endPointFrom = new IPEndPoint(IPAddress.Any, portTo);
            try { udpClient.Bind(endPointFrom); }
            catch(SocketException ex)
            {
                throw ex;
            }
            com_timer = new Multimedia.Timer();
            com_timer.Stop();
            com_timer.Tick += com_timer_Tick;
            com_timer.Period = 1;
            com_timer.Resolution = 0;
        }

        public void SendCommand(Command command_out)
        {
            try
            {
                if (command_out != null) 
                {
                    if (udpClient.Available > 1) //вычитываем из порта все если там что-то есть
                    {
                        byte[] tmp = new byte[udpClient.Available];
                        udpClient.Receive(tmp);
                    }
                    
                    int n = udpClient.SendTo(command_out.GetCommandByteBuf(), endPointTo);

                    //command_in = command_out;
                    command_in = new Command(1000);
                    command_in.index = command_out.index;
                    command_in.set_or_get = command_out.set_or_get;
                    state_rx = STATE_RX.ADDR;
                    time = (long)command_in.timeOut;
                    com_timer.Start();

                }
            }
            catch (Exception ex) { throw ex; }
        }

        void com_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!active || !udpClient.Connected)
                {
                    com_timer.Stop();
                    rx_queue.Clear();
                    return;
                }

                if (udpClient.Available > 1)
                {
                    byte[] tmp = new byte[udpClient.Available];
                    udpClient.Receive(tmp);
                    foreach (byte b in tmp) rx_queue.Enqueue(b);
                }

                if (state_rx == STATE_RX.ADDR) //считываем адрес
                {
                    byte[] tmp = new byte[2];
                    if (rx_queue.Count >= tmp.Length)
                    {
                        for (int i = 0; i < tmp.Length; i++) tmp[i] = rx_queue.Dequeue();
                        command_in.address = (ushort)((tmp[1] << 8) + (tmp[0]));
                        state_rx = STATE_RX.CNT;
                    }
                }

                if (state_rx == STATE_RX.CNT) //считываем размер полученных данных
                {
                    byte[] tmp = new byte[2];
                    if (rx_queue.Count >= tmp.Length)
                    {
                        for (int i = 0; i < tmp.Length; i++) tmp[i] = rx_queue.Dequeue();
                        command_in.count = (ushort)((tmp[1] << 8) + (tmp[0]));
                        state_rx = STATE_RX.DATA;
                    }
                }

                if (state_rx == STATE_RX.DATA)  //считываем сами данные
                {
                    if (rx_queue.Count >= command_in.count) //принимаем данные согласно протоколу в размере command_in.count
                    {
                        command_in.SetDataNewSize(command_in.count);
                        for (int i = 0; i < command_in.count; i++) command_in.data[i] = rx_queue.Dequeue();
                        command_in.result = CommandResult.Success;

                        com_timer.Stop();
                        rx_queue.Clear();

                        //вычитываем в никуда данные из буфера если там есть что-то
                        if (udpClient.Available > 0)
                        {
                            byte[] tmp = new byte[udpClient.Available];
                            udpClient.Receive(tmp);
                        }
                        received(command_in);
                    }
                    else //рукожопный костыль, созданный для приема команды НЕ по протоколу в количестве 14 байт данных (спасибо Захаренко, создавшему эту команду)
                    {
                        command_in.SetDataNewSize((ushort)rx_queue.Count);
                        int tmpCnt = rx_queue.Count;
                        for (int i = 0; i < tmpCnt; i++) command_in.data[i] = rx_queue.Dequeue();
                            command_in.result = CommandResult.Success;

                            com_timer.Stop();
                            rx_queue.Clear();

                            //вычитываем в никуда данные из буфера если там есть что-то
                            if (udpClient.Available > 0)
                            {
                                byte[] tmp = new byte[udpClient.Available];
                                udpClient.Receive(tmp);
                            }
                            received(command_in);
                    }
                }

                if (time <= 0)
                {
                    command_in.result = CommandResult.Timeout;
                    com_timer.Stop();
                    rx_queue.Clear();
                    received(command_in);
                }
                time -= (uint)com_timer.Period;
                return;

            }
            catch (Exception ex) { throw ex; }
        }

        public bool Open()
        {
            active = true;
            udpClient.Connect(endPointTo);
            return true;
        }

        public bool Open(IPAddress _addr, int _port)
        {
            if (Close())
            {
                addrTo = _addr;
                portTo = _port;
                endPointTo = new IPEndPoint(addrTo, portTo);
                endPointFrom = new IPEndPoint(IPAddress.Any, portTo);
                try { udpClient.Bind(endPointFrom); }
                catch (SocketException ex){ throw ex; }
                return Open();
            }

            return false;
        }
        public bool Close()
        {
            DesetActive();
            while (com_timer.IsRunning) ;
            udpClient.Close();
            return true;
        }

        private void DesetActive()
        {
            active = false;
        }

        public void SetActive()
        {
            active = true;
        }
    }
}
