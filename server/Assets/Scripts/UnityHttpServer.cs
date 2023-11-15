using UnityEngine;

public class UnityHttpServer : MonoBehaviour {
    public int port;
    
    private HttpServer server;

    void Awake() {
        DontDestroyOnLoad(gameObject);
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
