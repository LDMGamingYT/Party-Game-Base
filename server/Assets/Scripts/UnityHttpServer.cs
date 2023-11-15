using TMPro;
using UnityEngine;

public class UnityHttpServer : MonoBehaviour {
    public int port;
    public TextMeshProUGUI serverAddress;
    
    private HttpServer server;

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
        return $"http://localhost:{port}";
    }

    void OnApplicationQuit() {
        server.Stop();
    }
}
