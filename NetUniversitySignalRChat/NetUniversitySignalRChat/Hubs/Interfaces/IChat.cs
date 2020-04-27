using NetUniversitySignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetUniversitySignalRChat.Hubs.Interfaces
{
    public interface IChat
    {
        Task EnviarMensaje(Mensaje mensaje);
        Task RecibirMensaje(Mensaje mensaje);

        Task Counter();
    }
}
