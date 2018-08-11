using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties prop = new Properties(args);
            if (prop.ICheckedProperties())
            {
                ZipManager zip = new ZipManager(prop);
            }           
        }
    }    
}



