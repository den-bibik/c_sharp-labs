using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    class Student:Person, IDate, IDeepCopy, IEnumerable
    {
        private List<Test> test;
        private List<Exam> exam;

        public int group { set; get; }
        public double res { get; }
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
            test.Concat(par);
        }

        public void AddExams(params Exam[] par)
        {
            exam.Concat(par);
        }

        public override string ToString()
        {
            string tst = "tests:";
            foreach (Test t in test)
                 tst = string.Concat(tst, "\n" + t.ToString());
            string exm = "exams:";
            foreach (Exam e in exam)
                tst = string.Concat(exm, "\n" + e.ToString());

            return base.ToString() + " group:" + group.ToString() + "average: " + res.ToString() + "\n" + tst + "\n" + exm;
        }
        public override string ToShortString()
        {
            return base.ToShortString() + " group:" + group.ToString() + "average: " + res.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return test.GetEnumerator();
        }
        public override object DeepCopy()
        {
            return default(object);
        }
    }
}
