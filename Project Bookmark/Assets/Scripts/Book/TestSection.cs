using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSection : Section {

	public TestSection()
	{
		SectionName = "TestSection";
		NumPages = 5;
	}

	protected override void Switches(int i)
    {
        switch (i)
        {
            case 0:
                Pages.Add(i, GetPage0);
                break;
            case 1:
                Pages.Add(i, GetPage1);
                break;
            case 2:
                Pages.Add(i, GetPage2);
                break;
            case 3:
                Pages.Add(i, GetPage3);
                break;
            case 4:
                Pages.Add(i, GetPage4);
				break;

            default:
                Debug.LogError("Case " + i.ToString() + " not found in Section " + SectionName);
                break;
        }
    }

	Page GetPage0()
    {
        Page page = new Page();

        page.SetHeader("PAGE ZERO TEST");
        page.SetBody("THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. " +
                     "THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. " +
                     "THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. " +
                     "THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. ");
        if (PlayerData.instance == null)
            Debug.Log("AHHHH");
        if (PlayerData.instance.StatCheck(Stats.Strength, 10) == true)
            page.SetOptionText(0, "You are strong enough.");
        if (PlayerData.instance.StatCheck(Stats.Dexterity, 10) == true)
            page.SetOptionText(1, "You are Dexterous enough.");
        if (PlayerData.instance.StatCheck(Stats.Constitution, 10) == true)
            page.SetOptionText(2, "You are Con enough.");
        if (PlayerData.instance.StatCheck(Stats.Intelligence, 10) == true)
            page.SetOptionText(3, "You are Smart enough.");
        if (PlayerData.instance.StatCheck(Stats.Wisdom, 10) == true)
            page.SetOptionText(4, "You are wise enough.");
        if (PlayerData.instance.StatCheck(Stats.Charisma, 10) == true)
            page.SetOptionText(5, "You are Charismatic enough.");

        return page;
    }


    Page GetPage1()
    {
        Page page = new Page();

		page.SetHeader("PAGE ONE TEST");
        page.SetBody("THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. " +
                     "THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. " +
                     "THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. " +
                     "THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE.");


        return page;
    }

    Page GetPage2()
    {
        Page page = new Page();

		page.SetHeader("PAGE TWO TEST");
        page.SetBody("THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. " +
                     "THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. " +
                     "THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. " +
                     "THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. ");


        return page;
    }

    Page GetPage3()
    {
        Page page = new Page();

		page.SetHeader("PAGE THREE TEST");
        page.SetBody("THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. " +
                     "THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. " +
                     "THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. " +
                     "THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. ");


        return page;
    }

    Page GetPage4()
    {
        Page page = new Page();

		page.SetHeader("PAGE FOUR TEST");
        page.SetBody("THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. " +
                     "THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. " +
                     "THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. " +
                     "THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. ");


        return page;
    }
}
