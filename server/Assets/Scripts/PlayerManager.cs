using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI connectedPlayers;
	private TextMeshProUGUI playerList;
	private List<Player> players = new List<Player>();

	void Awake() {
		playerList = GetComponent<TextMeshProUGUI>();
	}

	public bool IsPlayerConnected(string name) {
		return players.Any(player => player.name == name);
	}
	
	public bool AddPlayer(string name, string ip) {
		players.Add(new Player(name, ip));
		playerList.SetText($"{playerList.text}\n{name} ({ip})");
		connectedPlayers.SetText((int.Parse(connectedPlayers.text) + 1).ToString());
		return true;
	}

	public Player GetPlayer(int player) {
		return players[player];
	}
}
