using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_libary
{
    [Serializable]
    enum SubjectSet { math, english, smth }
    [Serializable]
    enum Marks { неуд = 2, уд = 3, хор = 4, отл = 5 }
    [Serializable]
    class Exam: IDate, IDeepCopy
    {
        public SubjectSet subject { get; set; }
        public Marks mark { get; set;  }
        public DateTime Date { get; set; }
        public object DeepCopy()
        {
            return new Exam(subject, mark, Date);
        }

        public Exam(SubjectSet s = SubjectSet.math, Marks m = Marks.хор, DateTime d = default(DateTime))
        {
            subject = s;
            mark = m;
            Date = d;
        }

        public override string ToString()
        {
            return subject.ToString() + " at " + Date.ToShortDateString() + " with mark " + mark.ToString();
        }
    }
}
