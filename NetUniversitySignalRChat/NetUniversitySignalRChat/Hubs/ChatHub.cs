using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NetUniversitySignalRChat.Hubs.Interfaces;
using NetUniversitySignalRChat.Models;

namespace NetUniversitySignalRChat.Hubs
{
    public class ChatHub : Hub<IChat>
    {
        public async Task EnviarMensaje(Mensaje mensaje)
        {
            await Clients.All.RecibirMensaje(mensaje);
        }
    }
}
