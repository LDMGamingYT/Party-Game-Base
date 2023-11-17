using System;
using UnityEngine;

[Serializable]
public class HttpResponseData {
	public string message;

	public HttpResponseData(string message) {
		this.message = message;
	}

	public string ToJson() {
		return JsonUtility.ToJson(this);
	}
}