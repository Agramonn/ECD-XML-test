// Program to select and process Daily Statements Accounts (ECD in spanish)
//  Bravos tecnichal test for web developers applicants
//  Follow the instructions from README file

// using ECD_Handler.Handler;


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
        static void process_files_dir(string[] files) // Currently not returning a value
        {
            string[] file_msg_aux= {""
                                ,""
                                ,new String('-', 30)};
            foreach(string file in files)
            {
                file_msg_aux[0]= file.Split('\\')[file.Split('\\').Length - 1];
                // only displays the filename
                print_msgs(txt_msgs: file_msg_aux, end: "\n\n");
                
                // 2. Processing: either by adding local functions or by developing the Handler class
            }
            
            
        }
        
        static void Main(string[] args)
        {
            string work_dir= Path.Join(get_home_dir(), "Downloads"); // * 1. Target dir where statements can be found
            string[] files= Directory.GetFiles(work_dir, "*.xml"); // * Always xml
            string[] welcome_msg= {"   # Programa ECD Handler"
                                ," # The files are going to be selected from "+work_dir};
            
            print_msgs(txt_msgs: welcome_msg, sep: new string('\n', 2));
            
            // 3. Results returning??
            process_files_dir(files);
            
            // 4. Print the total of each invoice for each ECD provided
        }
    }
}

