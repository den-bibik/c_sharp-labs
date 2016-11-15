
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

        }

        public override string ToString()
        {
            string str = "";
            foreach(Student s in list)
            {
                str = string.Concat(str,s.ToString() + "\n");
            }
            return str;
        }
        public IEnumerable<Student> PassedOneDay
        {
            get
            {
                foreach(Student s in list)
                {
                    bool ok = false;
                    HashSet<DateTime> examDates = new HashSet<DateTime>();
                    foreach (Exam e in s.examList)
                    {
                        if(e.mark > Marks.неуд)
                        {
                            if (examDates.Contains(e.Date))
                            {
                                ok = true;
                                break;
                            }
                            examDates.Add(e.Date);
                        }
                    }
                    if(ok) yield return s;
                }
            }
        }

        public IEnumerable<Student> SortedBySurname
        {
            get
            {
                list.Sort((x, y) => x.surname.CompareTo(y.surname));
                foreach (Student s in list)
                    yield return s;
            }
        }

        public IEnumerable<Student> SortedByLastDate
        {
            get
            {
                list.Sort((x, y) => x.testList.Max(t => t.Date).CompareTo(y.testList.Max(t => t.Date)));
                foreach (Student s in list)
                {
                    bool ok = true;
                    foreach (Test t in s.testList) ok = ok && t.pass;
                    if(ok) yield return s;
                }
            }
        }

        public IEnumerable<IGrouping<int, Student>> GroupByNTests
        {
            get
            {
                return list.GroupBy(s => s.testList.Sum(t => t.pass.CompareTo(true)));
            }
        }

        public double maxAverage
        {
            get
            {
                if(list.Count() > 0)
                    return list.Max(x => x.res);
                return -1.0;
            }
        }
        public DateTime LastExam
        {
            get
            {
                return list.Max(x =>(x.examList.Max(y => y.Date)));
            }
        }

        public Student LastExamSrudent
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

        public List<SubjectSet> EasyTests
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
