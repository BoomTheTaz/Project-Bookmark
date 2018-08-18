using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionButton : MonoBehaviour {

	public UnityAction ActionOnClick;
	public Text Label;
 

	// Use this for initialization
	void Start () {
		Button button = GetComponent<Button>();

		GetComponent<Button>().onClick.AddListener(Click);
		//ActionOnClick = () => { Here(3); };

	}

    void Here(int i)
	{
		Debug.Log("Here " + i.ToString());
	}

	void Click()
	{
		ActionOnClick();
	}

    public void SetText(string s)
	{
		Label.text = s;
	}
}
