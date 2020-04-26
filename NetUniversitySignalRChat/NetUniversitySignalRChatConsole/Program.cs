using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace NetUniversitySignalRChatConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(2000);

            var conexion = new HubConnectionBuilder().WithUrl("http://localhost:50380/chatHub").Build();

            conexion.On<Mensaje>("RecibirMensaje", (mensaje) =>
            {
                Console.WriteLine($"{mensaje.Usuario} - {mensaje.Contenido}");
            });

            conexion.StartAsync().Wait();

            while (true)
            {
                var contenidoMensaje = Console.ReadLine();

                conexion.InvokeAsync("EnviarMensaje", new Mensaje() { Usuario = "Consola", Contenido = contenidoMensaje });
            }
        }
    }
}
