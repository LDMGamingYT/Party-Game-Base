using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

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

    public void StartServer() {
        server = new HttpServer(port);
        UnityEngine.Debug.Log($"Server started on port {port}");
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
