using System;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour {
    public new string name;
    public string ip;

    public Player(string name, string ip) {
        this.name = name;
        this.ip = ip;
    }

    public static Player FromJson(string rawJson) {
		Player jsonObject = (Player)JsonUtility.FromJson(rawJson, typeof(Player));
		return new Player(jsonObject.name, jsonObject.ip);
	}
}
