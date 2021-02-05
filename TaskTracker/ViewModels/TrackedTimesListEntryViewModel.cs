using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.ViewModels
{
    class TrackedTimesListEntryViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string TrackedTime { get; set; }

        public TrackedTimesListEntryViewModel()
        {

        }
    }
}
