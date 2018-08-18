using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;
	public Text HeaderText;
	public Text BodyText;
	public Transform OptionContainer;

	OptionButton[] Options;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	private void Start()
	{
		Options = new OptionButton[OptionContainer.childCount];
		for (int i = 0; i < OptionContainer.childCount; i++)
		{
			Options[i] = OptionContainer.GetChild(i).GetComponent<OptionButton>();
		}
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
				Options[i].SetText(page.OptionTexts[i]);
				Options[i].ActionOnClick = page.ButtonActions[i];
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
			//Options[iLocal].onClick.AddListener(delegate
			//{
			//	b.SelectedOption(iLocal);
			//});
		}
	}
}
