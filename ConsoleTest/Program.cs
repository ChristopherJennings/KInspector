﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kentico.KInspector.WebApplication;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://localhost:9000"))
            {
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();
            }
        }
    }
}
