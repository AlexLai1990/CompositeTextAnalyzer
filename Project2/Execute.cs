///////////////////////////////////////////////////////////////////////
///  Execute.cs -  This is the package to run the main operation    ///
///  ver 1.0                                                        ///
///                                                                 ///
///  Language:     Visual C#                                        ///
///  Platform:     ThinkPad T420, Windows 8                         ///
///  Author:       Xincheng Lai,            Syracuse Univ.          /// 
///////////////////////////////////////////////////////////////////////

/*
 *  Module Operations:
 *  ==================
 *  This package is used for executing the whole project 
 *  
 *  Dependency : Display.cs CommandLineProc.cs
 * 
 *  Maintenance History:
 *  ==================== 
 *  ver 1.0 : 8 Oct 13
 *  - first release
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.IO;

namespace Project2
{
    class Execute
    {
        static void Main(string[] args)
        {
            bool flag = true;
            Display.showTitle("Demonstrate – Composite Text Analyzer(CTA) Requirements");
            List<string> mOperations = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                mOperations.Add(args[i]);
            }
            Display.showInputArguments(args);

            CommandLineProc.meetRequirments(mOperations);

            Console.Write("\n\n");

            while (flag) 
            {
                Console.Write("\n  Please 'c' enter to continue.\n");
                if ( Console.ReadLine() == "c")
                    flag = false;
            }

        }
    }
}
