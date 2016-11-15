
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    class StudentList
    {
        private System.Collections.Generic.List<Student> list= new System.Collections.Generic.List<Student>();

        public void AddDefaults()
        {
            Student s = new Student();
            s.AddExams(
                new Exam(SubjectSet.math, Marks.уд, new DateTime(2012, 1, 1)),
                new Exam(SubjectSet.english, Marks.неуд, new DateTime(2010, 1, 1))     
            );

            s.AddTests(
                new Test(SubjectSet.math, true, new DateTime(2011, 11, 1)),
                new Test(SubjectSet.english, true, new DateTime(2011, 11, 15)),
                new Test(SubjectSet.smth, false, new DateTime(2011, 11, 15))
            );

            Student s1 = (Student)s.DeepCopy();
            s1.examList[0].mark = Marks.отл;
            s1.AddExams(new Exam(SubjectSet.smth, Marks.уд, new DateTime(2012, 1, 2)));

            list.Add(s);
            list.Add(s1);
            list.Add(new Student());

        }

        public override string ToString()
        {
            string str = "";
            foreach(Student s in list)
            {
                string.Concat(str,s.ToString() + "\n");
            }
            return str;
        }

        double maxAverage
        {
            get
            {
                if(list.Count() > 0)
                    return list.Max(x => x.res);
                return -1.0;
            }
        }
        DateTime LastExam
        {
            get
            {
                return list.Max(x => x.examList.Max(y => y.Date));
            }
        }

        Student LastExamSrudent
        {
            get
            {
                DateTime maxDT = LastExam;
                foreach (Student s in list)
                {
                    int examIndex = s.examList.FindIndex(y => y.Date == maxDT);
                    if(examIndex != -1) return s;
                }
                return null;
            }
        }

        List<SubjectSet> EasyTests
        {
            get
            {
                HashSet<SubjectSet> res = new HashSet<SubjectSet>();
                HashSet<SubjectSet> notPassed = new HashSet<SubjectSet>();
                foreach (Student s in list)
                {
                    foreach (Test t in s.testList)
                    {
                        res.Add(t.subject);
                        if (!t.pass)
                            notPassed.Add(t.subject);
                    }
                }
                return new List<SubjectSet>(res.Except(notPassed));
            }
        }

    }
}
