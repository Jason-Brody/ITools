using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITools
{
    interface IWorking
    {
        event OnWorkingHanlder OnWorking;
    }

    public class WorkingEventArgs:EventArgs
    {
        public int Current { get; set; }

        public int Max { get; set; }

        public bool IsProcessKnow { get; set; }
    }
}
