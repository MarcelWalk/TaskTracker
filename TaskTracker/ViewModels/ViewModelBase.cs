using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskTracker.Objects;

namespace TaskTracker.ViewModels
{
    public class ViewModelBase : ReactiveObject, INotifyPropertyChanged
    {
        protected Dictionary<string, List<string>> DependencyMap;
        public ViewModelBase(){}

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
