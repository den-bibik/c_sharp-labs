using System;

namespace lab_libary
{

    class Test:IDate
    {
        public SubjectSet subject { get; set; }
        public bool pass { get; set; }
        public DateTime Date { get; set; }

        public Test(SubjectSet subject = SubjectSet.math, bool pass = true, DateTime Date = default(DateTime))
        {
            this.subject = subject;
            this.pass = pass;
            this.Date = Date;
        }
        public override string ToString()
        {
            return subject.ToString() + " " + pass.ToString() + " " + Date.ToShortDateString();
        }
        public object DeepCopy()
        {
            return new Test(subject, pass, Date);
        }

    }
}
