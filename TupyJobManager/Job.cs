using System;
using Tupy;

namespace TupyJobManager
{
    public class Job
    {
        public string Name { get; set; }
        public Schedule Schedule { get; set; }
        public Action StepAction { get; set; }
        public Action<ExecutionStatus> ReportStatus { get; set; }

        public Job()
        {
            Schedule = new Schedule()
            {
                FrequencyOption = FrequencyOptions.Hour,
                FrequencyInterval = 1
            };
        }

        public Job(FrequencyOptions frequencyOption, int frequencyInterval) : this()
        {
            Schedule.FrequencyOption = frequencyOption;
            Schedule.FrequencyInterval = frequencyInterval;
        }

        public ExecutionStatus Execute()
        {
            var result = new ExecutionStatus(true);

            try
            {
                StepAction();
                Schedule.Update();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error has occured.";
                result.Data = ex.Message;
            }

            ReportStatus?.Invoke(result);

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}