using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class TimerFactory : IComponentFactory
    {
        public string ComponentName
        {
            get { return "Timer"; }
        }

        public string Description
        {
            get { return "Displays the current run time."; }
        }

        public ComponentCategory Category
        {
            get { return ComponentCategory.Timer; }
        }

        public IComponent Create(LiveSplitState state)
        {
           return new Timer();
        }

        public string UpdateName
        {
            get { return ComponentName; }
        }

        public string XMLURL
        {
#if RELEASE_CANDIDATE
            get { return "http://livesplit.org/update_rc_sdhjdop/Components/update.LiveSplit.Timer.xml"; }
#else
            get { return "http://livesplit.org/update/Components/update.LiveSplit.Timer.xml"; }
#endif
        }

        public string UpdateURL
        {
#if RELEASE_CANDIDATE
            get { return "http://livesplit.org/update_rc_sdhjdop/"; }
#else
            get { return "http://livesplit.org/update/"; }
#endif
        }

        public Version Version
        {
            get { return Version.Parse("1.3.10"); }
        }        
    }
}
