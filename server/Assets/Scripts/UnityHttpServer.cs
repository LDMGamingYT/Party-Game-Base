using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using TMPro;
using UnityEngine;
using System;

public class UnityHttpServer : MonoBehaviour {
    [Header("Networking")]
    public int port;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI serverAddress;
    [SerializeField] private TextMeshProUGUI connectedPlayers;
    
    
    private HttpServer server;
    private string localIp = GetLocalIPAddress();

    void Awake() {
        DontDestroyOnLoad(gameObject);
        serverAddress.SetText(GetUrl());
        if (server == null) StartServer();
    }

    private void HandleHttpRequest(HttpListenerContext context) {
        Debug.Log("Handling HTTP request");

        HttpListenerRequest request = context.Request;
        string connectionType = request.Headers["X-Connection-Type"];

        if (connectionType != null && connectionType.Equals("connect"))
            UnityMainThreadDispatcher.Instance().Enqueue(ConnectPlayer());

        HttpListenerResponse response = context.Response;
        response.AddHeader("Access-Control-Allow-Origin", "*");
        response.AddHeader("Access-Control-Allow-Headers", "*");
        
        string data = "{\"status\":\"success\"}";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);

        response.ContentType = "application/json";
        response.ContentLength64 = buffer.Length;

        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }

    private IEnumerator ConnectPlayer() {
        connectedPlayers.SetText((int.Parse(connectedPlayers.text) + 1).ToString());
        yield return null;
    }

    public void StartServer() {
        server = new HttpServer(port, HandleHttpRequest);
        Debug.Log($"Server started on port {port}");
    }

    public string GetUrl() {
        return $"http://{localIp}:{port}";
    }

    public static string GetLocalIPAddress() {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    void OnApplicationQuit() {
        server.Stop();
    }
}
