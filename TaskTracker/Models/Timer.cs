using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TaskTracker.Components;
using TaskTracker.Objects;
using TaskTracker.ViewModels;

namespace TaskTracker.Models
{
    class Timer : INotifyPropertyChanged
    {
        private bool isRunning;
        private List<TrackingEntry> timeEntries;
        private DateTime startTime;
        private System.Timers.Timer refreshTimer;
        private string trackedTimeAsString;
        private string totalTrackedTimeString;
        private ObservableCollection<TrackedTimesListEntry> listEntries;

        public ObservableCollection<TrackedTimesListEntry> ListEntries { get => listEntries; set { listEntries = value; OnPropertyChanged(); } }

        public List<TrackingEntry> TimeEntries
        {
            get => timeEntries;
            set
            {
                timeEntries = value;
                UpdateListEntries();
                RefreshTotalTrackedTime();
                OnPropertyChanged();
            }
        }

        private void UpdateListEntries()
        {
            if (ListEntries != null)
                ListEntries.Clear();
            foreach (var item in TimeEntries)
            {

                var listEntry = new TrackedTimesListEntry();
                //var entryVm = (TrackedTimesListEntryViewModel)listEntry.DataContext;

                listEntry.Title = item.Title;
                listEntry.TrackedTime = item.TrackedTime.ToString(@"hh\:mm\:ss");

                ListEntries.Add(listEntry);
            }
        }

        private void RefreshTotalTrackedTime()
        {
            TotalTrackedTimeString = TimeEntries.Count == 0 ? "00:00:00" : TimeEntries.Select(x => x.TrackedTime).Aggregate((a, b) => a.Add(b)).ToString(@"hh\:mm\:ss");
        }

        public bool IsRunning { get => isRunning; set { isRunning = value; OnPropertyChanged(); } }
        public string TotalTrackedTimeString
        {
            get => totalTrackedTimeString;
            set { totalTrackedTimeString = value; OnPropertyChanged(); }
        }
        public string TrackedTimeString { get => trackedTimeAsString; set { trackedTimeAsString = value; OnPropertyChanged(); } }

        public Timer()
        {
            TimeEntries = new List<TrackingEntry>();
            ListEntries = new ObservableCollection<TrackedTimesListEntry>();

            refreshTimer = new System.Timers.Timer();
            refreshTimer.Interval = 500;
            refreshTimer.Elapsed += RefreshTimer_Elapsed;
        }

        private void RefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TrackedTimeString = CalculateDiff().ToString(@"hh\:mm\:ss");
        }

        public void ToggleTimer(string taskName)
        {
            if (IsRunning)
            {
                StopTracking(taskName);
            }
            else
            {
                StartTracking();
            }

            IsRunning = !IsRunning;
        }

        private void StartTracking()
        {
            startTime = DateTime.Now;
            refreshTimer.Start();
        }

        private void StopTracking(string taskName)
        {
            refreshTimer.Stop();

            if (TimeEntries.Any(x => x.Title.ToUpper() == taskName.ToUpper()))
            {
                //Task already known
                var entry = TimeEntries.Where(x => x.Title.ToUpper() == taskName.ToUpper()).FirstOrDefault();
                TimeSpan totalTrackedTime = CalculateDiff();
                entry.TrackedTime = entry.TrackedTime.Add(totalTrackedTime);
            }
            else
            {
                TimeEntries.Add(new TrackingEntry
                {
                    Title = taskName,
                    TrackedTime = CalculateDiff()
                });
            }

            TimeEntries = TimeEntries; //Workaround to trigger propertychanged
        }

        private TimeSpan CalculateDiff()
        {
            return DateTime.Now.Subtract(startTime);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}