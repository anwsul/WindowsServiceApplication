using System.ServiceProcess;

namespace WindowsServiceApplication
{
    internal class Program
    {
        static void Main()
        {
            ServiceBase.Run(new Service1());
        }
    }
}
