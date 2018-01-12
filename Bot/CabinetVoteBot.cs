using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Bot
{
    public class CabinetVoteBot : IDisposable
    {
        private TelegramBotClient telegramBotClient;
        
        public event EventHandler<string> OnMessageRecieved;
        public string FirstName { get; }

        public CabinetVoteBot(string token)
        {
            telegramBotClient = new TelegramBotClient(token);
            FirstName = telegramBotClient.GetMeAsync().Result.FirstName;
            
            telegramBotClient.OnMessage += OnMessageRecievedInternal;
        }

        private bool started = false;
        public void Start()
        {
            telegramBotClient.StartReceiving();
            started = true;
        }

        private void OnMessageRecievedInternal(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if(message == null || message.Type != MessageType.Text)
                return;

            var parts = message.Text.Split(' ');
            var command = parts.First();
            var args = parts.Skip(1);

            var onMessageRecieved = OnMessageRecieved;
            switch (command)
            {
                    case "/add":
                        var content = string.Join(" ", args);
                        if(!string.IsNullOrWhiteSpace(content))
                            onMessageRecieved?.Invoke(this, content);
                        break;
                    default:
                        onMessageRecieved?.Invoke(this, "unknown");
                        break;
            }
        }

        public void Dispose()
        {
            if(started)
                telegramBotClient.StopReceiving();
        }
    }
}