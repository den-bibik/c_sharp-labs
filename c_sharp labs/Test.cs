using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    
    class Test
    {
        private SubjectSet subject;
        private bool pass;
        public Test(SubjectSet subject = SubjectSet.math, bool pass = true)
        {
            this.subject = subject;
            this.pass = pass;
        }
        public override string ToString()
        {
            return subject.ToString() + " " + pass.ToString();
        }

    }
}
