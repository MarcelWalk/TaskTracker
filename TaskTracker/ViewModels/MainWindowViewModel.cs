using System;
using System.ComponentModel;
using TaskTracker.Models;

namespace TaskTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Timer _timer;

        public string TaskName { get; set; }

        private Timer Timer { get { return _timer; } set { _timer = value; OnPropertyChanged(); } }

        public MainWindowViewModel()
        {
            Timer = new Timer();
        }

        protected void BtnAddClicked()
        {
            Timer.ToggleTimer(TaskName);
        }
    }
}
