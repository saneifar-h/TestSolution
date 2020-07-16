using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TestSolution.Shared;

namespace TestSolution.Client.Pages.ChatPage
{
    public class ChatViewModel
    {
        private ChatClient _client;
        public bool Chatting;
        public string Message;
        public List<Message> Messages = new List<Message>();
        public string NewMessage;
        public string Username = null;


        public event EventHandler StateChanged;

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