using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Model
{
    class ProcessModel : INotifyPropertyChanged
    {
        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        #region Fields

        private string _ProcessName;
        private int _ProcessId;
        private int _ThreadsCount;
        private string fullname = null;

        #endregion


        #region Properties

        /// <summary>
        /// Get or set process name.
        /// </summary>
 
        public string ProcessName
        {
            get { return _ProcessName; }
            set
            {
                if (_ProcessName != value)
                {
                    _ProcessName = value;
                    OnPropertyChanged("ProcessName");
                }
            }
        }


        /// <summary>
        /// Get or set process id.
        /// </summary>

        public int ProcessId
        {
            get { return _ProcessId; }
            set
            {
                if (_ProcessId != value)
                {
                    _ProcessId = value;
                    OnPropertyChanged("ProcessId");
                }
            }
        }

        /// <summary>
        /// Get or set process threads count.
        /// </summary>

        public int ThreadsCount
        {
            get { return _ThreadsCount; }
            set
            {
                if (_ThreadsCount != value)
                {
                    _ThreadsCount = value;
                    OnPropertyChanged("ThreadsCount");
                }
            }
        }

        public string FullName
        {
            get
            {
                if (fullname == null)
                {
                    fullname = "ProcessName: " + ProcessName + " ID: " + ProcessId + " Threads count : " + ThreadsCount;
                }
                return fullname;
            }
            set
            {
                fullname = value;
                OnPropertyChanged("FullName");
            }
        }

        #endregion
    }
}
