using System;
using System.Diagnostics;
using System.IO;

namespace CSharpDatabase
{
    public class CSharpDatabase
    {
        private FileStream stream;
        public CSharpDatabase()
        {

        }

        public void Open(string path)
        {
            //Open the Database
            stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            byte[] temp = new byte[1];
            stream.Read(temp, 0, temp.Length);
            string x = System.Text.Encoding.GetEncoding(437).GetString(temp);
            if(x)
        }

        public void Create(string path)
        {

            //Check if Path has File Extension
            if (path.Contains("."))
            {
                path = path.Remove(path.IndexOf("."));
            }

            //Create the Database with csdb file extension (CSharpDatabase)
            using (FileStream stream = File.Create(path + ".csdb"))
            {
                //Add the Header
                AddHeader(path + ".csdb", stream);
                stream.Close();
            }
        }

        private void AddHeader(string path, FileStream stream)
        {
            Header header = new Header();
            stream.Write(header.header, 0, header.header.Length);
        }

        public int GetTableCount()
        {
            if (stream.CanRead)
            {
                byte[] temp = new byte[1];
                int tableCount;

                stream.Position = 11;
                stream.Read(temp, 0, temp.Length);
                tableCount = Convert.ToInt32(System.Text.Encoding.GetEncoding(437).GetString(temp));

                return tableCount;
            }
            else
                throw new Exception("No Database opened");
        }
    }

    class Table
    {
        private string[] columns;
        public Table(string[] columns)
        {
            this.columns = columns;
        }

    }

    class Header
    {
        public readonly byte[] header;
        private int tableCount = 0;
        public Header()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            header = System.Text.Encoding.GetEncoding(437).GetBytes("\01" + version + "\02" + tableCount + "\03CSharpDatabase");
        }
    }
}
