using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    
	public GameObject CardFront;
	public GameObject CardBack;
	public float speed;

	public float ATK { get; protected set; }
	public float DEF { get; protected set; }
	public int AP { get; protected set; }
    
	public static int Width;
	public static int Height;


	bool isFront = true;

	private void Start()
	{
		if (Width == 0)
		{
			Width = Mathf.RoundToInt(GetComponent<RectTransform>().rect.width);
			Height = Mathf.RoundToInt(GetComponent<RectTransform>().rect.height);
		}
	}

	// Update is called once per frame
	void Update () {

		//RectTransform rt = GetComponent<RectTransform>();

		//rt.Rotate(Vector3.up * speed);

		//int mod = Mathf.RoundToInt(rt.rotation.eulerAngles.y+180)%360;
        
		//if ( mod >= 90 && mod <= 270 && isFront == false)
		//{
		//	Debug.Log("Flipping");
		//	CardFront.SetActive(true);
		//	CardBack.SetActive(false);
		//	isFront = true;
		//}
		//else if ((mod < 90 || mod > 270) && isFront == true)
   //     {
			//Debug.Log("FlippingBack");
        //    CardFront.SetActive(false);
        //    CardBack.SetActive(true);
        //    isFront = false;
        //}

	}

    public float GetWidth()
	{
		return GetComponent<RectTransform>().rect.width;
	}

    public void SetStats(float a, float d, int ap)
	{
		ATK = a;
		DEF = d;
		AP = ap;
	}

    public void Flip()
	{
		isFront = !isFront;

		if (isFront == true)
		{
			CardFront.SetActive(true);
			CardBack.SetActive(false);
		}
		else if (isFront == false)
        {
            CardFront.SetActive(false);
            CardBack.SetActive(true);
        }
	}
}
