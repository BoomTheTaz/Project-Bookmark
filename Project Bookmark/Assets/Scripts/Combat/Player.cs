using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int AP { get; protected set; }
	public float ATK { get; protected set; }
	public float DEF { get; protected set; }

	private void Awake()
	{
		AP = 3;
		ATK = 1;
		DEF = 1;
	}
}
