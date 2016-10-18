using System;

enum SubjectSet { math, english }
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
                new Exam(SubjectSet.english, Marks.c, new DateTime(2012, 1, 2))
            );

            s.AddTests(
                new Test(SubjectSet.math, true, new DateTime(2011, 11, 1)),
                new Test(SubjectSet.english, false, new DateTime(2011, 11, 15))
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
            Console.ReadKey();
        }
    }
}
