using System;
using UnityEngine;

[Serializable]
public class HttpData {
	public string ToJson() {
		return JsonUtility.ToJson(this);
	}
}

public class HttpResponse_Generic: HttpData {
	public string message;

	public HttpResponse_Generic(string message) {
		this.message = message;
	}
}

public class HttpRequest_ConnectPlayer: HttpData {
	public string name;

	public HttpRequest_ConnectPlayer(string name) {
		this.name = name;
	}
}