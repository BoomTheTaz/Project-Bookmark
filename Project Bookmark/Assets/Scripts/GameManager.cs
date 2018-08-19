using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	BookManager bookManager;

	bool inBook = true;

	public static GameManager instance;

	PageReference VictoryPage;
	PageReference DefeatPage;

	public PageReference PageToLoad { get; private set; }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
			Destroy(this);
	}

	// Use this for initialization
	void Start () {
		//PlayerData player = new PlayerData();

		// Test Stats
		//Debug.Log("Strength: " + player.GetStat(Stats.Strength).ToString());
		//Debug.Log("Dexterity: " + player.GetStat(Stats.Dexterity).ToString());
		//Debug.Log("Constitution: " + player.GetStat(Stats.Constitution).ToString());
		//Debug.Log("Intelligence: " + player.GetStat(Stats.Intelligence).ToString());
		//Debug.Log("Wisdom: " + player.GetStat(Stats.Wisdom).ToString());
		//Debug.Log("Charisma: " + player.GetStat(Stats.Charisma).ToString());
		PageToLoad = new PageReference("Prologue", 0);

		if (inBook == true)
		{

			BookManager.instance.SetupPageReference(new PageReference("Prologue", 0));

			//UIManager.instance.SetButtonListeners(bookManager);
		}


	}

	public void SetPostCombatPages(PageReference v, PageReference d)
	{
		VictoryPage = v;
		DefeatPage = d;

		Debug.Log("Victory page: " + VictoryPage.page.ToString() + "\nDefeat Page: " + DefeatPage.page.ToString());
	}
	
    public void CombatVictory()
	{
		PageToLoad = VictoryPage;
		SceneManager.LoadScene("Book");
	}

    public void CombatDefeat()
	{
		PageToLoad = DefeatPage;
		SceneManager.LoadScene("Book");
  
	}
}
