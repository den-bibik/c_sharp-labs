using System;

namespace c_sharp_labs
{
    class Person:IDeepCopy, IDate
    {
        protected string[] nm =new String[2];
        protected System.DateTime birthday;

        public Person(string name =  "Default", string surname = "Default", int day = 1, int month = 1, int year = 1984)
        {
            nm[0] = name;
            nm[1] = surname;
            birthday = new DateTime(year, month, day);
        }

        public string name
        {
            set
            {
                nm[0] = value;
            }
            get
            {
                return nm[0];
            }
        }

        public string surname
        {
            set
            {
                nm[1] = value;
            }
            get
            {
                return nm[1];
            }
        }

        

        public override string ToString()
        {
            return nm[1] + " " + nm[0] + " " + birthday.ToShortDateString();
        }

        public virtual String ToShortString()
        {
            return nm[1] + " " + nm[0][0];
        }

        public DateTime Date
        {
            get
            {
                return default(DateTime);
            }

            set
            {
                birthday = value;
            }
        }

        public virtual object DeepCopy()
        {
            return new Person(nm[0], nm[1], birthday.Day, birthday.Month, birthday.Year);
        }

    }


}
