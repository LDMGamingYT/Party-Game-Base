using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

public class HttpServer {
    private int port;
    private string rootDir;
    private Thread thread;
    private HttpListener listener;

    public HttpServer(string rootDir, int port) {
        this.port = port;
        this.rootDir = rootDir;
        thread = new Thread(this.Listen);
        thread.Start();
    }

    private void Listen() {
        listener = new HttpListener();
        listener.Prefixes.Add("http://*:" + port + "/");
        listener.Start();
        while (true) {
            try {
                HandleHttpRequest(listener.GetContext());
            } catch (Exception e) {
                UnityEngine.Debug.LogError(e);
            }
        }
    }

    private void HandleHttpRequest(HttpListenerContext context) {
        UnityEngine.Debug.Log(context);
    }

    public void Stop() {
        thread.Abort();
        listener.Stop();
    }
}
