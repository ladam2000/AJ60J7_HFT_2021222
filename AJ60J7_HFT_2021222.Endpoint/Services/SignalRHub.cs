﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace AJ60J7_HFT_2021222.Endpoint.Services
{
    public class SignalRHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync(" Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Caller.SendAsync(" Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

