using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	BookManager bookManager;

	// Use this for initialization
	void Start () {
		PlayerData player = new PlayerData();

		// Test Stats
		//Debug.Log("Strength: " + player.GetStat(Stats.Strength).ToString());
		//Debug.Log("Dexterity: " + player.GetStat(Stats.Dexterity).ToString());
		//Debug.Log("Constitution: " + player.GetStat(Stats.Constitution).ToString());
		//Debug.Log("Intelligence: " + player.GetStat(Stats.Intelligence).ToString());
		//Debug.Log("Wisdom: " + player.GetStat(Stats.Wisdom).ToString());
		//Debug.Log("Charisma: " + player.GetStat(Stats.Charisma).ToString());

		bookManager = new BookManager("Prologue");
		bookManager.SetupPage(0);

		UIManager.instance.SetButtonListeners(bookManager);


	}
	

}
