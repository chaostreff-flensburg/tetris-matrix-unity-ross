using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public PixelGrid pixelGrid;
    public InputField inputField;
    public Slider slider;
    public int brightness;

    public async void SendData()
    {
        var socket = new ClientWebSocket();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;
        var uriAddress = inputField.text == "" ? inputField.placeholder.GetComponent<Text>().text : inputField.text;
        await socket.ConnectAsync(new Uri("ws://" + uriAddress + ":81"), token);
        int index = 0;
        byte[] result = new byte[1152];

        foreach (var pixel in pixelGrid.pixels)
        {
            var color = pixel.GetComponent<PixelInfo>().GetColor();
            Color32 color32 = color;
            result[index] = Convert.ToByte(color32.r);
            result[index + 1] = Convert.ToByte(color32.g);
            result[index + 2] = Convert.ToByte(color32.b);

            index += 3;
        }

        await socket.SendAsync(new ArraySegment<byte>(result), WebSocketMessageType.Binary, true, token);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, token);
    }

    public async void SendBrightness()
    {
        var socket = new ClientWebSocket();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;
        var uriAddress = inputField.text == "" ? inputField.placeholder.GetComponent<Text>().text : inputField.text;
        await socket.ConnectAsync(new Uri("ws://" + uriAddress + ":81"), token);
        byte[] result = BitConverter.GetBytes(brightness);

        await socket.SendAsync(new ArraySegment<byte>(result), WebSocketMessageType.Binary, true, token);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, token);
    }

    public async void RandomFlash()
    {
        var socket = new ClientWebSocket();
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;
        var uriAddress = inputField.text == "" ? inputField.placeholder.GetComponent<Text>().text : inputField.text;
        await socket.ConnectAsync(new Uri("ws://" + uriAddress + ":81"), token);
        int index = 0;
        byte[] result = new byte[1152];

        foreach (var pixel in pixelGrid.pixels)
        {
            result[index] = Convert.ToByte(Random.Range(0, 255));
            result[index + 1] = Convert.ToByte(Random.Range(0, 255));
            result[index + 2] = Convert.ToByte(Random.Range(0, 255));

            index += 3;
        }

        await socket.SendAsync(new ArraySegment<byte>(result), WebSocketMessageType.Binary, true, token);
        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, token);
    }

    public void SetBrightness()
    {
        brightness = (int) slider.value;
    }
}