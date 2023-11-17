using System;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class HttpResponse {
	[NonSerialized] private HttpListenerResponse response;
	public bool ok = true;

	public HttpResponse(HttpListenerResponse response) {
		this.response = response;
		response.AddHeader("Access-Control-Allow-Origin", "*");
        response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        response.AddHeader("Access-Control-Allow-Headers", "Content-Type, X-Connection-Type");
		response.ContentType = "application/json";
	}

	public void WriteAndSend() {
		byte[] buffer = System.Text.Encoding.UTF8.GetBytes(ToJson());
		response.ContentLength64 = buffer.Length;

		Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
	}

	public string ToJson() {
		return JsonUtility.ToJson(this);
	}
}

public class HttpResponse_Generic: HttpResponse {
	public string message;

	public HttpResponse_Generic(HttpListenerResponse response): base(response) {}
}

[Serializable]
public class HttpRequest_ConnectPlayer {
	public string name;

	public HttpRequest_ConnectPlayer(string json) {
		HttpRequest_ConnectPlayer content = (HttpRequest_ConnectPlayer)JsonUtility.FromJson(json, typeof(HttpRequest_ConnectPlayer));
		name = content.name;
	}
}