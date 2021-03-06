﻿using System;
using Fleck;

public class WebsocketHandler : IDisposable
{
    private readonly WebSocketServer server;
    private IWebSocketConnection client;

    public WebsocketHandler(string location)
    {
        server = new WebSocketServer(location);
        server.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                if (client != null) socket.Close();
                client = socket;
                client.Send("_connected");
            };
        });
    }

    public void SendMessage(string message)
    {
        client.Send(message);
    }

    public void Dispose()
    {
        server?.Dispose();
    }
}