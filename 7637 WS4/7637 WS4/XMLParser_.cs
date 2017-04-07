using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _7637_WS4
{
    public static class XMLParser
    {
        public static List<Board> OpenListBoards(string filename)
        {
            List<Board> listBoards = new List<Board>();
           
            XmlTextReader reader = new XmlTextReader(filename);
            string paramName = string.Empty;
            string catalog = string.Empty;
            string boardName = null;
            string imageLinkString = string.Empty;
            string commentString = string.Empty;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                        paramName = reader.Name;
                        if (reader.Name == "BOARD")
                        {

                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                boardName = reader.Value;
                            }
                        }

                        break;

                    case XmlNodeType.Text: // Вывести текст в каждом элементе.

                        if (paramName == "CATALOG")
                        {
                            catalog = reader.Value;
                        }


                        if (paramName == "COMMENT")
                        {
                            commentString = reader.Value;
                        }

                        if (paramName == "IMAGELINK")
                        {
                            imageLinkString = reader.Value;
                       }


                       break;

                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        if (reader.Name == "BOARD")
                        {
                            Board board = new Board(boardName, catalog, imageLinkString, commentString);
                            listBoards.Add(board);
                            commentString = string.Empty;
                            imageLinkString = string.Empty;
                            catalog = string.Empty;
                        }

                        break;
                }
            }


            return listBoards;
        }


        public static List<TestInfo> OpenListTests(string filename)
        {
            List<TestInfo> listTests = new List<TestInfo>();

            XmlTextReader reader = new XmlTextReader(filename);
            string paramName = string.Empty;
            string catalog = string.Empty;
            string testName = null;
            string imageLinkString = string.Empty;
            string commentString = string.Empty;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                        paramName = reader.Name;
                        if (reader.Name == "TEST")
                        {

                            reader.MoveToNextAttribute();
                            if (reader.Name == "NAME")
                            {
                                testName = reader.Value;
                            }
                        }

                        break;

                    case XmlNodeType.Text: // Вывести текст в каждом элементе.


                        if (paramName == "COMMENT")
                        {
                            commentString = reader.Value;
                        }

                        if (paramName == "IMAGELINK")
                        {
                            imageLinkString = reader.Value;
                        }


                        break;

                    case XmlNodeType.EndElement: // Вывести конец элемента.
                        if (reader.Name == "TEST")
                        {
                            TestInfo board = new TestInfo(testName, imageLinkString, commentString);
                            listTests.Add(board);
                            commentString = string.Empty;
                            imageLinkString = string.Empty;
                            catalog = string.Empty;
                        }

                        break;
                }
            }


            return listTests;
        }


        public static List<Help> OpenListBZHelp(string filename)
        {
            

            return new List<Help>();
        }
    }


}
