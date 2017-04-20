using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using lab_libary;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace GuiApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            soc.changed = true;
        }
        public void Save(object sender, ExecutedRoutedEventArgs e)
        {
                
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    FileStream fs = new FileStream(dlg.FileName, FileMode.Create);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, soc);
                }
                soc.changed = false;
 
        }
        public void New(object sender, ExecutedRoutedEventArgs e)
        {
            if(soc.changed == true)
            {
                if(MessageBox.Show("Сохранить?", "Confirm", MessageBoxButton.YesNo).ToString() == "Yes"){
                    object obj = new object();
                    ApplicationCommands.Save.Execute(null, null);
                }

            }

            soc.Clear();
        }
        public void Open(object sender, ExecutedRoutedEventArgs e)
        {
           
            if (soc.changed == true)
            {
                if (MessageBox.Show("Сохранить?", "Confirm", MessageBoxButton.YesNo).ToString() == "Yes")
                {
                    object obj = new object();
                    ApplicationCommands.Save.Execute(null, null);
                }

            }

            FileStream fs = null;
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    fs = new FileStream(dlg.FileName, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();
                    soc = (StudentObservableCollection)formatter.Deserialize(fs);

                }
                soc.changed = false;
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            soc.Remove((Student)lst.SelectedItem);
        }
        private void AddDefaultStudent(object sender, ExecutedRoutedEventArgs e)
        {
            soc.Add_DefaultStudent(1);
        }
        private void Add(object sender, ExecutedRoutedEventArgs e)
        {
            soc.Add_Student(new Student());
        }
        private StudentObservableCollection s;
        public StudentObservableCollection soc
        {
            get {
                return s;
            }
            set
            {
                s = value;
                this.DataContext = null;
                this.DataContext = this;
                s.CollectionChanged += this.OnCollectionChanged;
            }
        }
        void OnClose(object sender, EventArgs e)
        {
            if (soc.changed == true)
            {
                if (MessageBox.Show("Сохранить?", "Confirm", MessageBoxButton.YesNo).ToString() == "Yes")
                {
                    Save(null, null);
                }

            }
        }

        public static Array MarksArray { set; get; } = Enum.GetValues(typeof(Marks));
        public MainWindow()
        {
            
            soc = new StudentObservableCollection();
            
            //soc.Add_DefaultStudent(7);
            InitializeComponent();
            this.Closed += OnClose;
            //lst.ItemsSource = soc;
        }
      


    }
}
