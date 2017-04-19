using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration conf = ConfigurationReader.Read();
            if (conf != null)
            {
                foreach(string url in conf.Urls)
                {
                    Console.WriteLine("Url: " + url);
                }

                foreach (SharepointList splist in conf.SharepointLists)
                {
                    Console.WriteLine("ListName: " + splist.Name);
                    foreach(string col in splist.Columns)
                    {
                        Console.WriteLine("Column: " + col);
                        Console.WriteLine("Content: " + splist.Content.Find(c => c.Key == col).Value);
                    }
                }
            }
            else
            {
                Console.WriteLine("Configuration is not available");
            }
            Console.WriteLine("\npress any key to exit");
            Console.ReadKey(false);
        }
    }
}
