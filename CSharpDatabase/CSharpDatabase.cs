using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSharpDatabase
{
    [Serializable]
    public class CSharpDatabase
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        private string Name;

        public CSharpDatabase()
        {

        }

        public void Connect()
        {

        }

        public void Create(string path)
        {
            bool overwrite = true;
            //Check if file already exists
            if (File.Exists(path) is true)
            {
                //Adapt to Console or GUI
                if (GetConsoleWindow() != IntPtr.Zero)
                {
                    //Ask for Overwriting
                    Console.WriteLine("Database is already existing.\nOverwrite it? [yes/no]");
                    while (true)
                    {
                        Console.Clear();
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
                        }
                    }
                }
                else
                {
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
            if (overwrite is true)
            {
                //Create the Database
                using (FileStream stream = File.Create(path))
                {

                }
            }
            else
            {

            }
        }
    }
}
