namespace audit.twitter.Processors
{
    public interface IProcessor
    {
        void Process(string jsonText);

        void SetStartTime();

        void SetEndTime();
    }
}