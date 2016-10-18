using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace c_sharp_labs
{
    class Student:Person, IDate, IDeepCopy, IEnumerable
    {
        private List<Test> test = new List<Test>();
        private List<Exam> exam = new List<Exam>();

        public int group { set; get; }
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
        public Student(string surname = "Default", string name = "Default", DateTime birthday = default(DateTime), int group = 1):
            base(name, surname, birthday.Day, birthday.Month, birthday.Year)
        {
            this.group = group;
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
            foreach(Exam e in exam) if(e.mark > Marks.d) s.Add(e.subject);
            foreach(Test t in test) if(t.pass && s.Contains(t.subject)) yield return t.subject;
        }

        public IEnumerable ExamEnumerator(DateTime before)
        {
            foreach (Exam e in exam) if (e.Date <= before) yield return e;
            yield break;
        }
        public override object DeepCopy()
        {
            Student tmp = new Student(surname, name, birthday, group);
            foreach (Exam e in exam) tmp.AddExams((Exam)e.DeepCopy());
            foreach (Test t in test) tmp.AddTests((Test)t.DeepCopy());
            return tmp;
        }
    }
}
