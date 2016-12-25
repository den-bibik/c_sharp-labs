using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_libary
{
    public class SJournal
    {
        private System.IO.StreamWriter stream;
        public SJournal(string fileName)
        {
            stream = new System.IO.StreamWriter(fileName, true);
            stream.AutoFlush = true;
            
        }

        public void onEvent<TKey>(object source, SCollectionHandlerEventArgs<TKey> args)
        {
            stream.WriteLine(DateTime.Now);
            stream.WriteLine(args.ToString());
        }
    }
}
