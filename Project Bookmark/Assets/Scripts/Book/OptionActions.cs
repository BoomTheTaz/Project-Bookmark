using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class OptionActions  {

	public static UnityAction GoToPage(PageReference pr)
	{
		UnityAction temp;

		temp = () => BookManager.instance.GoToPage(pr);
		return temp;
	}

	public static UnityAction GoToCombat(string s, PageReference VictoryPR, PageReference DefeatPR)
	{
		UnityAction temp = () =>
		{
			Debug.Log("Off to fight " + s);

			GameManager.instance.SetPostCombatPages(VictoryPR, DefeatPR);
			SceneManager.LoadScene("Combat");
		};
		return temp;
	}

}
