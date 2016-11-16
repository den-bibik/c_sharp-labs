using System;
using System.Collections.Generic;
using System.Linq;

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
           /* Console.WriteLine(s);
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(s.LastExam);
            Console.WriteLine(s.maxAverage);
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine(s.LastExamStudent);
            Console.WriteLine(); Console.WriteLine();*/
            foreach (SubjectSet t in s.EasyTests)
                Console.WriteLine(t);
           /* Console.WriteLine(); Console.WriteLine();
            foreach (Student st in s.PassedOneDay)
                Console.WriteLine(st);
           Console.WriteLine(); Console.WriteLine();
            foreach (Student st in s.SortedBySurname)
                Console.WriteLine(st.ToShortString());
             Console.WriteLine("\n Soreted by last date");
            foreach (Student st in s.SortedByLastDate)
                Console.WriteLine(st.ToString());

            Console.WriteLine();
            IEnumerable<IGrouping<int, Student>> query = s.GroupByNTests;
            foreach(IGrouping<int, Student> group in query)
            {
                Console.WriteLine(group.Key);
                foreach (Student st in group) Console.WriteLine(st.ToShortString());
            }
            Console.WriteLine(); Console.WriteLine("LINQ");
            Console.WriteLine("Выведем студентов, с именем содежащим 'В'");
            var query1 = from Student in s.list where Student.name.Contains("В") select Student.ToShortString();
            foreach (var a in query1)
            {
                System.Console.WriteLine(a);
            }
            */
            Console.ReadKey();
        }
    }
}
