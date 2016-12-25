using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab_libary;

namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            Student st = new Student(5, new Random());
            Student st_copy = (Student)st.DeepCopy();
            Console.WriteLine("Student");
            Console.WriteLine(st);
            Console.WriteLine("\n\ncopy");
            Console.WriteLine(st_copy);

            StudentList stlist = new StudentList();
            stlist.AddDefaults();

            StudentList.Save("../../../stlist", stlist);


            Console.ReadKey();
        }
    }
}
