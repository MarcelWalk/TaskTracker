using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskTracker.Objects;
using TaskTracker.ViewModels;

namespace TaskTracker.Components
{
    public class TrackedTimesListEntry : UserControl, INotifyPropertyChanged
    {
        private string title;
        private string trackedTime;

        public string Title { get => title; set { title = value; OnPropertyChanged(); } }
        public string TrackedTime { get => trackedTime; set { trackedTime = value; OnPropertyChanged(); } }

        public TrackedTimesListEntry()
        {
            InitializeComponent();

            Title = "Empty";
            TrackedTime = "Empty";

            this.DataContext = this;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
