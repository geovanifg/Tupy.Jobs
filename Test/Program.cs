using System;
using System.Threading;
using Tupy;
using TupyJobManager;

namespace Test
{
    class Program
    {
        static void Write(Job job)
        {
            Console.WriteLine($"{DateTime.Now} - {job.Name} - {job.Schedule.FrequencyOption} - {job.Schedule.FrequencyInterval} - {job.Schedule.LastExecution}");
        }

        static void Calc()
        {
            int a=2, b = 0;

            int result = a / b;
        }

        static void Log(ExecutionResponse status)
        {
            Console.WriteLine($"{DateTime.Now} - {status.IsSuccess} - {status.Message} - {status.Content}");
        }

        static void Main(string[] args)
        {
            var j1 = new Job()
            {
                //Code = new Action(() => { int a = 10; })

                Name = "Job 1",
                //StepAction = 
            };

            j1.StepAction = delegate ()
            {
                Write(j1);
                Calc();
            };
            j1.ReportStatus = Log;
            j1.Schedule.FrequencyOption = FrequencyOptions.Minute;
            j1.Schedule.FrequencyInterval = 2;

            var j2 = new Job()
            {
                Name = "Job 2",
                //StepAction = delegate ()
                //{
                //    Console.WriteLine("Job 2");
                //}
            };

            j2.StepAction = delegate ()
            {
                Write(j2);
            };
            j2.Schedule.FrequencyOption = FrequencyOptions.Hour;
            j2.Schedule.FrequencyInterval = 1;

            JobManager.Jobs.Add(j1);
            JobManager.Jobs.Add(j2);

            JobManager.Start();

            // Aguardar uma hora
            //Thread.Sleep(60 * 1000 * 60);

            // Aguardar um minuto
            Thread.Sleep(60 * 1000);

            JobManager.Stop();
        }
    }
}
