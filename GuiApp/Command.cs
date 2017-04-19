using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GuiApp
{
    public static class Command
    {
        public static readonly RoutedUICommand AddDefaultStudent = new RoutedUICommand("Add default student", "AddDefaultStudent", typeof(Window));
        public static readonly RoutedUICommand AddStudent = new RoutedUICommand("Add student", "AddStudent", typeof(Window));
    }
}
