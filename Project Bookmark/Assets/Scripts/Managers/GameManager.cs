using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	BookManager bookManager;

	bool inBook = false;

	public static GameManager instance;

	PageReference VictoryPage;
	PageReference DefeatPage;

	public PageReference PageToLoad { get; private set; }

    public CharacterData playerData { get; private set; }
    public CharacterData enemyData { get; private set; }

    private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);

            // ==============================================
            // =============== TEMPORARY, DELETE ASAP =============
            // ===============================================
            playerData = new PlayerData(2, 3, 1, 100, new CardStats[]
            {
                new CardStats(CardType.ATK_Phys, 1,1,1,"Thrust"),
                new CardStats(CardType.ATK_Phys, 1,1,1,"Thrust"),
                new CardStats(CardType.ATK_Phys, 2,1,2,"Slash"),
                new CardStats(CardType.ATK_Phys, 2,1,2,"Break Defense", new CardEffect[]{ new CardEffect(EffectTypes.DrawCard, 1) }),
                new CardStats(CardType.ATK_Phys, 1,1,2,"Stunning Strike", new CardEffect[]{ new CardEffect(EffectTypes.TakeAP, 1) }),
                new CardStats(CardType.DEF_Phys, 1,1,1,"Brace"),
                new CardStats(CardType.DEF_Phys, 1,1,1,"Brace"),
                new CardStats(CardType.DEF_Phys, 1,2,2,"Deflect"),
                new CardStats(CardType.DEF_Phys, 1,1,2,"Counter Attack", new CardEffect[]{ new CardEffect(EffectTypes.GainAP, 1) }),
                new CardStats(CardType.DEF_Phys, 1,2,2,"Defensive Stance", new CardEffect[]{ new CardEffect(EffectTypes.DiscardCard, 1) }),
                new CardStats(CardType.DEF_Mag, 1,1,2,"Magic Shield"),
                new CardStats(CardType.ATK_Mag, 1,1,3,"Fireball")
            });

            enemyData = new RatsData();
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
