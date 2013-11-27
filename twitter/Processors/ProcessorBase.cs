namespace audit.twitter.Processors
{
    using System;

    public abstract class ProcessorBase : IProcessor
    {
        public abstract void Process(string jsonText);

        public void SetStartTime()
        {
            this.StartTime = DateTime.Now;
        }

        protected DateTime StartTime { get; set; }

        protected DateTime EndTime { get; set; }

        protected string TotalTime
        {
            get
            {
                if (this.EndTime == DateTime.MinValue || this.StartTime == DateTime.MinValue )
                {
                    return "Not valid";
                }


                TimeSpan dif = this.EndTime - this.StartTime;
                return dif.ToString();
            }
       
        }

        public void SetEndTime()
        {
            this.EndTime = DateTime.Now;
        }

        public abstract void Report();
    }
}