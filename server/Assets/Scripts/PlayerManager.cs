using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI connectedPlayers;
	private TextMeshProUGUI playerList;
	private List<string> players = new List<string>();

	void Awake() {
		playerList = GetComponent<TextMeshProUGUI>();
	}

	public bool IsPlayerConnected(string name) {
		return players.Contains(name);
	}
	
	public bool AddPlayer(string name) {
		players.Add(name);
		playerList.SetText(playerList.text + "\n" + name);
		connectedPlayers.SetText((int.Parse(connectedPlayers.text) + 1).ToString());
		return true;
	}
}
