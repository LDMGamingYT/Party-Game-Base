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
}
