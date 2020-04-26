"use strict";

var conexion = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

conexion.on("RecibirMensaje", function (mensaje) {
    var li = document.createElement("li");
    li.textContent = mensaje.usuario + " - " + mensaje.contenido;
    document.getElementById("lstMensajes").appendChild(li);
});

conexion.start().then(function () {
    var li = document.createElement("li");
    li.textContent = "Bienvenido al Chat";
    document.getElementById("lstMensajes").appendChild(li);
}).catch(function (error) {
    console.error(error);
});

document.getElementById("btnEnviar").addEventListener("click", function (event) {
    var usuario = document.getElementById("txtUsuario").value;
    var mensaje = document.getElementById("txtMensaje").value;

    var objetoMensaje = {
        usuario: usuario,
        contenido: mensaje
    }

    conexion.invoke("EnviarMensaje", objetoMensaje).catch(function (error) {
        console.error(error);
    });

    document.getElementById("txtMensaje").value = "";

    event.preventDefault();
})
