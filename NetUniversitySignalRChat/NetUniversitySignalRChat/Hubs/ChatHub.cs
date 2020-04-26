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
        public static Dictionary<string, string> lstUsuarios { get; set; } = new Dictionary<string, string>();

        public async Task EnviarMensaje(Mensaje mensaje)
        {
            if ( !string.IsNullOrEmpty(mensaje.Contenido))
            {
                await Clients.All.RecibirMensaje(mensaje);
            }
            else if(!string.IsNullOrEmpty(mensaje.Usuario))
            {   
                lstUsuarios.Add(Context.ConnectionId, mensaje.Usuario);
                await Clients.AllExcept(Context.ConnectionId).RecibirMensaje(new Mensaje() { Usuario = mensaje.Usuario, Contenido = "Se ha conectado!" });
            }
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).RecibirMensaje(new Mensaje() { Usuario = "Host", Contenido = "Hola Bienvenido al Chat" });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.AllExcept(Context.ConnectionId).RecibirMensaje(new Mensaje() { Usuario = "Host", Contenido = $"{lstUsuarios[Context.ConnectionId]} ha salido del chat" });

            lstUsuarios.Remove(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
