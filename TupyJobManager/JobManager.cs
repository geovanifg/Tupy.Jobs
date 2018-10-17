using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tupy.Jobs
{
    public static class JobManager
    {
        private static bool continueexecute;
        public static bool Running { get; private set; }
        public static List<Job> Jobs { get; set; } = new List<Job>();

        public static void Start()
        {
            continueexecute = true;
            Task.Run(() => Run());
        }

        public static void Stop()
        {
            continueexecute = false;
        }

        private static async Task Run()
        {
            Running = true;
            while (continueexecute)
            {
                var jobstoexecute = Jobs.Where(r => r.Schedule.CanExecute());
                foreach (var item in jobstoexecute)
                {
                    await Task.Run(() => 
                    {
                        var result = item.Execute();
                    });
                }

                // Wait for 1 minute
                Thread.Sleep(60 * 1000);
            }
            Running = false;
        }
    }
}
