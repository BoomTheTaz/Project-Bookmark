using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : Section {
   

	public Prologue()
	{
		SectionName = "Prologue";
		NumPages = 17;
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
			case 6:
                Pages.Add(i, GetPage6);
                break;
			case 7:
                Pages.Add(i, GetPage7);
                break;
			case 8:
                Pages.Add(i, GetPage8);
                break;
			case 9:
                Pages.Add(i, GetPage9);
                break;
			case 10:
                Pages.Add(i, GetPage10);
                break;
			case 11:
                Pages.Add(i, GetPage11);
                break;
			case 12:
                Pages.Add(i, GetPage12);
                break;
			case 13:
                Pages.Add(i, GetPage13);
                break;
			case 14:
                Pages.Add(i, GetPage14);
                break;
			case 15:
                Pages.Add(i, GetPage15);
                break;
			case 16:
                Pages.Add(i, GetPage16);
                break;


			default:
				Debug.LogError("Case " + i.ToString() + " not found in Section " + SectionName);
				break;
		}
	}

	Page GetPage0()
    {
        Page page = new Page();
        
        page.SetHeader("WELCOME");
		page.SetBody("This is the first page of your journey.\n" +
		             "I hope you enjoy!\n" +
		             "Where would you like to go first?\n" +
		             "Where does your adventure take you?");



		page.SetOptionText(0, "I want to sleep in.");
        page.SetOptionText(1, "Let's go to the pub");
        

		page.LinkPage(0, new PageReference("Prologue", 1));
		page.LinkPage(1, new PageReference("Prologue", 3));
		//page.GoToCombat(1, "rats");


  
		return page;
    }
    

	Page GetPage1()
	{
		Page page = new Page();

		page.SetHeader("BOOOORING!");
		page.SetBody("Okay, let's say you have already done that\n" +
		             "And you are currently not tired.\n" +
		             "Now what would you like to do?");

		page.SetOptionText(0, "Netflix all day!");
        page.SetOptionText(1, "Let's head to the pub.");


        page.LinkPage(0, new PageReference("Prologue", 2));
        page.LinkPage(1, new PageReference("Prologue", 3));
        
		return page;
	}

	Page GetPage2()
    {
		Page page = new Page();

		page.SetBody("Oh how wonderful!\n" +
		             "You want to start at the pub?\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "Great Idea!\n" +
		             "All great advenures start there.");

		page.SetOptionText(0, "Fine.... You win.");

        page.LinkPage(0, new PageReference("Prologue", 3));

        return page;
    }

	Page GetPage3()
    {
		Page page = new Page();

		page.SetHeader("Setting: Pub");
		page.SetBody("The pub is packed with the usual crowd\n" +
		             "A bunch of midday drunks yell from the corner\n" +
		             "The bartender acknowledges your entrance with a slight nod\n" +
		             "How would you like to proceed.");

		page.SetOptionText(0, "Approach the drunks");
        page.SetOptionText(1, "Approach the bartender");
       

		page.LinkPage(0, new PageReference("Prologue", 4));
		page.LinkPage(1, new PageReference("Prologue", 5));
        

        return page;
    }

	Page GetPage4()
    {
        Page page = new Page();
        
		page.SetBody("The drunks pay you little mind.");
        
		page.SetOptionText(0, "Shove one of them");
        page.SetOptionText(1, "Go to the bartender");
             
		page.LinkPage(0, new PageReference("Prologue", 6));
		page.LinkPage(1, new PageReference("Prologue", 5));
        
        return page;
    }

	Page GetPage5()
    {
		Page page = new Page();

		page.SetBody("\"What can I do for you?\" asks the bartender");
        
		page.SetOptionText(0, "I need a drink.");
        page.SetOptionText(1, "Looking for work.");
        

		page.LinkPage(0, new PageReference("Prologue", 9));
		page.LinkPage(1, new PageReference("Prologue", 10));

        return page;
    }


	Page GetPage6()
    {
        Page page = new Page();

        page.SetBody("This enrages the drunks to the point of fighting.\n" +
		             "There's no getting out of this one." +
		             "Prepare for battle!");

        page.SetOptionText(0, "Whelp, here goes nothing...");


		page.GoToCombat(0, "Drunks", new PageReference("Prologue", 7), new PageReference("Prologue", 8));
       
        return page;
    }

	// VICTORY: Drunks
    Page GetPage7()
    {
        Page page = new Page();
		page.SetHeader("Victory!");
        page.SetBody("You have successfully defeated the drunks\n" +
		             "Congratulations on your victory!\n" +
		             "Where to now?");

        page.SetOptionText(0, "Let's start over.");
		page.SetOptionText(1, "Let's Fight again");

		page.LinkPage(0, new PageReference("Prologue", 0));
		page.GoToCombat(1, "Drunks", new PageReference("Prologue", 7), new PageReference("Prologue", 8));

        return page;
    }

	// DEFEAT: Drunks
    Page GetPage8()
    {
		Page page = new Page();
        page.SetHeader("Defeat!");
        page.SetBody("You have been bested by the drunks\n" +
                     "How embarassing!\n" +
                     "How will you redeem yourself?");

		page.SetOptionText(0, "Let's start over.");
        page.SetOptionText(1, "Let's Fight again");

        page.LinkPage(0, new PageReference("Prologue", 0));
        page.GoToCombat(1, "Drunks", new PageReference("Prologue", 7), new PageReference("Prologue", 8));

        return page;
    }

    // drink
	Page GetPage9()
    {
        Page page = new Page();

        page.SetBody("The bartender gives you a drink\n" +
		             "While you sip it, the bartender says," +
		             "\"You look like the kind of guy who can fight well.\"");

        page.SetOptionText(0, "Go on...");
        //page.SetOptionText(10, "Looking for work.");


        page.LinkPage(0, new PageReference("Prologue", 10));

        return page;
    }

    // work
	Page GetPage10()
    {
        Page page = new Page();

        page.SetBody("\"I have a rat infestation,\n" +
		             "think you could lend a hand?\" asks the bartender");

        page.SetOptionText(0, "Sure thing. Glad to help.");
        page.SetOptionText(1, "I'm not feeling it today");


        page.LinkPage(0, new PageReference("Prologue", 11));
        page.LinkPage(1, new PageReference("Prologue", 14));

        return page;
    }

	Page GetPage11()
    {
        Page page = new Page();

        page.SetBody("\"Thank you kindly.\n" +
                     "Drink is on me. Now get to it.\" asks the bartender");

        page.SetOptionText(0, "On my way!");
        
		page.GoToCombat(1, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));
        
        return page;
    }

	// VICTORY: Rats
    Page GetPage12()
    {
        Page page = new Page();
        page.SetHeader("Victory!");
        page.SetBody("You have successfully defeated the rats\n" +
                     "Congratulations on your victory!\n" +
                     "Where to now?");

        page.SetOptionText(0, "Let's start over.");
        page.SetOptionText(1, "Let's Fight again");

        page.LinkPage(0, new PageReference("Prologue", 0));
		page.GoToCombat(1, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));

        return page;
    }

    // DEFEAT: Drunks
    Page GetPage13()
    {
        Page page = new Page();
        page.SetHeader("Defeat!");
        page.SetBody("You have been bested by the rats\n" +
                     "How embarassing!\n" +
                     "How will you redeem yourself?");

        page.SetOptionText(0, "Let's start over.");
        page.SetOptionText(1, "Let's Fight again");

        page.LinkPage(0, new PageReference("Prologue", 0));
		page.GoToCombat(1, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));

        return page;
    }

	Page GetPage14()
    {
        Page page = new Page();

		page.SetBody("\"That wasn't really a question,\"\n" +
		             "the bartender growls." +
		             "\"You fight me or them\n" +
		             "Take your pick\"");

        page.SetOptionText(0, "Rats it is...");
        page.SetOptionText(1, "You've made a huge mistake");

		page.GoToCombat(0, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));
        page.GoToCombat(1, "Bartender", new PageReference("Prologue", 15), new PageReference("Prologue", 16));

        return page;
    }

	// VICTORY: Bartender
    Page GetPage15()
    {
        Page page = new Page();
        page.SetHeader("Victory!");
        page.SetBody("You have successfully defeated the Bartender\n" +
                     "As you stand over his corpse, you feel pride\n" +
		             "And more than a little confusion." +
                     "What will you do now?");

        page.SetOptionText(0, "Let's start over.");
        page.SetOptionText(1, "Let's Fight again");

        page.LinkPage(0, new PageReference("Prologue", 0));
		page.GoToCombat(1, "Bartender", new PageReference("Prologue", 15), new PageReference("Prologue", 16));

        return page;
    }

    // DEFEAT: Bartender
    Page GetPage16()
    {
        Page page = new Page();
        page.SetHeader("Defeat!");
        page.SetBody("You have been bested by the bartender\n" +
                     "How embarassing!\n" +
		             "He spits on your bloodied body and laughs.\n" +
                     "How will you redeem yourself?");

        page.SetOptionText(0, "Let's start over.");
        page.SetOptionText(1, "Let's Fight again");

        page.LinkPage(0, new PageReference("Prologue", 0));
		page.GoToCombat(1, "Bartender", new PageReference("Prologue", 15), new PageReference("Prologue", 16));

        return page;
    }

}
