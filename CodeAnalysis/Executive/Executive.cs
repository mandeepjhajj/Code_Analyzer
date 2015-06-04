/////////////////////////////////////////////////////////////////////////
// Executive.cs  -  It act as main entry point. No processing is done  //
//                  in this package.                                   //
//                                                                     //
// ver 1.0                                                             //
// Language:    C#, Visual Studio 13.0, .Net Framework 4.5             //
// Platform:    HP Pavilion dv6 , Win 7, SP 1                          //
// Application: Code Analyzer                                          //
// Author:      Mandeep Singh, Syracuse University                     //
//              315-751-3413, mjhajj@syr.edu                           //
//                                                                     //
/////////////////////////////////////////////////////////////////////////
/*
 * Module Operations
 * =================
 * This module defines the following class:
 *  - Executive
 *   
 * Public Interface
 * ================
 * main(string[] args) - This is the entry point for the project.
 * 
 */
/*
 * Build Process
 * =============
 * Required Files:
 *   Executive.cs CmdP.cs FileH.cs Disp.cs Analyzer.cs
 *   
 * Build Command
 * =============
 * csc /target:exe Executive.cs CmdP.cs FileH.cs Analyzer.cs Disp.cs IRulesAndActions.cs RulesAndActions.cs Parsing.cs Semiexpresion.cs Tokenizer.cs PermanentRepository.cs ScopeStack.cs
 * 
 * Maintenance History
 * ===================
 * 
 * ver 1.0 : 06 September 2014
 * - First release
 * 
 * Planned Changes:
 * ----------------
 * 
 */
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCS
{
    class Executive
    {
                
        // Entry point for the project
        static void Main(string[] args)
        {

            List<string> patterns = new List<string>();
            List<string> options = new List<string>();
            string path = null;

            List<string> files = new List<string>();

            //Instantiate and passed the arguments to command line processer

            ParseCmdLine cmd = new ParseCmdLine();
            cmd.ParseLine(args,ref patterns, ref options, ref path);

            //Instantiate the Filemanager
            CFileMgr fm = new CFileMgr();
            fm.SearchFiles(path, patterns, options, ref files);

            //Check for the files returned
            if (files.Count != 0)
            {
                Analyzer anal = new Analyzer();
                anal.getFile(files, options);

                //After completing the analysis executive will call the Display package

                DisplayTables d = new DisplayTables();
                d.display();

                //To display output to XML file

                if ((options.Contains("/X") || options.Contains("/x")))
                {
                    d.displayXML();
                }
            }
        }
    }
}
