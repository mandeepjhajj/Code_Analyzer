/////////////////////////////////////////////////////////////////////////
// Disp.cs -    Used to dsiplay the output to the screen or XML file   //
//              based on the input option                              //
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
 *   DisplayTables
 *   TestDisplay
 *   
 * Public Interface
 * ================
 * display() - Used to dispaly output on screen, retrives data stored in repository.
 * displayXML() - Used to redirect output to XML file.
 * display()  - Used for test stub.
 * 
 */
/*
 * Build Process
 * =============
 * Required Files:
 *   Disp.cs
 *   
 * Build command:
 *   csc /target:library /D:TEST_DISPLAY Disp.cs
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
 * 
 */
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParserCS
{
    public class DisplayTables
    {

        RepositoryPermanemt pr = new RepositoryPermanemt();
       
        List<Relation> sp2 = storeRel.returnRelation();
        List<string> files = new List<string>();

        /*<------Below function will display the output to the screen. It will fetch the data from repository---->*/
        public void display()
        {
                List<Elements> fp1 = pr.returnPermanentRep();
                int compare=0 ;
                string a, b, c, d, f, g, h, i, j, k, m, n, functionnm = null, namesp = null ;
                a= "Namespace";
                b= "Type";
                c= "TypeName";
                d= "Begin";
                f= "end";
                g = "NameSpace1";
                h = "Type1";
                i = "Relation";
                j = "NameSpace2";
                k = "Type2";
                m = "LOC";
                n = "Complexity";

                foreach(Elements e in fp1)
                {
                    if(files.Count == 0)
                    {
                        files.Add(e.file);
                    }
                    else if(!files.Contains(e.file))
                    {
                        files.Add(e.file);
                    }
                }
                foreach (string s in files)
                {
                    Console.Write("\n\n Type Analysis of file: {0}\n", s);
                    Console.Write("\n ----------------------------\n\n");
                    Console.Write("\n{0,10} {1,10} {2,20} {3,10} {4,10} {5,5} {6,3}",a,b,c,d,f,m,n);
                    foreach(Elements l in fp1)
                    {
                        if(s.Equals(l.file))
                        {
                            int LOC = l.end - l.begin;
                            Console.Write("\n{0,10} {1,10} {2,20} {3,10} {4,10} {5,5} {6,3}", l.namesp, l.type, l.name, l.begin, l.end,LOC,l.complex);
                            int temp = compare;
                            compare = Math.Max(compare, l.complex);
                            if(temp != compare)
                            {
                                functionnm=l.name;
                                namesp = l.namesp;
                            }
                        }                    
                    }
                 }

                foreach (string s in files)
                {
                    Console.Write("\n\n Relationship Analysis of file: {0}\n",s);
                    Console.Write("\n ----------------------------\n\n");
                    Console.Write("\n{0,10} {1,20} {2,10} {3,15} {4,15}\n", g, h, i, j, k);
                    foreach (Relation lm in sp2)
                    {
                        if (s.Equals(lm.file))
                        {
                            Console.Write("\n{0,10} {1,20} {2,10} {3,15} {4,15}", lm.namespace1, lm.type1, lm.name, lm.namespace2, lm.type2);
                        }
                    }
                    Console.Write("\n\n");
                 }
                Console.Write("\nSummary of files\n");
                Console.Write("\n ----------------------------\n");
                Console.Write("\nTotal number of files processed = {0}",files.Count);
                Console.Write("\nFunction with highest complexity is {0} present in {1} namespace", functionnm, namesp);
                Console.Write("\nAbove function has complexity of {0}", compare);
        }  

        /*<------- Below function will generate the XML file and will store the information in it-------->*/
        public void displayXML()
        {
            List<Elements> fp1 = pr.returnPermanentRep();
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(xmlDeclaration);
                XmlNode node = doc.CreateElement("CodeAnalyzer");
                doc.AppendChild(node);
                XmlNode element1 = doc.CreateElement("TypeAnalysis");
                node.AppendChild(element1);
                XmlNode element2 = doc.CreateElement("RelationshipAnalysis");
                node.AppendChild(element2);
                foreach (string file in files)
                {
                    XmlNode n1 = doc.CreateElement("FILE");
                    element1.AppendChild(n1);

                    XmlAttribute File = doc.CreateAttribute("Name");
                    File.Value = file;
                    n1.Attributes.Append(File);
                    element1.AppendChild(n1);
                    foreach (Elements e in fp1)
                    {
                        if (file.Equals(e.file))
                        {
                            XmlNode n = doc.CreateElement("ANALYSIS");
                            n1.AppendChild(n);

                            XmlAttribute attribute1 = doc.CreateAttribute("NameSpace");
                            attribute1.Value = e.namesp;
                            n.Attributes.Append(attribute1);
                            n1.AppendChild(n);
                            XmlAttribute attribute2 = doc.CreateAttribute("Type");
                            attribute2.Value = e.type;
                            n.Attributes.Append(attribute2);
                            n1.AppendChild(n);
                            XmlAttribute attribute3 = doc.CreateAttribute("TypeName");
                            attribute3.Value = e.name;
                            n.Attributes.Append(attribute3);
                            n1.AppendChild(n);
                            XmlAttribute attribute4 = doc.CreateAttribute("Begin");
                            attribute4.Value = e.begin.ToString();
                            n.Attributes.Append(attribute4);
                            n1.AppendChild(n);
                            XmlAttribute attribute5 = doc.CreateAttribute("End");
                            attribute5.Value = e.end.ToString();
                            n.Attributes.Append(attribute5);
                            n1.AppendChild(n);
                            XmlAttribute attribute6 = doc.CreateAttribute("LOC");
                            attribute6.Value = (e.end - e.begin).ToString();
                            n.Attributes.Append(attribute6);
                            n1.AppendChild(n);
                            XmlAttribute attribute7 = doc.CreateAttribute("Complexity");
                            attribute7.Value = e.complex.ToString();
                            n.Attributes.Append(attribute7);
                            n1.AppendChild(n);
                        }
                    }
    
                }
                foreach (string file in files)
                {
                    XmlNode n2 = doc.CreateElement("FILE");
                    element1.AppendChild(n2);

                    XmlAttribute FileR = doc.CreateAttribute("Name");
                    FileR.Value = file;
                    n2.Attributes.Append(FileR);
                    element2.AppendChild(n2);
                    foreach(Relation e in sp2)
                    {
                        if (file.Equals(e.file))
                        {
                            XmlNode n = doc.CreateElement("RELATIONS");
                            n2.AppendChild(n);

                            XmlAttribute attribute1 = doc.CreateAttribute("NameSpace1");
                            attribute1.Value = e.namespace1;
                            n.Attributes.Append(attribute1);
                            n2.AppendChild(n);
                            XmlAttribute attribute2 = doc.CreateAttribute("Type1");
                            attribute2.Value = e.type1;
                            n.Attributes.Append(attribute2);
                            n2.AppendChild(n);
                            XmlAttribute attribute7 = doc.CreateAttribute("Relation");
                            attribute7.Value = e.name;
                            n.Attributes.Append(attribute7);
                            n2.AppendChild(n);
                            XmlAttribute attribute3 = doc.CreateAttribute("NameSpace2");
                            attribute3.Value = e.namespace2;
                            n.Attributes.Append(attribute3);
                            n2.AppendChild(n);
                            XmlAttribute attribute4 = doc.CreateAttribute("Type2");
                            attribute4.Value = e.type2;
                            n.Attributes.Append(attribute4);
                            n2.AppendChild(n);
                        }
                    }
                }
                doc.Save("CodeAnalysis.xml");
                Console.Write("\n\n Output is saved in current directory with file name CodeAnalysis.xml\n\n");
            }
            catch(Exception e)
            {
                Console.Write("\n\nExpected a XML exception {0}", e);
            }
        }

    }
    /*<-----------Test class for test stub------->*/
    class TestDisplay
    {

        public void display()
        {
            Elements e = new Elements();
            Relation r = new Relation();

            List<Elements> perm = new List<Elements>();
            List<Relation> rln = new List<Relation>();

            e.name = "A";
            e.namesp = "Namespace";
            e.type = "Class";
            e.begin = 10;
            e.end = 50;

            perm.Add(e);

            e.name = "B";
            e.namesp = "CodeAnalysis";
            e.type = "function";
            e.begin = 150;
            e.end = 200;

            perm.Add(e);

            e.name = "C";
            e.namesp = "ParserCS";
            e.type = "struct";
            e.begin = 60;
            e.end = 80;

            perm.Add(e);

            r.namespace1 = "Namespace1";
            r.namespace2 = "Namespace2";
            r.name = "Aggregate";
            r.type1 = "ClassA";
            r.type2 = "ClassB";

            rln.Add(r);

            r.namespace1 = "parser";
            r.namespace2 = "analyzer";
            r.name = "Composes";
            r.type1 = "A";
            r.type2 = "B";

            rln.Add(r);

            r.namespace1 = "Semi";
            r.namespace2 = "Toker";
            r.name = "Inherit";
            r.type1 = "Semiexpression";
            r.type2 = "Tokenizer";

            rln.Add(r);

            foreach (Elements el in perm)
            {
                Console.Write("\n{0,10} {1,10} {2,10} {3,10} {4,10} {5,5}", el.namesp, el.type, el.name, el.begin, el.end);
            }
            foreach (Relation r1 in rln)
            {
                Console.Write("\n{0,10} {1,10} {2,10} {3,10} {4,10}", r1.namespace1, r1.type1, r1.name, r1.namespace2, r1.type2);
            }
      
        }

        /*<---------------Test Stub---------------------->*/
#if(TEST_DISPLAY)
        static void Main(string[] args)
        {
            Console.Write("\n  Demonstrating Display");
            Console.Write("\n ======================\n");

            TestDisplay d = new TestDisplay();
            d.display();
         }
#endif
    }

}
  

