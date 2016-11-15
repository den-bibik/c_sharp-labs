using System;
using System.Collections.Generic;

enum SubjectSet { math, english, smth }
enum Marks {неуд = 2, уд = 3, хор = 4, отл = 5}


namespace c_sharp_labs
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentList s = new StudentList();
            s.AddDefaults();
            Console.WriteLine(s);
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(s.LastExam);
            Console.WriteLine(s.maxAverage);
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(s.LastExamSrudent);

            Console.ReadKey();
        }
    }
}
