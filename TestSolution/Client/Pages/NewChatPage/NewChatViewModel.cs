using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestSolution.Client.Pages.ChatPage;
using TestSolution.Shared;

namespace TestSolution.Client.Pages.NewChatPage
{
    public class NewChatViewModel
    {
        private ChatClient _client;
        public bool Chatting;

        public List<string> CustomSymbols = new List<string>
        {
            "⺀", "⽥", "⽨", "⽣", "㣕"
        };

        public bool IsConnected;
        public string Message;
        public List<Message> Messages = new List<Message>();
        public string NewMessage;
        public string Username = null;


        public event EventHandler StateChanged;

        public void AddFiles(List<string> imgUris)
        {
            foreach (var imgUri in imgUris) AddToMsg($"<img src='{imgUri}' alt='Image' class='resizedImg' />");

            Send();
            StateChanged?.Invoke(this, null);
        }

        private void AddToMsg(string msg)
        {
            NewMessage += msg;
        }

        public Task Send()
        {
            return SendAsync();
        }

        public async Task Chat(string url)
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                Message = "Please enter a name";
                return;
            }

            try
            {
                Messages.Clear();
                var baseUrl = url;
                _client = new ChatClient(Username, baseUrl);
                _client.MessageReceived += MessageReceived;
                IsConnected = true;
                await _client.StartAsync();
                Chatting = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var isMine = false;
            if (!string.IsNullOrWhiteSpace(e.Username))
                isMine = string.Equals(e.Username, Username, StringComparison.CurrentCultureIgnoreCase);

            var newMsg = new Message(e.Username, e.Message, isMine);
            Messages.Add(newMsg);
            StateChanged?.Invoke(this, null);
        }

        public async Task DisconnectAsync()
        {
            if (Chatting)
            {
                await _client.StopAsync();
                _client = null;
                Message = "chat ended";
                Chatting = false;
            }
        }

        public async Task SendAsync()
        {
            if (Chatting && !string.IsNullOrWhiteSpace(NewMessage))
            {
                await _client.SendAsync(NewMessage);
                NewMessage = "";
            }
        }
    }
}