"use strict";

var conexion = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

conexion.on("RecibirMensaje", function (mensaje) {
    var li = document.createElement("li");
    li.textContent = mensaje.usuario + " - " + mensaje.contenido;
    document.getElementById("lstMensajes").appendChild(li);
});


document.getElementById("btnConectar").addEventListener("click", function (event) {

    if (conexion.state === signalR.HubConnectionState.Disconnected) {

        conexion.start().then(function () {
            var li = document.createElement("li");
            li.textContent = "Conexión Exitosa";
            document.getElementById("lstMensajes").appendChild(li);
            document.getElementById("btnConectar").value = "Desconectar";
            document.getElementById("txtUsuario").disabled = true;
            document.getElementById("btnEnviar").disabled = false;

            var usuario = document.getElementById("txtUsuario").value;
            var sala = document.getElementById("sala").value;

            var objetoMensaje = {
                usuario: usuario,
                contenido: "",
                sala: sala
            }

            conexion.invoke("EnviarMensaje", objetoMensaje).catch(function (error) {
                console.error(error);
            });

            conexion.stream("Counter").subscribe(
                {
                    next: (item) => { document.getElementById("lblDuracion").innerHTML = item },
                    complete: (item) => { document.getElementById("lblDuracion").innerHTML = "Se acabó el tiempo" },
                    error: (error) => { console.error(error) },
                });

        }).catch(function (error) {
            console.error(error);
        });
    }
    else if (conexion.state === signalR.HubConnectionState.Connected) {
        conexion.stop();

        var li = document.createElement("li");
        li.textContent = "Has salido del chat";
        document.getElementById("lstMensajes").appendChild(li);
        document.getElementById("btnConectar").value = "Conectar";
        document.getElementById("txtUsuario").disabled = false;
    }
});

document.getElementById("btnEnviar").addEventListener("click", function (event) {

    if (conexion.state !== signalR.HubConnectionState.Connected) {
        return;
    }

    var usuario = document.getElementById("txtUsuario").value;
    var mensaje = document.getElementById("txtMensaje").value;
    var sala = document.getElementById("sala").value;

    var objetoMensaje = {
        usuario: usuario,
        contenido: mensaje,
        sala:sala
    }

    conexion.invoke("EnviarMensaje", objetoMensaje).catch(function (error) {
        console.error(error);
    });

    document.getElementById("txtMensaje").value = "";

    event.preventDefault();
})
