using LiveSplit.Model;
using System;

namespace LiveSplit.UI.Components
{
    public class TimerFactory : IComponentFactory
    {
        public string ComponentName => "Timer";

        public string Description => "Displays the current run time.";

        public ComponentCategory Category => ComponentCategory.Timer;

        public IComponent Create(LiveSplitState state) => new Timer();

        public string UpdateName => ComponentName;

        public string XMLURL => "http://livesplit.org/update/Components/update.LiveSplit.Timer.xml";

        public string UpdateURL => "http://livesplit.org/update/";

        public Version Version => Version.Parse("1.7.5");
    }
}
