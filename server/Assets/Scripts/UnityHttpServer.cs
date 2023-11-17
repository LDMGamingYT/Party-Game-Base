using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class UnityHttpServer : MonoBehaviour {
    [Header("Networking")]
    public int port;

    [Header("Information Display")]
    [SerializeField] private TextMeshProUGUI serverAddress;
    [SerializeField] private QrCodeGenerator qrCodeGenerator;
    [SerializeField] private PlayerManager playerManager;
    
    
    private HttpServer server;
    private string localIp = GetLocalIPAddress();

    void Awake() {
        DontDestroyOnLoad(gameObject);
        serverAddress.SetText(GetUrl());
        qrCodeGenerator.Encode(GetUrl());
        if (server == null) StartServer();
    }

    private void HandleHttpRequest(HttpListenerContext context) {
        HttpListenerRequest request = context.Request;
        string connectionType = request.Headers["X-Connection-Type"];

        string _ct = connectionType != null ? $" (Type: {connectionType})": "";
        Debug.Log($"Handling HTTP {request.HttpMethod} request{_ct}");

        HttpListenerResponse response = context.Response;
        response.AddHeader("Access-Control-Allow-Origin", "*");
        response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        response.AddHeader("Access-Control-Allow-Headers", "Content-Type, X-Connection-Type");

        string requestBody = new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd();

        HttpResponse_Generic responseJson;

        switch (connectionType) {
            case "connect":
                string playerName = new HttpRequest_ConnectPlayer(requestBody).name;
                if (!playerManager.IsPlayerConnected(playerName)) {
                    UnityMainThreadDispatcher.Instance().Enqueue(ConnectPlayer(playerName, request.RemoteEndPoint.Address.ToString()));
                    responseJson = new HttpResponse_Generic("Connected!");
                } else responseJson = new HttpResponse_Generic($"There's already a player named '{playerName}'.");
                break;
            default:
                responseJson = new HttpResponse_Generic("Invalid or bad request");
                break;
        }
        
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseJson.ToJson());

        response.ContentType = "application/json";
        response.ContentLength64 = buffer.Length;

        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }

    private IEnumerator ConnectPlayer(string name, string ip) {
        playerManager.AddPlayer(name, ip);
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
