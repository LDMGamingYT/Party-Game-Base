using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using TMPro;
using UnityEngine;

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

        HttpResponse_Generic response = new HttpResponse_Generic(context.Response);

        string requestBody = new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd();

        switch (connectionType) {
            case "connect":
                string playerName = Player.FromJson(requestBody).name;
                if (!playerManager.IsPlayerConnected(playerName)) {
                    UnityMainThreadDispatcher.Instance().Enqueue(ConnectPlayer(playerName, request.RemoteEndPoint.Address.ToString()));
                    response.message = "Connected!";
                } else {
                    response.message = $"There's already a player named '{playerName}'.";
                    response.ok = false;
                }

                break;
            default:
                response.message = "Invalid or bad request";
                response.ok = false;
                break;
        }
        
        response.Send();
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

    public void SendRequestToPlayer(int player) {
        StartCoroutine(playerManager.GetPlayer(player).SendRequest());
    }

    void OnApplicationQuit() {
        server.Stop();
    }
}
