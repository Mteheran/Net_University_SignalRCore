using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace NetUniversitySignalRChatConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(2000);


            Console.WriteLine("Escoga la sala 1=sala1, 2=sala2, 3=sala3");
            var sala = Console.ReadLine();

            switch (sala)
            {
                case "1":
                    sala = "Sala1";
                    break;
                case "2":
                    sala = "Sala2";
                    break;
                case "3":
                    sala = "Sala3";
                    break;
                default:
                    break;
            }

            var conexion = new HubConnectionBuilder().WithUrl("http://localhost:50380/chatHub").Build();

            conexion.On<Mensaje>("RecibirMensaje", (mensaje) =>
            {
                Console.WriteLine($"{mensaje.Usuario} - {mensaje.Contenido}");
            });

            conexion.StartAsync().Wait();

            conexion.InvokeAsync("EnviarMensaje", new Mensaje() { Usuario = "Consola", Contenido = "", Sala = sala });

            while (true)
            {
                var contenidoMensaje = Console.ReadLine();

                conexion.InvokeAsync("EnviarMensaje", new Mensaje() { Usuario = "Consola", Contenido = contenidoMensaje, Sala = sala });
            }
        }
    }
}
