/////////////////////////////////////////////////////////////////////////
// Analyzer.cs  -   Build the parser for type and relationship analysis//
//                                                                     //
// ver 1.0                                                             //
// Language:    C#, Visual Studio 13.0, .Net Framework 4.5             //
// Platform:    HP Pavilion dv6 , Win 7, SP 1                          //
// Application: Code Analyzer                                          //
// Author:      Mandeep Singh, Syracuse University                     //
//              315-751-3413, mjhajj@syr.edu                           //
//                                                                     //
// Source:      Jim Fawcett, CST 2-187, Syracuse University            //
//              (315) 443-3948, jfawcett@twcny.rr.com                  //
/////////////////////////////////////////////////////////////////////////
/*
 * Module Operations
 * =================
 * This module defines the following classes 
 *   Analyzer
 *   TestAnalyzer
 *   
 * Public Interface
 * ================
 * getFile(List<string> files, List<string> option)    // Calls the analysis and relation function for each file
 * doAnalysis(string file)     //Builds the parser for type analysis
 * FindRelation(string file)   //Builds the parser for relationship analysis
 * 
 */
/*
 * Build Process
 * =============
 * Required Files:
 *   IRulesAndActions.cs, RulesAndActions.cs, Parsing.cs, Semiexpresion.cs, Tokenizer.cs
 * 
 * Compiler Command:
 *   csc /target:library /define:TEST_ANALYZER IRulesAndActions.cs RulesAndActions.cs Parsing.cs Semiexpresion.cs Tokenizer.cs
 * 
 * Maintenance History
 * ===================
 * 
 * ver 1.0 : 06 September 2014
 * - First release
 * 
 * Planned Changes:
 * ----------------
 * - Build new rules for special cases for type relationships
 * - Hnalding the special cases with performing action on the local semi generated
 */
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParserCS
{
    public class Analyzer
    {

        RepositoryPermanemt pRepo = new RepositoryPermanemt();
        List<string> check = new List<string>();

        /*<-------It will call analysis/relation function for each file--------->*/
        public void getFile(List<string> files, List<string> option)
        {
            foreach (string file in files)
            {
                doAnalysis(file);
            }
            check = option;
            if (check.Contains("/R") || check.Contains("/r"))
            {
                foreach (string file in files)
                {
                    FindRelation(file);
                }
            }
        }

        /*<------This function will build the parser with set of rules defined in rules and action package------>*/
        public void doAnalysis(string file)
        {
            
            SemiEx.CSemiExp semi = new SemiEx.CSemiExp();
            semi.displayNewLines = false;

            if (!semi.open(file as string))
            {
                Console.Write("\n  Can't open {0}\n\n", file);
                return;
            }
            
            BuildCodeAnalyzer builder = new BuildCodeAnalyzer(semi);
            Parser parser = builder.build();

            try
            {
                while (semi.getSemi())
                    parser.parse(semi, file);
            }
            catch (Exception ex)
            {
                Console.Write("\n\n  {0}\n", ex.Message);
            }

            /*<----Copying all elements per file to the some permanent table----->*/
            Repository repo = Repository.getInstance();
            List<Elem> El = repo.stack.getList();
            pRepo.copyElements(Repository.getInstance(), file, El);

            semi.close();
        }

        /*<--------This will build the parser for finding relationships-------->*/
        public void FindRelation(string file)
        {
            SemiEx.CSemiExp semi = new SemiEx.CSemiExp();
            semi.displayNewLines = false;

            if (!semi.open(file as string))
            {
                Console.Write("\n  Can't open {0}\n\n", file);
                return;
            }
            
            RelationshipAnalyzer builder = new RelationshipAnalyzer(semi);
            Parser parser = builder.build();

            try
            {
                while (semi.getSemi())
                    parser.relations(semi, file);
            }
            catch (Exception ex)
            {
                Console.Write("\n\n  {0}\n", ex.Message);
            }
            semi.close();
        }
    }
    public class TestAnalyzer
    {
    //----< process commandline to get file references >-----------------

      static List<string> ProcessCommandline(string[] args)
      {
        List<string> files = new List<string>();
        if (args.Length == 0)
        {
            Console.Write("\n  Please enter file(s) to analyze\n\n");
            return files;
        }
        string path = args[0];
        path = Path.GetFullPath(path);
        for (int i = 1; i < args.Length; ++i)
        {
            string filename = Path.GetFileName(args[i]);
            files.AddRange(Directory.GetFiles(path, filename));
        }
        return files;
     }

    static void ShowCommandLine(string[] args)
    {
      Console.Write("\n  Commandline args are:\n");
      foreach (string arg in args)
      {
        Console.Write("  {0}", arg);
      }
      Console.Write("\n\n  current directory: {0}", System.IO.Directory.GetCurrentDirectory());
      Console.Write("\n\n");
    }

/*<-----------------Test Stub---------------->*/

#if(TEST_ANALYZER)
     static void Main(string[] args)
    {
        Console.Write("\n  Demonstrating Analyzer");
        Console.Write("\n ======================\n");

        ShowCommandLine(args);

        List<string> files = TestAnalyzer.ProcessCommandline(args);
        List<string> opt = new List<string>();

        opt.Add("/R");
        opt.Add("/S");

        Analyzer anal = new Analyzer();
        anal.getFile(files, opt);

        RepositoryPermanemt pr = new RepositoryPermanemt();
        List<Elements> fp1 = pr.returnPermanentRep();

        List<Relation> sp2 = storeRel.returnRelation();

        Console.Write("\n\n Type Analysis of file\n");
        Console.Write("\n ----------------------------\n\n");
        foreach (Elements l in fp1)
        {
            Console.Write("\n{0,10} {1,10} {2,10} {3,10} {4,10}", l.namesp, l.type, l.name, l.begin, l.end);
        }
        
        Console.Write("\n\n Relationship Analysis of file\n");
        Console.Write("\n ----------------------------\n\n");
        foreach (Relation l in sp2)
        {
            Console.Write("\n{0,5} {1,5} {2,5} {3,10} {4,10} {5,10}", l.namespace1, l.type1, l.name, l.namespace2, l.type2, l.file);
        }
        Console.Write("\n\n  That's all folks!\n\n");
      }
#endif
    }
}
