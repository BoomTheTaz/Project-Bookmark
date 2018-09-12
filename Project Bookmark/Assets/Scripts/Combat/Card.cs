using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CardType { ATK_Phys, ATK_Mag, DEF_Phys, DEF_Mag}

public class Card : MonoBehaviour
{

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

    public bool hasAttackEffect { get; private set; }
    public bool hasDefenseEffect { get; private set; }

    public delegate void CardEffect();
    public CardEffect AttackEffect { get; private set; }
    public CardEffect DefenseEffect { get; private set; }

    public bool isPlayer;

    // ===== MOVE VARIABLES =====
    const float moveTime = .3f;
    Vector3 moveVel;

    // ===== FLIP VARIABLES =====
    const float flipSpeed = 6f;

    // ===== SCALE VARIABLES =====
    const float scaleSpeed = 10f;

    float moveSpeed = 0.04f;
	//float flipSpeed = 0.07f;
	//float scaleSpeed = 0.15f;
	float scaleScaler = 1.5f;

	public int TemplateID { get; private set; }

	private void Awake()
	{
		if (Width == 0)
        {
            Width = Mathf.RoundToInt(GetComponent<RectTransform>().rect.width);
            Height = Mathf.RoundToInt(GetComponent<RectTransform>().rect.height);
        }
	}
 
	public float GetWidth()
	{
		return GetComponent<RectTransform>().rect.width;
	}

	float BaseScaler = 0.6f;
	float AttackScaler = 1.2f;
	float MagicScaler = 1.5f;
	float AltScaler = 2f;


	public void SetupCard(CardStats c, CharacterData data)
	{
		Type = c.Type;
		Name = c.Name;
		AP = c.AP;
		TemplateID = c.TemplateID;
        
		switch (Type)
		{
			case CardType.ATK_Mag:
				ATK = Mathf.RoundToInt(data.WeaponMag + AttackScaler * MagicScaler * BaseScaler * data.GetStat(Stats.Magic) * (Mathf.Pow(c.AP, 2) + c.ATK));
				DEF = Mathf.RoundToInt(data.ArmorMag + MagicScaler * BaseScaler * data.GetStat(Stats.Magic) * (Mathf.Pow(c.AP,2) + c.DEF) / AltScaler);
				break;
			case CardType.ATK_Phys:
				ATK = Mathf.RoundToInt(data.WeaponPhys + AttackScaler * BaseScaler * data.GetStat(Stats.Power) * (Mathf.Pow(c.AP, 2) + c.ATK));
				DEF = Mathf.RoundToInt(data.ArmorPhys + BaseScaler * data.GetStat(Stats.Technique) * (Mathf.Pow(c.AP, 2) + c.DEF) / AltScaler);
				break;
			case CardType.DEF_Mag:
				ATK = Mathf.RoundToInt(data.WeaponMag + AttackScaler * MagicScaler * BaseScaler * data.GetStat(Stats.Magic) * (Mathf.Pow(c.AP, 2) + c.ATK) / AltScaler);
				DEF = Mathf.RoundToInt(data.ArmorMag + MagicScaler * BaseScaler * data.GetStat(Stats.Magic) * (Mathf.Pow(c.AP, 2) + c.DEF));
                break;
			case CardType.DEF_Phys:
				ATK = Mathf.RoundToInt(data.WeaponPhys + AttackScaler * BaseScaler * data.GetStat(Stats.Power) * (Mathf.Pow(c.AP, 2) + c.ATK) / AltScaler);
				DEF = Mathf.RoundToInt(data.ArmorPhys + BaseScaler * data.GetStat(Stats.Technique) * (Mathf.Pow(c.AP, 2) + c.DEF));
                break;
                
			default:
				Debug.LogError("Invalid Card type");
				break;
		}

        if (c.EffectType != EffectTypes.NONE)
            SetCardEffects(c.EffectType, c.EffectInt);

		ATK = Mathf.RoundToInt(ATK * AttackScaler);

		UpdateVisuals();
	}

    void SetCardEffects(EffectTypes t, int i)
    {
        Debug.Log("Setting up card Effect");
        switch (t)
        {
            case EffectTypes.NONE:
                break;
            case EffectTypes.GainAP:
                DefenseEffect =  () => {CombatManager.instance.GainAP(i, isPlayer); };
                hasDefenseEffect = true;
                break;
            case EffectTypes.TakeAP:
                break;
            case EffectTypes.DrawCard:
                break;
            case EffectTypes.DiscardCard:
                break;
            default:
                break;
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

    public void HasAttackEffect()
    {
        hasAttackEffect = true;
    }

    public void HasDefenseEffect()
    {
        hasDefenseEffect = true;
    }

    #region Move Card
    public void RegisterToMove(Vector3 p)
	{
		targetPosition = p;

		CombatUI.instance.RegisterCardToMove(this);
	}

	public void Move()
	{
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref moveVel, moveTime);
		
		if (Vector3.Distance(transform.localPosition, targetPosition) < 1)
		{
			transform.localPosition = targetPosition;
			CombatUI.instance.UnregisterCardToMove(this);
		}
	}
	#endregion

	#region Flip Card

	float angleFlipped;
	public void RegisterToFlip()
	{
		hasFlipped = false;
		angleFlipped = 0;
		if (isFront == true)
			targetRotation = Quaternion.Euler(0, 180, 0);
		else
			targetRotation = Quaternion.Euler(0, 0, 0);


		CombatUI.instance.RegisterCardToFlip(this);
	}
	public void Flip()
	{
       
        float before = transform.rotation.eulerAngles.y;
		// Lerp rotation to target
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, flipSpeed * Time.deltaTime);
		float after = transform.rotation.eulerAngles.y;

		if (hasFlipped == true)
		    angleFlipped += Mathf.Abs(after - before);

		if (90-angleFlipped < 2)
		{
			transform.rotation = targetRotation;
			CombatUI.instance.UnregisterCardToFlip(this);
		}
        
		if (hasFlipped == true)
			return;

		if (Mathf.Abs(before - after) < 20)
		{

			// if was front, but now paseed 90, switch active side of card
			if (before >= 270 && after < 270)
			{
				CardFront.SetActive(false);
				CardBack.SetActive(true);
				hasFlipped = true;
				isFront = false;
			}
			// else if was back, but now less than 90, switch active side of card
			else if (before < 270 && after >= 270)
			{
				CardFront.SetActive(true);
				CardBack.SetActive(false);
				hasFlipped = true;
				isFront = true;
			}
		}
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

	#endregion

	#region Scale Card
	public void Scale()
	{
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

		if (Vector3.Distance(transform.localScale, targetScale) < 0.1f)
		{
			if (isScalingToOne == false)
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
    

    
	public void RegisterToScale(bool shouldEnlarge = false)
	{
		
		if (isPlayer == true && shouldEnlarge == true)
		{
			isScalingToOne = false;
			targetScale = Vector3.one * scaleScaler;
			CombatUI.instance.RegisterCardToScale(this);
		}
		else
		{
			targetScale = Vector3.one;
			isScalingToOne = true;
			CombatUI.instance.RegisterCardToScale(this);
		}


	}
#endregion
}
