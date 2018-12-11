using System;
using System.Collections.Generic;
using System.Text;
using CSharpDatabase;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CSharpDatabase.CSharpDatabase cSharpDatabase = new CSharpDatabase.CSharpDatabase();
            cSharpDatabase.Create("Test.db");
        }
    }
}
