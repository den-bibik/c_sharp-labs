using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GuiApp
{
    using lab_libary;
    [Serializable]
    public class StudentObservableCollection : System.Collections.ObjectModel.ObservableCollection<Student>
    {

        public void Add_Student(Student Item)
        {
            this.Add(Item);
        }
        public void Remove_StudentAt(int index)
        {
            this.RemoveAt(index);
        }
        public void Add_DefaultStudent(int num)
        {
            Random rnd = new Random();
            for (int i = 0; i < num; i++)
            {
                this.Add(new Student(10, rnd));
            }
        }
        public bool changed = false;
        public override string ToString()
        {
            string s = "";
            foreach(Student x in this)
                s += x.ToString() + "\n";
            return s;
        }
    }
}
