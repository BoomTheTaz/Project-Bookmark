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

	Transform targetTransform;
	Vector3 targetPosition;
	Quaternion targetRotation;
	Vector3 targetScale;
    
	bool isFront = true;
	bool hasFlipped;
	bool isScalingToOne;
    

	float moveSpeed = 0.07f;
	float flipSpeed = 0.08f;
	float scaleSpeed = 0.15f;
	float scaleScaler = 1.5f;

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

    public void FlipInstant()
	{
		isFront = !isFront;

		if (isFront == true)
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
			CardFront.SetActive(true);
			CardBack.SetActive(false);
		}
		else if (isFront == false)
        {
			transform.rotation = Quaternion.Euler(0, 180, 0);
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
    
    public void Move()
	{
		

		//transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15);
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed);
		//transform.position = Vector3.Slerp(transform.position, targetPosition, .3f);
		//Debug.Log(transform.localPosition);
		//Debug.Log(targetPosition);
		if (Vector3.Distance(transform.localPosition, targetPosition) < 1)
		{
			transform.localPosition = targetPosition;
			CombatUI.instance.UnregisterCardToMove(this);
		}
	}
    
    
    public void Flip()
	{
		float before = transform.rotation.eulerAngles.y;
		// Lerp rotation to target
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, flipSpeed);
		float after = transform.rotation.eulerAngles.y;

		if (Mathf.Abs(transform.rotation.eulerAngles.y - targetRotation.eulerAngles.y) <= .1)
		{
			transform.rotation = targetRotation;
			CombatUI.instance.UnregisterCardToFlip(this);
		}

		if (hasFlipped == true)
			return;

        // if was front, but now paseed 90, switch active side of card
		if (before >= 270 && after < 270)
		{
			CardFront.SetActive(false);
			CardBack.SetActive(true);
			hasFlipped = true;
			isFront = false;
		}
        // else if was back, but now less than 90, switch active side of card
		else if (before <270 && after >= 270)
		{
			CardFront.SetActive(true);
            CardBack.SetActive(false);
			hasFlipped = true;
			isFront = true;
		}
	}

    public void Scale()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed);

		if (Vector3.Distance(transform.localScale,targetScale) < 0.1f)
		{
			if ( isScalingToOne == false)
			{
				targetScale = Vector3.one;
				isScalingToOne = true;
			}
			else
			{
				transform.localScale = targetScale;
				CombatUI.instance.UnregisterCardToScale(this);
				//isScalingToOne = false;
			}
		}
	}
    
	public void RegisterToMove(Vector3 p)
    {
		targetPosition = p;

        CombatUI.instance.RegisterCardToMove(this);
    }

	public void RegisterToFlip()
	{
		hasFlipped = false;

		if (isFront == true)
			targetRotation = Quaternion.Euler(0, 180, 0);
		else
			targetRotation = Quaternion.Euler(0, 0, 0);

		CombatUI.instance.RegisterCardToFlip(this);
	}

	public void RegisterToScale()
	{
		isScalingToOne = false;
		targetScale = Vector3.one * scaleScaler;
		CombatUI.instance.RegisterCardToScale(this);
    }
}
