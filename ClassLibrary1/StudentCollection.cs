using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_libary
{
    public enum SearchMetod{byKey, byValue};
    public class SCollectionHandlerEventArgs<TKey>:System.EventArgs
    {
        public TKey tKey { set; get; }
        public int searchTime { set; get; }
        public bool searched { set; get; }
        public SearchMetod searchMetod { set; get;}
        public override string ToString()
        {
            string s =
                "tkey: " + tKey.ToString() + "\n" +
                "Время поиска: " + searchTime.ToString() + "\n" +
                "Найдено: " + searched.ToString() + "\n" +
                "Способ поиска: " + searchMetod.ToString() + "\n";
            return s;
 
        }

    }
    public delegate TKey KeySelector<TKey>(Person ps);
    public delegate void SCollectionHandler<TKey>(object source, SCollectionHandlerEventArgs<TKey> args);

    public class SCollection<TKey>
    {
        private Dictionary<TKey, Student> collection;
        private KeySelector<TKey> keySelector;
        public SCollection(KeySelector<TKey> ks) {
            keySelector = ks;
            collection = new Dictionary<TKey, Student>();
        }
        public KeyValuePair<TKey, Student> takeStudent()
        {
            return collection.Take(1).Single();
        }
        public void AddDefaults() {
            Student s = new Student("Иванов", "Вася");
            s.AddExams(
                new Exam(SubjectSet.math, Marks.уд, new DateTime(2012, 1, 2)),
                new Exam(SubjectSet.english, Marks.неуд, new DateTime(2010, 1, 1))
            );
            collection.Add(keySelector(s), s);

        }
        public void AddStudents(int n) {
            //Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                Student s = new Student(i.ToString());
                collection.Add(keySelector(s), s);
            }

        }
        public bool ContainsStudent(TKey Key) {
            SCollectionHandlerEventArgs<TKey> arg = new SCollectionHandlerEventArgs<TKey>();
            arg.searchMetod = SearchMetod.byKey;
            arg.tKey = Key;
            arg.searchTime = Environment.TickCount;
            arg.searched = collection.ContainsKey(Key);
            arg.searchTime = Environment.TickCount - arg.searchTime;

            ContainsStudentEvent(this, arg);

            return arg.searched;
        }
        public bool ContainsStudent(Student st)
        {
            SCollectionHandlerEventArgs<TKey> arg = new SCollectionHandlerEventArgs<TKey>();
            arg.searchMetod = SearchMetod.byValue;

            /*arg.searchTime = new System.Diagnostics.Stopwatch();
            arg.searchTime.Start();
            arg.searched = collection.ContainsValue(st);
            arg.searchTime.Stop();*/
            arg.searchTime = Environment.TickCount;
            arg.searched = collection.ContainsValue(st);
            arg.searchTime = Environment.TickCount - arg.searchTime;
            arg.tKey = keySelector(st);

            ContainsStudentEvent(this, arg);

            return arg.searched;
        }
        public void RemoveStudentsByExam(SubjectSet subject)
        {
            var toRemove =
                from st in collection
                where (
                    from exam in st.Value.examList
                    where ((exam.subject == subject) && (exam.mark == Marks.неуд))
                    select exam
                ).Count() > 0
                select st.Key;

            foreach (var st in toRemove.ToList())
                collection.Remove(st);        
                
        }
        public void RemoveStudentsByTests()
        {
            var toRemove =
                from st in collection
                where (
                    from test in st.Value.testList
                    where test.pass == true
                    select test
                ).Count() == 0
                select st.Key;

            foreach (TKey st in toRemove.ToList())
                collection.Remove(st);

        }

        public override string ToString()
        {
            string s = collection.Count().ToString() +" elements in collection\n\n\n";
            foreach (var st in collection)
            {
                s += st.Key.ToString() + "\n";
                s += st.Value.ToString() + "\n\n";
            }
            return s;
        }

        public event SCollectionHandler<TKey> ContainsStudentEvent;
    }
}
