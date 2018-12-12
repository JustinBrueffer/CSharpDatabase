using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSharpDatabase
{
    public class CSharpDatabase
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        private string Name;

        public CSharpDatabase()
        {

        }

        public void Open(string path)
        {
            
        }

        public void Create(string path)
        {
            bool overwrite = true;

            //Check if Path has File Extension
            if (path.Contains("."))
            {
                path = path.Remove(path.IndexOf("."));
            }
            //Check if file already exists
            if (File.Exists(path + ".csdb"))
            {
                //Adapt to Console or GUI
                if (GetConsoleWindow() != IntPtr.Zero)
                {
                    //Ask for Overwriting
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Database is already existing.\nOverwrite it? [yes/no]");
                        var choice = Console.ReadLine();
                        if (choice.ToLower() == "yes")
                        {
                            overwrite = true;
                            break;
                        }
                        else if (choice.ToLower() == "no")
                        {
                            overwrite = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("No valid option.");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    //Ask for Overwriting
                    if (MessageBox.Show(null, "Database is already existing.\nOverwrite it?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) is DialogResult.Yes)
                    {
                        overwrite = true;
                    }
                    else
                    {
                        overwrite = false;
                    }
                }
            }
            //Check if overwriting is allowed
            if (overwrite)
            {
                //Create the Database with csdb file extension (CSharpDatabase)
                try
                {
                    using (FileStream stream = File.Create(path + ".csdb"))
                    {
                        stream.Close();
                    }
                    Console.WriteLine("Database has been created. Open it? [yes/no]");
                    while (true)
                    {
                        var choice = Console.ReadLine();
                        if (choice.ToLower() == "yes")
                        {
                            Open(path + ".csdb");
                            break;
                        }
                        else if (choice.ToLower() == "no")
                        {

                            break;
                        }
                        else
                        {
                            Console.WriteLine("No valid option.");
                            Console.ReadKey();
                        }
                    }
                }
                catch (Exception e)
                {
                    //Inform the user if the path
                    if (e is DirectoryNotFoundException)
                    {
                        Console.WriteLine("The used path is unavailable or doesn´t exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Database has not been overwritten.");
            }
        }
    }
}
