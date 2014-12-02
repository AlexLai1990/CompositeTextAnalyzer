///////////////////////////////////////////////////////////////////////////////////////
///  MetadataSearch.cs - Use for extraction information from xml files              ///
///  ver 1.0                                                                        ///
///                                                                                 ///
///  Language:     Visual C#                                                        ///
///  Platform:     ThinkPad T420, Windows 8                                         ///
///  Author:       Xincheng Lai,            Syracuse Univ.                          /// 
///////////////////////////////////////////////////////////////////////////////////////

/*
 *   Module Operations
 *   -----------------
 *   This module is used for extract the information according to input arguments
 *   
 *   Public Interface
 *   ----------------  
 *   getAllMessageTags : divide input arguments"/M" to each searching elements
 *   searchMetaData : get the file from searching range and then do extraction all searching elements for each file in the searching range
 *  
 *   Maintenance History
 *   -------------------  
 *   ver 1.0 : 8 Oct 13
 *     - first release
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;

using System.IO;

namespace Project2
{
    public class MetadataSearch
    {
        string m_pattern = "*.xml";
        string m_path;
        List<string> m_searching_elements = new List<string>();
        bool m_Rflag = false;
        Navigate m_navigate;
        Navigate m_navigate_folder_set;
        List<string> m_searching_range = new List<string>();
        List<string> m_folder_set = new List<string>();


        public MetadataSearch() 
        {
        }
        
        public MetadataSearch(string pattern, string path, bool flag_recursive, List<string> searching_elements )
        { 
            pattern = m_pattern;
            m_path = path;
            m_searching_elements = searching_elements;
            m_Rflag = flag_recursive;
            m_navigate = new Navigate();
            m_navigate_folder_set = new Navigate();
            if (m_Rflag)
            {
                m_navigate.go_recursive(m_path, "*.xml");
                m_navigate_folder_set.go_recursive(m_path, "*.*");
            }
            else
            {
                m_navigate.go(m_path, "*.xml");
                m_navigate_folder_set.go(m_path, "*.*");
            }
            m_searching_range = m_navigate.return_my_range();

            Display.showTitle("The Searching range according to the input arguments is the following files:  ");
            Display.searchRange(m_searching_range);


            foreach (string file in m_navigate_folder_set.return_my_range()) 
            {
                if ( TextFile( Path.GetExtension(file) ) )
                    m_folder_set.Add( file );
            }
            Display.showTitle("\n  The Searching range of TextFiles according to input pattern arguments is following files : ");
            Display.searchRange(m_folder_set);
        }

        public static bool TextFile(string ext)
        {
            switch (ext)
            {
                case ".cs": return true;
                case ".csproj": return true;
                case ".config": return true;
                case ".txt": return true;
                case ".dat": return true;
                case ".doc": return true;
                case ".docx": return true;
                default: return false;
            }
        }

        public void getAllMessageTags( string input_string, ref List<string> searching_elements  ) 
        {
            // start to divide string to seperate tag strings
            input_string = input_string.Substring(2);
            int pos_comma = input_string.IndexOf(',');
            // if just one argument in the string 
            if (pos_comma == -1)
                searching_elements.Add(input_string);
            else {
                int pos_prev = 0;
                while ( pos_comma != -1 ){
                    searching_elements.Add(input_string.Substring(pos_prev, pos_comma));
                    input_string = input_string.Remove(0, pos_comma+1);
                    pos_comma = input_string.IndexOf(',');
                    pos_prev = pos_comma + 1;
                }
                searching_elements.Add(input_string);
            }
        }

        public bool 
            searchMetaData(){
            if (m_searching_range.Count < 1)
            {
                Console.Write("\n  There is no xml file in this file set\n  ");
                return false;
            }

            Display.showTitle("\n\n  Following are results of searching");

            foreach (string xml_file in m_searching_range) {
                // if the xml_file doesn't have a corresponding file , send an error message
                bool flag_exist = false;
                string xmlname = "";
                string filename = "";
                foreach (string file in m_folder_set)
                {
                    int pos_file = file.IndexOf('.');
                    int pos_xml = xml_file.IndexOf('.');
                    xmlname = Path.GetFileName(xml_file).Substring(0, Path.GetFileName(xml_file).IndexOf('.'));
                    filename = Path.GetFileName(file).Substring(0, Path.GetFileName(file).IndexOf('.'));
                    if (xmlname == filename && ( pos_file == pos_xml) )
                    {
                        flag_exist = true;
                        break;
                    }
                }
                if (!flag_exist){
                    Display.showError("Error Message!!:\n  XML file: " + xml_file + "\n  DOESN'T have correspoding text file");
                    continue;
                }
                // do the core reading function for XML files 
                coreSearchMetaData(xml_file, m_searching_elements);
            }
            return true;
        }

        public bool coreSearchMetaData(string xmlfile, List<string> tags ) {
            Display.searchedFile(xmlfile); 
            XDocument doc = XDocument.Load(@xmlfile);
            var q = from x in
                        doc.Elements("File")
                        .Elements()
                    select x;
            Console.Write("  {0,-12} {1}\n", "Tag", "Value");
            foreach (string tag in tags) {
                foreach ( var elem in q ){
                    if ( elem.Name == tag )
                        Console.Write("  {0,-12} {1}\n", elem.Name, elem.Value);
                }                
            }
            return true;
        }


        #if(TEST_METADATASEARCH)
        [STAThread]
        // FORMAT *.txt ../../TestFiles /Mname1,name2 /R 
        static void Main(string[] args)
        {
            List<string> mOperations = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                mOperations.Add(args[i]);
            }

            Display.showInputArguments(args);

            List<string> mSearch = new List<string>();
            MetadataSearch m_Metadata = new MetadataSearch();
            bool flag_r = false; 
            foreach ( string s in mOperations)
            {
                if (s.StartsWith("/M"))
                {
                    m_Metadata.getAllMessageTags(s, ref mSearch);
                }
                else if (s.StartsWith("/R"))
                    flag_r = true; 
            }

            // 1. Check the range of search 
          
            // in Initialization, we get the searching range.
            m_Metadata = new MetadataSearch(args[0], args[1], flag_r, mSearch);
            m_Metadata.searchMetaData(); 

        } 

        #endif
    }
}
