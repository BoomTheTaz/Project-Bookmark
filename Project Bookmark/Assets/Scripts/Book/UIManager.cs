using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

    public Transform BookCanvas;
	

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}
    
    public void SetupPage(Page p)
    {
        BookCanvas.GetChild(0).GetComponent<PageComponent>().SetupPage(p);
        BookCanvas.GetChild(1).GetComponent<PageComponent>().FlipPage();
    }

}
