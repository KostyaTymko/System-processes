using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApplication.Model;
//using System.Timers;

namespace WpfApplication.ViewModel
{
    class MainViewModel
    {

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainViewModel()
        {
            ClickCommand = new Command(arg => ClickMethod());
            ClickUpdateCommand = new Command(arg => ClickUpdate());
            DataUpdate();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start(); 
        }

        #endregion


        #region Properties

        /// <summary>
        /// Get or set Processes.
        /// </summary>

        private Process[] processlist = null;
        private ObservableCollection<ProcessModel> _collection = null;

        public ObservableCollection<ProcessModel> Processes
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new ObservableCollection<ProcessModel>();
                }
                return _collection;
            }
            set { _collection = value; }
        }


        private ObservableCollection<string> main_collection = null;

        public ObservableCollection<string> MainProcesses
        {
            get
            {
                if (main_collection == null)
                {
                    main_collection = new ObservableCollection<string>();
                }
                return main_collection;
            }
            set { main_collection = value; }
        }


        #endregion


        #region Commands

        /// <summary>
        /// Get or set ClickCommand.
        /// </summary>
        public ICommand ClickCommand { get; set; }
        public ICommand ClickUpdateCommand { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// Click method.
        /// </summary>
        private void ClickMethod()
        {
            string s = DateTime.Now.ToString("dd MMMM yyyy HH.mm");
            MessageBox.Show(s);
            Process[] processlist = Process.GetProcesses();
            StreamWriter sw = new StreamWriter(s + ".txt", true, Encoding.UTF8);
            for (int j = 0; j < processlist.Length; j++)
            {
                sw.WriteLine("Process: {0}, ID: {1}, count of threads: {2} ", processlist[j].ProcessName, processlist[j].Id, processlist[j].Threads.Count);
            }
            sw.Close();
        }

        private void ClickUpdate()
        {
            Processes.Clear();
            MainProcesses.Clear();
            DataUpdate();
        }

        private void DataUpdate() 
        {
            processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                Processes.Add(new ProcessModel { ProcessName = theprocess.ProcessName, ProcessId = theprocess.Id, ThreadsCount = theprocess.Threads.Count });
            }

            foreach (Process theprocess in processlist)
            {
                if (theprocess.MainWindowTitle.ToString() != "")
                {
                    MainProcesses.Add(theprocess.MainWindowTitle);
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ClickUpdate();
        } 

        #endregion

    }
}
