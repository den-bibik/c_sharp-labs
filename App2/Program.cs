﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab_libary;

namespace App2
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentList stlist = null;
            StudentList.Load("../../../stlist", ref stlist);
            Console.WriteLine(stlist);
            Console.ReadKey();
        }
    }
}
