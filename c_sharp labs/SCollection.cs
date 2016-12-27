using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    enum SearchType{ byKey = 0, byValue };
    delegate TKey KeySelector<TKey>(Person ps);
    class SCollectionHandlerEventArgs<TKey>: EventArgs
    {
        private TKey key;
        private DateTime search_time;
        private bool ok;
        private SearchType sType;

    }
    delegate void SCollectionHandler<TKey>(object source, SCollectionHandlerEventArgs<TKey> args);

    class SCollection<TKey>
    {
        
        private System.Collections.Generic.Dictionary<TKey, Student> dict;
        private KeySelector<TKey> ks;
        public SCollection(KeySelector<TKey> ks)
        {
            this.ks = ks;
        }
        public void addDefaults()
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
                new Exam(SubjectSet.english, Marks.отл, new DateTime(2010, 1, 1)),
                new Exam(SubjectSet.english, Marks.хор, new DateTime(2010, 1, 1))
            );

            s1.AddTests(
                new Test(SubjectSet.math, true, new DateTime(2011, 11, 1)),
                new Test(SubjectSet.english, false, new DateTime(2020, 11, 15)),
                new Test(SubjectSet.smth, true, new DateTime(2011, 1, 16))
            );
            Student s2 = new Student("Королев", "Иван");


            dict.Add(ks(s),s);
            dict.Add(ks(s1), s1);
            dict.Add(ks(s2), s2);
        }
        public void addstudents(int n)
        {
            for (int i = 0; i < n; i++){
                Student s = new Student();
                dict.Add(ks(s), s);
            }
        }
        public bool ContainsStudent(TKey Key)
        {
            return dict.ContainsKey(Key);
        }
        public bool ContainsStudent(Student st)
        {
            return dict.ContainsValue(st);
        }
        public void RemoveStudentsByExam(SubjectSet subject)
        {
            var query =
                from st in dict
                from exam in st.Value.examList
                where exam.subject == subject
                where exam.mark == Marks.неуд
                select st.Key;
            foreach (var stKey in query)
                dict.Remove(stKey);
        }
        public void RemoveStudentsByTests()
        {
            var query =
                from st in dict
                where
                    (from test in st.Value.testList
                     where test.pass
                     select test
                     ).Count() == 0
                select st.Key;   
            foreach (var stKey in query)
                dict.Remove(stKey);
        }

    }
}
