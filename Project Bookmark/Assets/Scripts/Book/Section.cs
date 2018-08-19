using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section {

	public delegate Page PageDelegate();
	protected int NumPages;

    // Store pages
	//protected Dictionary<int, PageDelegate> Pages;

	protected string SectionName;

 //   public Page GetPage(int i)
	//{
	//	if (Pages.ContainsKey(i) == true)
	//		return Pages[i].Invoke();
	//	else
	//	{
	//		Debug.LogError("Invalid index " + i.ToString() + " in Section " + SectionName + ".");
	//		return null;
	//	}
	//}
    
	public virtual Page GetPage(int i)
	{
		return null;
	}

 //   public void SetupDictionary()
	//{
	//	Pages = new Dictionary<int, PageDelegate>();

 //       for (int i = 0; i < NumPages; i++)
 //       {
 //           Switches(i);
 //       }
	//}

	//protected virtual void Switches(int i)
	//{
		
	//}

    public string GetSectionName()
	{
		return SectionName;
	}

}
