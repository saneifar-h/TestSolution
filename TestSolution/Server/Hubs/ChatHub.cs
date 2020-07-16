﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TestSolution.Shared;

namespace TestSolution.Server.Hubs
{
    /// <summary>
    ///     The SignalR hub
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        ///     connectionId-to-username lookup
        /// </summary>
        /// <remarks>
        ///     Needs to be static as the chat is created dynamically a lot
        /// </remarks>
        private static readonly Dictionary<string, string> userLookup = new Dictionary<string, string>();

        /// <summary>
        ///     Send a message to all clients
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string username, string message)
        {
            await Clients.All.SendAsync(Messages.Receive, username, message);
        }

        /// <summary>
        ///     Register username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task Register(string username)
        {
            var currentId = Context.ConnectionId;
            if (!userLookup.ContainsKey(currentId))
            {
                // maintain a lookup of connectionId-to-username
                userLookup.Add(currentId, username);
                // re-use existing message for now
                await Clients.AllExcept(currentId).SendAsync(
                    Messages.Receive,
                    username, $"{username} joined the chat");
            }
        }

        /// <summary>
        ///     Log connection
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        /// <summary>
        ///     Log disconnection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            // try to get connection
            var id = Context.ConnectionId;
            if (!userLookup.TryGetValue(id, out var username))
                username = "[unknown]";

            userLookup.Remove(id);
            await Clients.AllExcept(Context.ConnectionId).SendAsync(
                Messages.Receive,
                username, $"{username} has left the chat");
            await base.OnDisconnectedAsync(e);
        }
    }
}