using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;
	public Text HeaderText;
	public Text BodyText;
	public Button[] Options;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

   
	public void SetupPage(Page page)
	{
		HeaderText.text = page.Header;
		BodyText.text = page.Body;


		for (int i = 0; i < page.OptionTexts.Length; i++)
		{
			if (page.OptionTexts[i] != null)
			{
				Options[i].gameObject.SetActive(true);
				Options[i].GetComponentInChildren<Text>().text = page.OptionTexts[i];
			}
			else
			{
				Options[i].gameObject.SetActive(false);
			}
		}
	}
   
	public void SetButtonListeners(BookManager b)
	{
		
		for (int i = 0; i < Options.Length; i++)
		{
			int iLocal = i;
			Options[iLocal].onClick.AddListener(delegate
			{
				b.SelectedOption(iLocal);
			});
		}
	}
}
