using System;
using System.Collections;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

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

    public IEnumerator SendRequest(string url) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError("Request error: " + webRequest.error);
            } else {
                Debug.Log("Request successful");
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
        }
    }
}
