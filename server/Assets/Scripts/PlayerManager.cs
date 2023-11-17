using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	private TextMeshProUGUI playerList;

	void Awake() {
		playerList = GetComponent<TextMeshProUGUI>();
	}
	
	public void AddPlayerToList(string name) {
		playerList.SetText(playerList.text + "\n" + name);
	}
}
