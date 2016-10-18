﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    class Exam: IDate, IDeepCopy
    {
        public SubjectSet subject { get; set; } = SubjectSet.math;
        public Marks mark { get; set;  }
        public DateTime Date { get; set; }
        public object DeepCopy()
        {
            return new Exam(subject, mark, Date);
        }

        public Exam(SubjectSet s = SubjectSet.math, Marks m = Marks.b, DateTime d = default(DateTime))
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