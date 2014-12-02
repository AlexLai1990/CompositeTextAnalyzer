using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
 
using System.IO;

namespace Project2
{
    public class MetadataTool
    {
        string m_path, m_name;
        //string[]  m_args;
        List<string> m_operations;
        string m_xmlPath = "";

        public MetadataTool(string path, List<string> operations)
        {
            // seperate the name from the path
            m_path = path;
            int pos_end;
            int pos = path.LastIndexOf('\\');
            if( pos == -1)
                pos = path.LastIndexOf('/');
            if( pos > -1) {
                ++pos; 
                m_name = path.Remove(0,pos);
                pos_end = m_name.LastIndexOf('.');
                m_name = m_name.Substring( 0, pos_end );
            }  

            m_operations = operations;
        }

        public bool generateMetadata( )
        {
            // check the file existed or not

            if ( !File.Exists(m_path) ) 
            {
                Console.WriteLine(" The Input file doesn't exists\n We don't create Metadata!\n ");
                return false;
            }  
            // If the File is existed we create a related Xml file for it
            // m_xmlPath = m_path.Split('.')[0] + m_name + ".xml"; 
            m_xmlPath = m_path.Substring(0, m_path.LastIndexOf("/") +1 )+ m_name + ".xml"; 
            // If the xml file already existed, we update it with exact argument
            if ( File.Exists(m_xmlPath) )
            {
                Console.WriteLine(" The Metadata is already existed!  Updating now ...\n ");
                updateXMLFile();
            }
            // If the xml file doesn't exist, we create a new one
            else
            {
                Console.WriteLine(" Creating a new metadata ...\n ");
                createNewXMLFile();
            }
            return true;
        }

        public void updateXMLFile() 
        {
            // load the xml file first
            XDocument doc = XDocument.Load(@m_xmlPath);
            XElement newElem;
            XElement add_pos = doc.Descendants("File").First();
            foreach (string item in m_operations )
            { 
                if (item.StartsWith("/K")) {
                    if ( doc.Descendants("KeyWords").Any() != false ){
                        add_pos.Element("KeyWords").Remove();
                    }
                    newElem = new XElement("KeyWords");
                    newElem.Value = item.Substring(2);
                    add_pos.Add(newElem);
                } else if (item.StartsWith("/T")) {
                    if (doc.Descendants("Description").Any() != false)
                    {
                        add_pos.Element("Description").Remove();
                    }
                    newElem = new XElement("Description");
                    newElem.Value = item.Substring(2);
                    add_pos.Add(newElem);
                } else if (item.StartsWith("/D")) {
                    if (doc.Descendants("Dependency").Any() != false)
                    {
                        add_pos.Element("Dependency").Remove();
                    }
                    newElem = new XElement("Dependency");
                    newElem.Value = item.Substring(2);
                    add_pos.Add(newElem);
                } 
            }
            doc.Save(m_xmlPath);
            //Console.Write("\n\n  ");
            Console.WriteLine(" "+ m_xmlPath + "  Updated!!\n "); 
        }

        public void createNewXMLFile()
        {
            bool flagKInSameTag = false;
            bool flagDInSameTag = false;
            bool flagCInSameTag = false;
            XDocument xml = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XComment(" This is XML file for "+ m_name ),
               new XElement("File")
            );
            XElement add_pos = xml.Descendants("File").First();
            foreach (string item in m_operations ) 
            {
                XElement newElem;
                if ( item.StartsWith("/K") )
                {
                    if (flagKInSameTag == false) 
                    {
                        XElement child2 = new XElement("AnotherChild",
                             new XElement("GrandChild", "different content") );
                        add_pos.Add(child2);
                        flagKInSameTag = true;
                    } 

                    newElem = new XElement("KeyWords");
                    newElem.Value = item.Substring(2);
                    add_pos.Add(newElem);
                }
                else if (item.StartsWith("/T"))
                {
                    newElem = new XElement("Description");
                    newElem.Value = item.Substring(2);
                    add_pos.Add(newElem);
                } 
                else if (item.StartsWith("/D"))
                {
                    newElem = new XElement("Dependency");
                    newElem.Value = item.Substring(2);
                    add_pos.Add(newElem);
                }
            }
            xml.Save(m_xmlPath);
            Console.Write("\n\n ");
            Console.WriteLine(m_xmlPath + "  Created!!\n "); 
            Console.Write("\n\n");
        }


        #if(TEST_METADATA)
        [STAThread]
        static void Main(string[] args)
        {
            List<string> mOperations = new List<string>();
            for ( int i = 1; i < args.Length; i++ )
            {
                mOperations.Add( args[i] );
            }

            Display.showInputArguments(args);

            string fullName = args[0];
            MetadataTool m_test = new MetadataTool(fullName , mOperations);
            m_test.generateMetadata();
        }

        #endif 
    } 
}
