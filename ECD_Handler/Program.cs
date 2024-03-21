// Program to select and process Daily Statements Accounts (ECD in spanish)
//  Bravos tecnichal test for web developers applicants
//  Follow the instructions from README file

// using ECD_Handler.Handler;


using ECD_Handler.Handler;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;

namespace ECD_Handler
{
    class Program
    {
        // to print several strings at once, with a defined separator and ending char/string
        static void print_msgs(string[]? txt_msgs= null, string sep= "\n", string end= "\n")
        {
            if(txt_msgs is null)
            {
                return;
            }
            
            foreach( string txt in txt_msgs.Take(txt_msgs.Length-1) )
            {
                Console.Write(txt+sep);
            }
            Console.Write(txt_msgs[txt_msgs.Length-1]+end);
            
            return;
        }
        
        // to get current user dir (home dir)
        static string get_home_dir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        
        //         
        static decimal process_files_dir(string file) 
        {
            // 2. Processing: either by adding local functions or by developing the Handler class
            //Parse the XML
            IEnumerable<XElement> xElements = ECD_File.parseXML(file);
            //Calculete the total balance of the ECD
            return ECD_File.calculateBalance(xElements);             
        }
        
        static void Main(string[] args)
        {
            // Read configuration from app.config file
            string? workDir = ConfigurationManager.AppSettings["TargetDir"];
            string? fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            string? outputDir = ConfigurationManager.AppSettings["OutputDir"];

            string work_dir= Path.Join(get_home_dir(), workDir); // * 1. Target dir where statements can be found
            string[] files= Directory.GetFiles(work_dir, fileExtension); // * Always xml
            string[] welcome_msg= {"# Programa ECD Handler"
                                ," # The files are going to be selected from "+work_dir};

            List<string> outputLines = new List<string>();
            outputLines.AddRange(welcome_msg);

            print_msgs(txt_msgs: welcome_msg, sep: new string('\n', 2));

            
            foreach (string file in files) // Iterate trough the array of XML files.
            {
                // 3. Results returning is the amount of montoTotal
                decimal totalBalance = process_files_dir(file);

                string[] file_msg_aux = {""
                                ,$"\n Total balance: {totalBalance.ToString("C")}"
                                ,new String('-', 30)};

                // 4. Print the total of each invoice for each ECD provided
                file_msg_aux[0] = "ECD: "+file.Split('\\')[file.Split('\\').Length - 1];
                print_msgs(txt_msgs: file_msg_aux, end: "\n\n");
                outputLines.AddRange(file_msg_aux);
            }

            // Write output to a file
            string outputPath = Path.Combine(get_home_dir(), outputDir);
            File.WriteAllLines(outputPath, outputLines);
            Console.WriteLine($"Output written to: {outputPath}");
            Console.ReadLine();
        }
    }
}

