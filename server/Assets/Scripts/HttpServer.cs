using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

public class HttpServer {
    private int port;
    private Thread thread;
    private HttpListener listener;

    public HttpServer(int port) {
        this.port = port;
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
        UnityEngine.Debug.Log("Handling HTTP request");

        HttpListenerResponse response = context.Response;
        
        string data = "{\"status\":\"success\"}";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data);

        response.ContentType = "application/json";
        response.ContentLength64 = buffer.Length;

        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }

    public void Stop() {
        thread.Abort();
        listener.Stop();
    }
}
