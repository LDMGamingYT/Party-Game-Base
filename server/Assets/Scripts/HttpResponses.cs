using UnityEngine;

public class HttpResponseData {
	public string ToJson() {
		return JsonUtility.ToJson(this);
	}
}

public class GenericHttpResponseData: HttpResponseData {
	public string message { get; }

	public GenericHttpResponseData(string message) {
		this.message = message;
	}
}