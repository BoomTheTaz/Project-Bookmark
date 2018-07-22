using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PageReference {
    
	public string section { get; private set; }
	public int page  {get; private set; }


	public PageReference(string s, int p)
	{
		section = s;
		page = p;
	}

}
