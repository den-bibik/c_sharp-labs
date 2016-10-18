using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
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
            return subject.ToString() + " " + pass.ToString() + " " + Date.ToString();
        }

    }
}
