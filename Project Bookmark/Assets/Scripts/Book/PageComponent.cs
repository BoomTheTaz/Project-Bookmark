using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageComponent : MonoBehaviour {

    static float WIDTH;
    static Vector3 OFF_POSITION;
    public PageComponent OtherPage;
    public Image Shader;

    // Page text and Options
    public Text HeaderText;
    public Text BodyText;
    public Transform OptionContainer;
    public RectTransform ScrollContent;
    OptionButton[] Options;

    bool ShouldMove = false;
    float flipTime = 4f;
    float Timer;
    Vector3 vel;
    Color shadeColor = Color.black;

    // Use this for initialization
    void Start() {
        
        if (WIDTH == 0)
        {
            WIDTH = GetComponent<RectTransform>().rect.width;
            OFF_POSITION = Vector3.left * WIDTH ;
        }

        Options = new OptionButton[OptionContainer.childCount];
        for (int i = 0; i < OptionContainer.childCount; i++)
        {
            Options[i] = OptionContainer.GetChild(i).GetComponent<OptionButton>();
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.T) && transform.name == "Page_1")
            FlipPage();

        if (ShouldMove == true)
        {
            Timer += Time.deltaTime;
            float t = Timer/flipTime;
            transform.localPosition = Vector3.Slerp(transform.localPosition, OFF_POSITION, t);
            OtherPage.SetShade(-transform.localPosition.x);

            // Check if flipping is complete
            if (Vector3.Distance(transform.localPosition, OFF_POSITION) <= 2)
            {
                ShouldMove = false;                         // Stop moving
                transform.SetAsFirstSibling();              // Place behind other sibling
                OtherPage.RemoveShader();                   // Remove shader on other page, allowing clicks
                transform.localPosition = Vector3.zero;     // reset position
                gameObject.SetActive(false);
            }
        }
	}

    public void FlipPage()
    {
        Debug.Log("Flipping Page");
        Timer = 0;
        ShouldMove = true;
    }

    // Function called by other page to set shad based on distance travelled
    public void SetShade(float f)
    {
        shadeColor.a = (1 - f / WIDTH) * 200/ 256;
        Shader.color = shadeColor;
    }

    public void RemoveShader()
    {
        Shader.gameObject.SetActive(false);
    }

    public void SetupPage(Page page)
    {
        gameObject.SetActive(true);
        Shader.gameObject.SetActive(true);
        // Disable Header if none given, else setup normally
        if (page.Header == null)
        {
            HeaderText.gameObject.SetActive(false);
        }
        else
        {
            HeaderText.gameObject.SetActive(true);
            HeaderText.text = page.Header;

        }

        // Disable Body if none given, else setup normally
        if (page.Body == null)
        {
            BodyText.gameObject.SetActive(false);
        }
        else
        {
            BodyText.gameObject.SetActive(true);
            BodyText.text = page.Body;
        }

        for (int i = 0; i < page.OptionTexts.Length; i++)
        {
            if (page.OptionTexts[i] != null)
            {
                Options[i].gameObject.SetActive(true);
                Options[i].SetText(page.OptionTexts[i]);
                Options[i].ActionOnClick = page.ButtonActions[i];
            }
            else
            {
                Options[i].gameObject.SetActive(false);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(ScrollContent);
    }
}