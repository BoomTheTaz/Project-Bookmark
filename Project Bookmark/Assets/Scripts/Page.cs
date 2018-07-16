using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page {

	const int MAX_OPTIONS = 6;
    
	public string Header { get; protected set; }
	public string Body{ get; protected set; }
    
	public string[] OptionTexts { get; protected set; }
	public PageReference[] OptionPageReference { get; protected set; }

    public Page()
	{
		OptionTexts = new string[MAX_OPTIONS];
		OptionPageReference = new PageReference[MAX_OPTIONS];
	}

    public void SetHeader(string s)
	{
		Header = s;
	}

    public void SetBody(string s)
	{
		Body = s;
	}

    public void SetOptionText(int o, string s)
	{
		OptionTexts[o] = s;
	}

	public void SetOptionPageLink(int o, PageReference p)
	{
		OptionPageReference[o] = p;
	}

	public PageReference GetPageReference(int o)
	{
		return OptionPageReference[o];
	}

}
