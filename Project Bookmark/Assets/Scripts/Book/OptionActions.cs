using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OptionActions  {

	public static UnityAction GoToPage(PageReference pr)
	{
		UnityAction temp;

		temp = () => BookManager.instance.GoToPage(pr);
		return temp;
	}

	public static UnityAction GoToCombat(string s)
	{
		UnityAction temp = () => Debug.Log("Off to fight " + s);
		return temp;
	}

}
