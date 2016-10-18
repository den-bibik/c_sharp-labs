using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_labs
{
    class Student
    {
        private Person pers;
        public int group { set; get; }
        private Test[] test;

        public Student(string surname = "Default", string name = "Default", DateTime birthday = default(DateTime), int group = 1)
        {
            this.pers = new Person(name, surname, birthday.Day, birthday.Month, birthday.Year);
            this.group = group;
        }

       public Person person{
            get
            {
                return pers;
            }
            set
            {
                pers = value;
            }
        }

        public Test[] testList
        {
            get
            {
                return test;
            }
            set
            {
                test = value;
            }
        }

        public void AddTests(params Test[] par)
        {
            test.Concat(par);
        }

        public override string ToString()
        {
            string s = "tests:";
            foreach (Test t in test)
                s = string.Concat(s, "\n" + test.ToString());
            return pers.ToString() + " group:" + group.ToString() + s;
        }
        public string ToShortString()
        {
            return pers.ToString() + " group:" + group.ToString();
        }
    }
}
