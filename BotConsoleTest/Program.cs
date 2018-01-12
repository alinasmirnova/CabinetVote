using System;
using System.IO;
using Bot;

namespace BotConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bot = new CabinetVoteBot(File.ReadAllText("token.txt")))
            {
                bot.OnMessageRecieved += (sender, s) => Console.WriteLine($"User: {s}");
                
                Console.WriteLine($"Hello World! I\'m a {bot.FirstName}");
                Console.WriteLine("Press any key to exit");
                
                bot.Start();
                
                Console.ReadLine();
            }
        }
    }
}