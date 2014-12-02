///////////////////////////////////////////////////////////////////////////////
///  DealWithArgs.cs - Dealwith Different Args in different situations      ///
///  ver 1.0                                                                ///
///                                                                         ///
///  Language:     Visual C#                                                ///
///  Platform:     ThinkPad T420, Windows 8                                 ///
///  Author:       Xincheng Lai,            Syracuse Univ.                  /// 
///////////////////////////////////////////////////////////////////////////////

/*
 *   Module Operations
 *   -----------------
 *   This module is used for geting commandline from excuting package, and then 
 *   transfer to different excution according to different inputs.
 *   
 *   Public Interface
 *   ---------------- 
 *   displayCommandLineArgs : showing the arguments
 *   meetRequirments : control the actual procedure according to different input arguments.
 *   
 *   Private:
 *   generateMetaData : use the MetadataTool to genreate Metadata
 *   run_TextSearch :  use TextSearch Package to do text search
 *   run_MetaDataQuery :  use Metadata Package to do extraction of metadata
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
using System.Threading.Tasks;

using System;
using System.IO;

namespace Project2
{
    public class CommandLineProc
    {
        static List<string> mOperations = new List<string>();

        public static void displayCommandLineArgs(string[] args)
        {
            Console.Write("\n  ");
            foreach (string arg in args)
                Console.Write(" {0}", arg);
            Console.Write("\n");
        }

        static void generateMetaData(string[] args)
        {
            // displayTitle("Demonstrate how to show you Meet Requirements");
            Console.Write("\n  command line arguments are: ");
            displayCommandLineArgs(args);
            Display.showInstructions(1);
            // use the args to call Metadata Tool 
        }

        public static void meetRequirments(List<string> args)
        {
            mOperations = args;
            bool found = false;
            foreach (string arg in args)
                if (arg.StartsWith("/N")){
                    found = true;
                    int reqNum = Int32.Parse(arg.Substring(2));
                    Console.Write("\n  Demonstrating requirement {0}", reqNum);
                    switch (reqNum){
                        case 3:{
                            Display.showTitle("Function: Text Query! Meet Requirment No2, No3, No5:");
                            Console.Write("\n    #3 Text Search\n  ");
                            run_TextSearch();
                            break;
                        }
                        case 4:{
                            Display.showTitle("Function: MetaData Query! Meet Requirment No2, No4, No5:");
                            Console.Write("\n    #4 MetaData Query\n  ");
                            run_MetaDataQuery();
                            break;
                        } 
                        case 6:{
                            Display.showTitle("Function: Generate Metadata File! Meet Requirment No6:");
                            Console.Write("\n    #6 Metadata Tool\n  ");
                            run_MetadataTool();
                            break;
                        }
                        default:{
                            Console.Write("\n    no such requirement");
                            break;
                        }
                    }
                }
            if (found == false)
                Console.Write("\n   Please specify a Tag /N to run the application, You can choose /N3, /N4 or /N6 with other correct arugments ");
            Console.Write("\n\n");
        }

        static void run_TextSearch() 
        {
            List<string> mSearch = new List<string>();
            bool flag_r = false;
            string operation = "/A";
            foreach (var s in mOperations)
            {
                if (s.StartsWith("/T"))
                    mSearch.Add(s.Substring(2));
                else if (s.StartsWith("/R"))
                    flag_r = true;
                else if (s.StartsWith("/A"))
                    operation = "/A";
                else if (s.StartsWith("/O"))
                    operation = "/O";
            }

            TextSearch m_textSearch = new TextSearch(mOperations[0], mOperations[1], flag_r, mSearch, operation);
            m_textSearch.searhText();
            List<string> mRes = m_textSearch.return_my_search_result();
            Console.Write("\n\n  The file contains elements: ");
            foreach (var item in mSearch)
                Console.Write(item + "  ");
            Console.Write("\n\n  Following are the list of those file: \n  ");
            foreach (var item in mRes)
            {
                Console.Write(item + "\n  ");
            }
        }

        static void run_MetaDataQuery(){
            List<string> mSearch = new List<string>();
            MetadataSearch m_Metadata = new MetadataSearch();
            bool flag_r = false;
            foreach (string s in mOperations)
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
            m_Metadata = new MetadataSearch(mOperations[0], mOperations[1], flag_r, mSearch);
            m_Metadata.searchMetaData(); 
        }

        static void run_MetadataTool()
        {
            string fullName = mOperations[0];
            MetadataTool m_test = new MetadataTool(fullName, mOperations);
            m_test.generateMetadata(); 
        }

        #if(TEST_DEALWITHARGS)
        static void Main(string[] args)
        {
            //  displayTitle("Demonstrate how to show you Meet Requirements");
            Console.Write("\n  command line arguments are: ");
            displayCommandLineArgs(args);
            bool found = false;
            foreach (string arg in args)
            {
                if (arg.StartsWith("/N"))
                {
                    found = true;
                    int reqNum = Int32.Parse(arg.Substring(2));
                    Console.Write("\n  Demonstrating requirement {0}", reqNum);
                    switch (reqNum)
                    {
                        case 2:
                            {
                                Console.Write("\n    #2's content is Yada, Yada, Yada, and more Yada");
                                // run_TextSearch();
                                break;
                            }
                        case 3:
                            {
                                Console.Write("\n    #3's content is Yada, Yada, Yada, and more Yada");
                                break;
                            } 
                        default:
                            {
                                Console.Write("\n    no such requirement");
                                break;
                            }
                    }
                }
            }
            if (found == false)
                Console.Write("\n   this content is based on the command line arguments");
            Console.Write("\n\n");
    } 
    #endif
    }
}
