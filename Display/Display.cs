///////////////////////////////////////////////////////////////////////////////////////
///  Display.cs - Display package for show the instructions and result on console   ///
///  ver 1.0                                                                        ///
///                                                                                 ///
///  Language:     Visual C#                                                        ///
///  Platform:     ThinkPad T420, Windows 8                                         ///
///  Author:       Xincheng Lai,            Syracuse Univ.                          /// 
///////////////////////////////////////////////////////////////////////////////////////

/*
 *   Module Operations
 *   -----------------
 *   This module is used for showing the instructions and results on the console,
 *   it is convenient for using CTA and geting the idea about the result.
 *   
 *   Public Interface
 *   ----------------  
 *   showInputArguments ： Showing the input arguments on the console
 *   showInstructions : Showing the instruction according to inputs
 *   showTitle : Show the string as Title
 *   searchedFile : Show the result of searching files range
 *   searchRange : Show the searching range
 *   show : Show ipnuts on the console
 *  
 *   Maintenance History
 *   -------------------  
 *   ver 1.0 : 8 Oct 13
 *     - first release
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Display
    { 
        public static void showInputArguments(string[] arguments)
        {
            showTitle("The arguments from input are following");

            string temp = ""; 
            foreach (string m_arg in arguments)
            {
                Console.Write("  {0,-1}", m_arg);
                temp += m_arg.ToString();
            }

            StringBuilder upper = new StringBuilder();
            for (int i = 0; i < temp.Length + 2; ++i)
                upper.Append('-');
            Console.Write("\n {0}\n", upper.ToString()); 
        }

        public static void showInstructions(int index)
        {
            switch (index)
            {
                // No1 Instruction is used for generating Metadata file 
                case 3:
                    showTitle("Function: Text Query! Meet Requirment No2, No3, No5:");
                    break;
                case 4:
                    showTitle("Function: Text Query! Meet Requirment No2, No4, No5:");
                    break;
                case 5:
                    showTitle("Function: Generate Metadata File! Meet Requirment No6:");
                    break; 
                default:{
                    Console.Write("\n    no such requirement");
                    break;
                }
            }
        }

        public static void showTitle(string title)
        {
            Console.Write("\n  {0}", title);
            StringBuilder under = new StringBuilder();
            for (int i = 0; i < title.Length + 2; ++i)
                under.Append('=');
            Console.Write("\n {0}\n", under.ToString());
        }

        public static void showError(string message)
        {
            StringBuilder upper = new StringBuilder();
            for (int i = 0; i < 18; ++i)
                upper.Append('*');
            Console.Write("\n {0}", upper.ToString());

            Console.Write("\n  {0}", message);

            StringBuilder under = new StringBuilder();
            for (int i = 0; i < 18; ++i)
                under.Append('*');
            Console.Write("\n {0}\n", under.ToString());
        }

        // show the searched result files
        public static void searchedFile(string title)
        { 
            StringBuilder upper = new StringBuilder();
            for (int i = 0; i < title.Length + 2; ++i)
                upper.Append('-');
            Console.Write("\n {0}\n", upper.ToString());

            Console.Write("  {0}", title);

            StringBuilder under = new StringBuilder();
            for (int i = 0; i < title.Length + 2; ++i)
                under.Append('-');
            Console.Write("\n {0}\n", under.ToString());
        }

        public static void searchRange( List<string> search_list)
        {
            foreach (string file in search_list)
            {
                Navigate.printoutFile(file);
            }
        }


        #if(TEST_DISPLAY)
                [STAThread]
                static void Main(string[] args)
                {
                    Display.showTitle("This package is just used for display content on the console");
                    return;
                }
        #endif
    }  
}
