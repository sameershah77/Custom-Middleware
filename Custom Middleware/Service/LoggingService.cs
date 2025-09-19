namespace Custom_Middleware.Service
{
    public class LoggingService : ILoggingService
    {
        private static readonly List<string> sequence = new() { "GET", "POST", "PUT", "DELETE" };
        private static int index = 0;

        public bool CheckSequence(string method)
        {
            if (method == sequence[index])
            {
                index++;
                if (index == sequence.Count)
                {
                    index = 0;
                }
                return true; 
            }
            else
            {
                return false;
            }
        }
    }
}
