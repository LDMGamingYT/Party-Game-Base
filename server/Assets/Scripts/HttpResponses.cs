using UnityEngine;

public class HttpResponseData {
	public string message { get; }

	public HttpResponseData(string message) {
		this.message = message;
	}

	public string ToJson() {
		return JsonUtility.ToJson(this);
	}
}