using System;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FileStreamEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading Stream on file TestFile.txt");
            string path = ConfigurationManager.AppSettings.Get("testfilePath");
            Console.WriteLine($"path {path}");
            Stream strm = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamIterator StrmIterClassList = new StreamIterator(strm);
            foreach (var i in StrmIterClassList)
            {
                Console.WriteLine($"Iteration {i}");
            }
        }
    }
}
