using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskTracker.Objects
{
    public class TrackingEntry
    {
        public string Title
        {
            get;
            set;
        }

        public TimeSpan TrackedTime
        {
            get;
            set;
        }
    }
}