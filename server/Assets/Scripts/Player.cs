using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Player {
    public string name;
    public string ip;

    public Player(string name, string ip) {
        this.name = name;
        this.ip = ip;
    }

    public static Player FromJson(string rawJson) {
        Dictionary<string, object> jsonDict = JsonUtility.FromJson<Dictionary<string, object>>(rawJson);

        string name = jsonDict.ContainsKey("name") ? jsonDict["name"].ToString() : null;
        string ip = jsonDict.ContainsKey("ip") ? jsonDict["ip"].ToString() : null;

        return new Player(name, ip);
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
