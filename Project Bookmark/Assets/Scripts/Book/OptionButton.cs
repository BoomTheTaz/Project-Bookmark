using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionButton : MonoBehaviour {

	public UnityAction ActionOnClick;
	public Text Label;
	public Color SelectedColor;

	Image background;


	bool isSelected;

	// Use this for initialization
	void Start () {
		//Button button = GetComponent<Button>();
		background = GetComponent<Image>();
		GetComponent<Button>().onClick.AddListener(Click);

	}

    void Here(int i)
	{
		Debug.Log("Here " + i.ToString());
	}
    
	void Click()
	{
		if (isSelected == false)
			Select();
		else
		{
			if (ActionOnClick == null)
				Debug.LogError("HUGE PROBLEM. Not sure what to do on clicking here.");
			else
			{
				ActionOnClick();
				Deselect();
			}
		}
	}

    public void SetText(string s)
	{
		Label.text = s;
	}

    public void Select()
	{
		// TODO: Inform book manager of selected number, Deselect if other currently selected
		isSelected = true;
		background.color = SelectedColor;
	}

    public void Deselect()
	{
		isSelected = false;
		background.color = Color.white;
	}
}
