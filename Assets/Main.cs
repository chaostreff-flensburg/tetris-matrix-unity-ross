using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public PixelGrid pixelGrid;

    public async void SendData()
    {
        var socket = new ClientWebSocket();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;
        await socket.ConnectAsync(new Uri("ws://10.0.39.227:3000"), token);
        int index = 0;
        byte[] result = new byte[1152];
        
        foreach (var pixel in pixelGrid.pixels)
        {
            var color = pixel.GetComponent<PixelInfo>().GetColor();
            Color32 color32 = color;
            result[index] = Convert.ToByte(color32.r);
            result[index+1] = Convert.ToByte(color32.g);
            result[index+2] = Convert.ToByte(color32.b);

            index += 3;
        }

        await socket.SendAsync(new ArraySegment<byte>(result), WebSocketMessageType.Binary, true, token);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, token);
    }
}