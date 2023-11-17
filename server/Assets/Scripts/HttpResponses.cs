using UnityEngine;

public class HttpResponses {
	public class HttpResponse {
		public string ToJson() {
			return JsonUtility.ToJson(this);
		}
	}

	public class GenericHttpResponse: HttpResponse {
		public string message { get; }

		public GenericHttpResponse(string message) {
			this.message = message;
		}
	}
}