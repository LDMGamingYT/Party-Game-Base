using System;
using System.Net;
using System.Threading;

public class HttpServer {
    private int port;
    private Thread thread;
    private HttpListener listener;
    private Action<HttpListenerContext> handleHttpRequest;

    public HttpServer(int port, Action<HttpListenerContext> requestHandler) {
        this.port = port;
        this.handleHttpRequest = requestHandler;
        thread = new Thread(this.Listen);
        thread.Start();
    }

    private void Listen() {
        listener = new HttpListener();
        listener.Prefixes.Add("http://*:" + port + "/");
        listener.Start();
        while (true) {
            try {
                handleHttpRequest(listener.GetContext());
            } catch (Exception e) {
                UnityEngine.Debug.LogError(e);
            }
        }
    }


    public void Stop() {
        thread.Abort();
        listener.Stop();
    }
}
