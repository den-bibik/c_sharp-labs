using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace lab_libary
{
    [Serializable]
    public class Student:Person, IDate, IDeepCopy, IComparable<Student>, IComparer<Student>
    {
        private List<Test> test = new List<Test>();
        private List<Exam> exam = new List<Exam>();
        private int group_num;

        public int group {
            set {
                group_num = value;
                onGroupChanged();

            }
            get { return group_num; } }
        public double res { get {
                int i = 0;
                int sum = 0;
                foreach (Exam e in exam)
                {
                    i++;
                    sum += (int)e.mark;
                }
                return (double)sum / (double)i;
            }
        }
        public Student(int maxN, Random rnd)//random student
        {
            string[] names = { "Ivan", "Oleg", "Julia", "Kate", "Bob", "Alice" };
            string[] surnames = { "Smith", "Ivanov", "Petrov", "Duran", "Dvacher"};
            Array subjects = Enum.GetValues(typeof(SubjectSet));
            Array markAr = Enum.GetValues(typeof(Marks));

            this.name = names[rnd.Next(0, names.Length)];
            this.surname = surnames[rnd.Next(0, surnames.Length)];
            this.group = rnd.Next(20);
            this.birthday = new DateTime(rnd.Next(1990, 1999), rnd.Next(1, 12), rnd.Next(1, 28));

            for(int i = 0; i < rnd.Next(maxN); i++)
            {
                Exam s = new Exam(
                    (SubjectSet)subjects.GetValue(rnd.Next(subjects.Length)),
                    (Marks)markAr.GetValue(rnd.Next(markAr.Length)),
                    new DateTime(rnd.Next(2010, 2015), rnd.Next(1, 12), rnd.Next(1, 28))
                );
                this.AddExams(s);
            }

            for (int i = 0; i < rnd.Next(maxN); i++)
            {
                Test s = new Test(
                    (SubjectSet)subjects.GetValue(rnd.Next(subjects.Length)),
                    rnd.NextDouble() < 0.2 ? true : false,
                    new DateTime(rnd.Next(2010, 2015), rnd.Next(1, 12), rnd.Next(1, 28))
                );
                this.AddTests(s);
            }

        }

        public Student(string surname = "Default", string name = "Default" , DateTime birthday = default(DateTime), int group = 1):
            base(name, surname, birthday.Day, birthday.Month, birthday.Year)
        {
            this.group_num = group;
        }


        public List<Test> testList
        {
            get { return test; }
            set { test = value; }
        }
        public List<Exam> examList
        {
            get { return exam; }
            set { exam = value; }
        }

        public void AddTests(params Test[] par)
        {
            test.AddRange(par);
        }

        public void AddExams(params Exam[] ex)
        {
            exam.AddRange(ex);
        }

        public override string ToString()
        {
            string tst = "tests:";
            foreach (Test t in test)
                 tst = string.Concat(tst, "\n" + t.ToString());
            string exm = "exams:";
            foreach (Exam e in exam)
                exm = string.Concat(exm, "\n" + e.ToString());

            return base.ToString() + " group:" + group.ToString() + " average: " + res.ToString() + "\n" + tst + "\n" + exm;
        }
        public override string ToShortString()
        {
            return base.ToShortString() + " group:" + group.ToString() + " average:" + res.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            HashSet<SubjectSet> s = new HashSet<SubjectSet>();
            foreach(Exam e in exam) if(e.mark > Marks.неуд) s.Add(e.subject);
            foreach(Test t in test) if(t.pass && s.Contains(t.subject)) yield return t.subject;
        }

        public IEnumerable ExamEnumerator(DateTime before)
        {
            foreach (Exam e in exam) if (e.mark > Marks.неуд && e.Date <= before) yield return e;
        }
        public override object DeepCopy()
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BinaryFormatter binF = new BinaryFormatter();
                binF.Serialize(ms, this);


                ms.Seek(0, SeekOrigin.Begin);
                Student st_copy = binF.Deserialize(ms) as Student;
                return st_copy;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int CompareTo(Student obj)
        { 
            return res.CompareTo(obj.res);
        }

        public int Compare(Student x, Student y)
        {
            return x.exam.Count.CompareTo(y.exam.Count);
        }
    }
}
