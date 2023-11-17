using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityHttpServer : MonoBehaviour {
    [Header("Networking")]
    public int port;

    [Header("Information Display")]
    [SerializeField] private TextMeshProUGUI serverAddress;
    [SerializeField] private TextMeshProUGUI connectedPlayers;
    [SerializeField] private QrCodeGenerator qrCodeGenerator;
    
    
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
        response.AddHeader("Access-Control-Allow-Headers", "*");

        Dictionary<string, object> responseJson;

        switch (connectionType) {
            case "connect":
                UnityMainThreadDispatcher.Instance().Enqueue(ConnectPlayer());
                responseJson = new Dictionary<string, object> {
                    {"message", "Connected!"},
                };
                break;
            default:
                response.StatusCode = 400;
                response.Close();
                return;
        }
        
        
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(responseJson));

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
