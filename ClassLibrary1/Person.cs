using System;

namespace lab_libary
{
    delegate void GroupChanged();
    [Serializable]
    class Person:IDeepCopy, IDate
    {
        public event GroupChanged groupChanged;
        protected void onGroupChanged()
        {
            if(groupChanged != null)
                groupChanged();
        }
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
                onGroupChanged();
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
                return birthday;
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
