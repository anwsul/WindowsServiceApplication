using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace WindowsServiceApplication
{
    internal class Service1 : ServiceBase
    {
        private readonly EventLog _eventLog;
        private Timer _timer;
        private int _counter;

        public Service1()
        {
            ServiceName = "Service1";
            string logName = "Application";

            if (!EventLog.SourceExists(ServiceName))
            {
                EventLog.CreateEventSource(ServiceName, logName);
            }

            _eventLog = new EventLog(logName)
            {
                Source = ServiceName
            };
        }

        private void UpdateCount(object _)
        {
            _eventLog.WriteEntry($"Count: {_counter}");
            _counter++;
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(
                callback: UpdateCount,
                state: _counter,
                dueTime: 0,
                period: 5000
            );
        }

        protected override void OnStop()
        {
            _timer.Dispose();
        }
    }
}
