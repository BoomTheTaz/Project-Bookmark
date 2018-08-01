using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {

	delegate void OnUpdate();
	OnUpdate Controls;

	public Hand PlayerHand;

	private void Start()
	{
		Controls += CombatControls;
	}

	// Update is called once per frame
	void Update () {

		if (Controls != null)
			Controls();

	}



	void CombatControls()
	{

		if (Input.GetKeyDown(KeyCode.Space))
			PlayerHand.DrawCard();

		if (Input.GetKeyDown(KeyCode.RightArrow))
			FindObjectOfType<CombatManager>().Evaluate();

	}
}