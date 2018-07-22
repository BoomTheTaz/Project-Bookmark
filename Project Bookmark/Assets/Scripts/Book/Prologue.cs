using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : Section {
   

	public Prologue()
	{
		SectionName = "Prologue";
		NumPages = 6;
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
			case 5:
				Pages.Add(i, GetPage5);
                break;

			default:
				Debug.LogError("Case " + i.ToString() + " not found in Section " + SectionName);
				break;
		}
	}

	Page GetPage0()
    {
        Page page = new Page();
        
        page.SetHeader("PAGE ZERO");
		page.SetBody("THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. " +
		             "THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. " +
		             "THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. " +
		             "THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. THIS IS PAGE ZERO. ");
		
		page.SetOptionText(0, "Go to page 0.");
        page.SetOptionText(1, "Go to page 1.");
        page.SetOptionText(2, "Go to page 2.");
        page.SetOptionText(3, "Go to page 3.");
        page.SetOptionText(4, "Go to page 4.");
        page.SetOptionText(5, "Go to page 5.");

		page.SetOptionPageLink(0, new PageReference("Prologue", 0));
		page.SetOptionPageLink(1, new PageReference("Prologue", 1));
		page.SetOptionPageLink(2, new PageReference("Prologue", 2));
		page.SetOptionPageLink(3, new PageReference("Prologue", 3));
		page.SetOptionPageLink(4, new PageReference("Prologue", 4));
		page.SetOptionPageLink(5, new PageReference("Prologue", 5));

        
        return page;
    }
    

	Page GetPage1()
	{
		Page page = new Page();

		page.SetHeader("PAGE ONE");
		page.SetBody("THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. " +
		             "THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. " +
		             "THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. " +
		             "THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE. THIS IS PAGE ONE.");

		page.SetOptionText(0, "Go to Test page 0.");
		page.SetOptionText(1, "Go to Test page 1.");
        page.SetOptionText(2, "Go to page 2.");
        page.SetOptionText(3, "Go to page 3.");
        page.SetOptionText(4, "Go to page 4.");
        page.SetOptionText(5, "Go to page 5.");

		page.SetOptionPageLink(0, new PageReference("TestSection", 0));
		page.SetOptionPageLink(1, new PageReference("TestSection", 1));
        page.SetOptionPageLink(2, new PageReference("Prologue", 2));
        page.SetOptionPageLink(3, new PageReference("Prologue", 3));
        page.SetOptionPageLink(4, new PageReference("Prologue", 4));
        page.SetOptionPageLink(5, new PageReference("Prologue", 5));

		return page;
	}

	Page GetPage2()
    {
		Page page = new Page();

        page.SetHeader("PAGE TWO");
		page.SetBody("THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. " +
		             "THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. " +
		             "THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. " +
		             "THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. THIS IS PAGE TWO. ");

		page.SetOptionText(0, "Go to page 0.");
		page.SetOptionText(1, "Go to page 1.");
		page.SetOptionText(2, "Go to page 2.");
		page.SetOptionText(3, "Go to page 3.");
		page.SetOptionText(4, "Go to page 4.");
		page.SetOptionText(5, "Go to page 5.");

		page.SetOptionPageLink(0, new PageReference("Prologue", 0));
        page.SetOptionPageLink(1, new PageReference("Prologue", 1));
        page.SetOptionPageLink(2, new PageReference("Prologue", 2));
        page.SetOptionPageLink(3, new PageReference("Prologue", 3));
        page.SetOptionPageLink(4, new PageReference("Prologue", 4));
        page.SetOptionPageLink(5, new PageReference("Prologue", 5));

        return page;
    }

	Page GetPage3()
    {
		Page page = new Page();

        page.SetHeader("PAGE THREE");
		page.SetBody("THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. " +
		             "THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. " +
		             "THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. " +
		             "THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. THIS IS PAGE THREE. ");

		page.SetOptionText(0, "Go to page 0.");
        page.SetOptionText(1, "Go to page 1.");
        page.SetOptionText(2, "Go to page 2.");
        page.SetOptionText(3, "Go to page 3.");
        page.SetOptionText(4, "Go to page 4.");
        page.SetOptionText(5, "Go to page 5.");

		page.SetOptionPageLink(0, new PageReference("Prologue", 0));
        page.SetOptionPageLink(1, new PageReference("Prologue", 1));
        page.SetOptionPageLink(2, new PageReference("Prologue", 2));
        page.SetOptionPageLink(3, new PageReference("Prologue", 3));
        page.SetOptionPageLink(4, new PageReference("Prologue", 4));
        page.SetOptionPageLink(5, new PageReference("Prologue", 5));

        return page;
    }

	Page GetPage4()
    {
        Page page = new Page();

        page.SetHeader("PAGE FOUR");
		page.SetBody("THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. " +
		             "THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. " +
		             "THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. " +
		             "THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. THIS IS PAGE FOUR. ");

		page.SetOptionText(0, "Go to page 0.");
        page.SetOptionText(1, "Go to page 1.");
        page.SetOptionText(2, "Go to page 2.");
        page.SetOptionText(3, "Go to page 3.");
        page.SetOptionText(4, "Go to page 4.");
        page.SetOptionText(5, "Go to page 5.");

		page.SetOptionPageLink(0, new PageReference("Prologue", 0));
        page.SetOptionPageLink(1, new PageReference("Prologue", 1));
        page.SetOptionPageLink(2, new PageReference("Prologue", 2));
        page.SetOptionPageLink(3, new PageReference("Prologue", 3));
        page.SetOptionPageLink(4, new PageReference("Prologue", 4));
        page.SetOptionPageLink(5, new PageReference("Prologue", 5));

        return page;
    }

	Page GetPage5()
    {
		Page page = new Page();

        page.SetHeader("PAGE FIVE");
		page.SetBody("THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. " +
		             "THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. " +
		             "THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. " +
		             "THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. THIS IS PAGE FIVE. ");

		page.SetOptionText(0, "Go to page 0.");
        page.SetOptionText(1, "Go to page 1.");
        page.SetOptionText(2, "Go to page 2.");
        page.SetOptionText(3, "Go to page 3.");
        page.SetOptionText(4, "Go to page 4.");
        page.SetOptionText(5, "Go to page 5.");

		page.SetOptionPageLink(0, new PageReference("Prologue", 0));
        page.SetOptionPageLink(1, new PageReference("Prologue", 1));
        page.SetOptionPageLink(2, new PageReference("Prologue", 2));
        page.SetOptionPageLink(3, new PageReference("Prologue", 3));
        page.SetOptionPageLink(4, new PageReference("Prologue", 4));
        page.SetOptionPageLink(5, new PageReference("Prologue", 5));

        return page;
    }


}
