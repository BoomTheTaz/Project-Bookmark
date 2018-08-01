using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CardType { ATK_Phys, ATK_Mag, DEF_Phys, DEF_Mag}

public class Card : MonoBehaviour {
    
	public GameObject CardFront;
	public GameObject CardBack;
	public float Speed { get; protected set; }

	public int ATK { get; protected set; }
	public int DEF { get; protected set; }
	public int AP { get; protected set; }
    
	public static int Width;
	public static int Height;

	public Text ATKText;
	public Text DEFText;
	public Text APText;
	public Text CardName;
	public Image CardTypeIMG;

	public CardType Type { get; protected set; }
	public string Name { get; protected set; }



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

	public void SetupCard(CardStats c)
	{
		Type = c.Type;
		Name = c.Name;
		ATK = c.ATK;
		DEF = c.DEF;
		AP = c.AP;

		UpdateVisuals();
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

    void UpdateVisuals()
	{
		ATKText.text = ATK.ToString();
		DEFText.text = DEF.ToString();
		CardName.text = Name;
		APText.text = AP.ToString();

        switch (Type)
		{

			case CardType.ATK_Mag:
				CardTypeIMG.color = Color.magenta;
				break;
			
			case CardType.ATK_Phys:
				CardTypeIMG.color = Color.red;
                break;
			case CardType.DEF_Mag:
				CardTypeIMG.color = Color.cyan;
                break;
			case CardType.DEF_Phys:
				CardTypeIMG.color = Color.blue;
                break;


			default:
				CardTypeIMG.color = Color.black;
				Debug.LogError("Invalid card type.");
				break;
		}
        
	}

    public void SetEnemyBack()
	{
		CardBack.GetComponent<Image>().sprite = Resources.Load<Sprite>("EnemyBack");
	}
}
