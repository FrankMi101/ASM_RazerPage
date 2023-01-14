namespace ASM.Api.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            // Take the message seave to Server or file
            if (type == "Error") Console.BackgroundColor = ConsoleColor.Red;
            if (type == "Warning") Console.BackgroundColor = ConsoleColor.DarkYellow;
            if (type == "Info") Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($" Logging message: {type} : {message}");

            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
