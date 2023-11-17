using System;
using UnityEngine;

[Serializable]
public class HttpResponse {
	public bool ok;

	public HttpResponse(bool ok) {
		this.ok = ok;
	}

	public string ToJson() {
		return JsonUtility.ToJson(this);
	}
}

public class HttpResponse_Generic: HttpResponse {
	public string message;

	public HttpResponse_Generic(string message, bool ok): base(ok) {
		this.message = message;
	}
}

[Serializable]
public class HttpRequest_ConnectPlayer {
	public string name;

	public HttpRequest_ConnectPlayer(string json) {
		HttpRequest_ConnectPlayer content = (HttpRequest_ConnectPlayer)JsonUtility.FromJson(json, typeof(HttpRequest_ConnectPlayer));
		name = content.name;
	}
}