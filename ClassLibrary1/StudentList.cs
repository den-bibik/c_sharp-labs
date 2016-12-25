
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace lab_libary
{
    [Serializable]
    class StudentList: IDeepCopy
    {
        public System.Collections.Generic.List<Student> list= new System.Collections.Generic.List<Student>();

        public void AddDefaults()
        {
            Student s = new Student("Иванов", "Вася");
            s.AddExams(
                new Exam(SubjectSet.math, Marks.уд, new DateTime(2012, 1, 2)),
                new Exam(SubjectSet.english, Marks.неуд, new DateTime(2010, 1, 1))     
            );

            s.AddTests(
                new Test(SubjectSet.math, true, new DateTime(2011, 11, 1)),
                new Test(SubjectSet.english, true, new DateTime(2011, 11, 15)),
                new Test(SubjectSet.smth, false, new DateTime(2011, 11, 15))
            );

            Student s1 = new Student("Королев", "Паша");
            s1.AddExams(
                new Exam(SubjectSet.math, Marks.уд, new DateTime(2012, 1, 2)),
                new Exam(SubjectSet.english, Marks.уд, new DateTime(2010, 1, 1)),
                new Exam(SubjectSet.smth, Marks.хор, new DateTime(2010, 1, 1))
            );

            s1.AddTests(
                new Test(SubjectSet.math, true, new DateTime(2011, 11, 1)),
                new Test(SubjectSet.english, true, new DateTime(2020, 11, 15)),
                new Test(SubjectSet.smth, true, new DateTime(2011, 1, 16))
            );
            Student s2 = new Student("Королев", "Иван");


            list.Add(s);
            list.Add(s1);
            list.Add(s2);

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

        public object DeepCopy()
        {
            try
            {
                //MemoryStream ms = new MemoryStream();
                MemoryStream ms = new MemoryStream();
                BinaryFormatter binF = new BinaryFormatter();
                binF.Serialize(ms, this);


                ms.Seek(0, SeekOrigin.Begin);
                StudentList st_copy = binF.Deserialize(ms) as StudentList;
                return st_copy;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        static bool Save(string filename, StudentList obj)
        {
            try
            {
                //MemoryStream ms = new MemoryStream();
                FileStream ms = File.Create(filename);
                BinaryFormatter binF = new BinaryFormatter();
                binF.Serialize(ms, obj);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        static bool Load(string filename, ref StudentList obj)
        {
            try
            {
                FileStream ms = File.OpenRead(filename);
                BinaryFormatter binF = new BinaryFormatter();


                ms.Seek(0, SeekOrigin.Begin);
                obj = binF.Deserialize(ms) as StudentList;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IEnumerable<Student> PassedOneDay
        {
            get
            {
                return
                       from st in list
                       where
                           (from g in (from test in st.examList
                                       where test.mark > Marks.неуд
                                       select test.Date).GroupBy(x => x)
                            where g.Count() > 1
                            select g
                            ).Count() > 0
                       select st;
            }
        }

        public IEnumerable<Student> SortedBySurname
        {
            get
            {
                return from x in list 
                       orderby x.surname
                       select x;
            }
        }

        public IEnumerable<Student> SortedByLastDate
        {
            get
            {
                return from st in list where st.testList.Count > 0
                       orderby(from test in st.testList where test.pass select test.Date).Max() select st;
            }
        }

        public IEnumerable<IGrouping<int, Student>> GroupByNTests
        {
            get
            {
                return list.GroupBy(s => s.testList.Sum(t => Convert.ToInt32(t.pass)));
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
                if (list.Count == 0) return new DateTime();
                var query = from st in list
                            from exam in st.examList
                            where st.examList.Count > 0
                            select exam.Date;

                return query.Max();
            }
        }

        public Student LastExamStudent
        {
            get
            {
                DateTime LE = LastExam;
                if (list.Count == 0) return null;
                var query = 
                    from st in list
                    where st.examList.Count > 0
                    where st.examList.Max(x => x.Date) == LE
                    select st;
                if (query.Count() > 0) return query.ElementAt(0);
                return null;
            }
        }

        public List<SubjectSet> EasyTests
        {
            get
            {
                var allSubj =
                    (from st in list
                    from test in st.testList
                    select test).GroupBy(x => x.subject);
                var res =
                    from subj in allSubj
                    where
                           subj.Count(x => x.pass) == subj.Count()
                    select subj.Key;
                return res.ToList();                               
            }
        }

        public IEnumerable<SubjectSet> PassedTestNotExams
        {
            get{
                var allCondSubj = 
                    from st in list
                    from subj in
                        Enumerable.Intersect<SubjectSet>(
                            (from test in st.testList
                            where test.pass
                            select test.subject), 

                            (from exam in st.examList
                            where exam.mark == Marks.неуд
                            select exam.subject)
                        )
                    select subj;
                return 
                    from gr in allCondSubj.GroupBy(x => x)
                    select gr.Key;

            }

        }


    }
}
