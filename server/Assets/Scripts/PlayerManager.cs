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
	
	public bool AddPlayer(string name) {
		if (players.Contains(name)) return false;
		players.Add(name);
		playerList.SetText(playerList.text + "\n" + name);
		connectedPlayers.SetText((int.Parse(connectedPlayers.text) + 1).ToString());
		return true;
	}
}
