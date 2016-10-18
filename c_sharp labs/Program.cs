using System;
using System.Collections.Generic;

enum SubjectSet { math, english, smth }
enum Marks {d = 2, c = 3, b = 4, a = 5}


namespace c_sharp_labs
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student();
            s.AddExams(
                new Exam(SubjectSet.math, Marks.c, new DateTime(2012,1,1)),
                new Exam(SubjectSet.english, Marks.d, new DateTime(2010, 1, 1)),
                new Exam(SubjectSet.smth, Marks.c, new DateTime(2012, 1, 2))
            );

            s.AddTests(
                new Test(SubjectSet.math, true, new DateTime(2011, 11, 1)),
                new Test(SubjectSet.english, true, new DateTime(2011, 11, 15)),
                new Test(SubjectSet.smth, false, new DateTime(2011, 11, 15))
            );
            Console.WriteLine(s);
            Console.WriteLine();

            Student s1 = (Student) s.DeepCopy();

            s1.examList[0].mark = Marks.a;
            Console.WriteLine(s1);
            Console.WriteLine();

            Console.WriteLine(s);
            Console.WriteLine();

            foreach(SubjectSet subj in s) Console.WriteLine(subj);
            foreach(Exam i in s.ExamEnumerator(new DateTime(2012, 1, 1)))
            {
                Console.WriteLine(i);
            }

            List<Person> p = new List<Person>();
            p.Add(s);
            p.Add(s1);
            p.Add(new Person());

            foreach (Person pers in p)
                Console.WriteLine(pers.ToShortString());
            

            List<IDate> idate = new List<IDate>();
            idate.Add(s);
            idate.Add(new Exam(SubjectSet.english, Marks.d, new DateTime(2012, 1, 25)));
            idate.Add(new Test(SubjectSet.math, true, new DateTime(2011, 10, 1)));
            foreach (IDate id in idate)
                Console.WriteLine(id.Date.ToShortDateString());


            Console.ReadKey();
        }
    }
}
