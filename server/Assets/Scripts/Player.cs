using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Player : MonoBehaviour {
    public new string name;
    public string ip;

    public Player(string name, string ip) {
        this.name = name;
        this.ip = ip;
    }

    public static Player FromJson(string rawJson) {
		Player jsonObject = JsonUtility.FromJson<Player>(rawJson);
		return new Player(jsonObject.name, jsonObject.ip);
	}

    public IEnumerator SendRequest() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(ip)) {
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
