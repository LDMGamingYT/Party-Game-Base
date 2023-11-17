using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI connectedPlayers;
	private TextMeshProUGUI playerList;
	private Dictionary<string, string> players = new Dictionary<string, string>();

	void Awake() {
		playerList = GetComponent<TextMeshProUGUI>();
	}

	public bool IsPlayerConnected(string name) {
		return players.ContainsKey(name);
	}
	
	public bool AddPlayer(string name, string ip) {
		players.Add(name, ip);
		playerList.SetText($"{playerList.text}\n{name} ({ip})");
		connectedPlayers.SetText((int.Parse(connectedPlayers.text) + 1).ToString());
		return true;
	}
}
