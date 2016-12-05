using System;
using System.Collections.Generic;
using System.Linq;

enum SubjectSet { math, english, smth }
enum Marks {неуд = 2, уд = 3, хор = 4, отл = 5}


namespace c_sharp_labs
{
    class Program
    {
        static string ks(Person st)
        {
            return st.name + st.surname + ((Student)st).group.ToString() + st.Date.ToShortDateString();
        }
        static void Main(string[] args)
        {
            /*StudentList s = new StudentList();
            s.AddDefaults();
            Console.WriteLine(s);
            Console.WriteLine(); Console.WriteLine("Last exam");
            Console.WriteLine(s.LastExam);
            Console.WriteLine(); Console.WriteLine("Max average");
            Console.WriteLine(s.maxAverage);
            Console.WriteLine(); Console.WriteLine("Last exam student");
            Console.WriteLine(s.LastExamStudent);
            Console.WriteLine(); Console.WriteLine("Passed by everybody tests");
            foreach (SubjectSet t in s.EasyTests)
                Console.WriteLine(t);
           Console.WriteLine(); Console.WriteLine("Tests passed one day");
            foreach (Student st in s.PassedOneDay)
                Console.WriteLine(st);
           Console.WriteLine(); Console.WriteLine("Sorted by surname students");
            foreach (Student st in s.SortedBySurname)
                Console.WriteLine(st.ToShortString());
            Console.WriteLine("\nSoretd by last date");
            foreach (Student st in s.SortedByLastDate)
                Console.WriteLine(st.ToString());

            Console.WriteLine("Group by num of tests");
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

            Console.WriteLine(); Console.WriteLine("Passed test not exams");
            foreach (SubjectSet t in s.PassedTestNotExams)
                Console.WriteLine(t);*/

            var col = new SCollection<string>(ks);

            col.AddStudents(100);
            var generated = new System.IO.StreamWriter("generated");
            generated.AutoFlush = true;
            generated.WriteLine(col);

            SJournal sj = new SJournal("journal");
            col.ContainsStudentEvent += sj.onEvent;

            col.ContainsStudent("Kate");
            col.ContainsStudent(col.takeStudent().Key);
            col.ContainsStudent(new Student(3, new Random()));
            col.ContainsStudent(col.takeStudent().Value);

            col.RemoveStudentsByExam(SubjectSet.english);
            var engDeleted = new System.IO.StreamWriter("engDeleted");
            engDeleted.AutoFlush = true;
            engDeleted.WriteLine(col);

            col.RemoveStudentsByTests();
            var testDeleted = new System.IO.StreamWriter("testDeleted");
            testDeleted.AutoFlush = true;
            testDeleted.WriteLine(col);

            Console.ReadKey();
        }
    }
}
