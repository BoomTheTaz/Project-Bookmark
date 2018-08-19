using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : Section
{


	public Prologue()
	{
		SectionName = "Prologue";
		NumPages = 17;
	}

	public override Page GetPage(int i)
	{
		Page toReturn = null;

		switch (i)
		{
			case 0:
				toReturn = new Page();

				toReturn.SetHeader("WELCOME");
				toReturn.SetBody("This is the first page of your journey.\n" +
							 "I hope you enjoy!\n" +
							 "Where would you like to go first?\n" +
							 "Where does your adventure take you?");



				toReturn.SetOptionText(0, "I want to sleep in.");
				toReturn.SetOptionText(1, "Let's go to the pub");


				toReturn.LinkPage(0, new PageReference("Prologue", 1));
				toReturn.LinkPage(1, new PageReference("Prologue", 3));

				return toReturn;

			case 1:
				toReturn = new Page();

				toReturn.SetHeader("BOOOORING!");
				toReturn.SetBody("Okay, let's say you have already done that\n" +
							 "And you are currently not tired.\n" +
							 "Now what would you like to do?");

				toReturn.SetOptionText(0, "Netflix all day!");
				toReturn.SetOptionText(1, "Let's head to the pub.");


				toReturn.LinkPage(0, new PageReference("Prologue", 2));
				toReturn.LinkPage(1, new PageReference("Prologue", 3));

				return toReturn;

			case 2:
				toReturn = new Page();

				toReturn.SetBody("Oh how wonderful!\n" +
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

				toReturn.SetOptionText(0, "Fine.... You win.");

				toReturn.LinkPage(0, new PageReference("Prologue", 3));

				return toReturn;

			case 3:
				toReturn = new Page();

				toReturn.SetHeader("Setting: Pub");
				toReturn.SetBody("The pub is packed with the usual crowd\n" +
							 "A bunch of midday drunks yell from the corner\n" +
							 "The bartender acknowledges your entrance with a slight nod\n" +
							 "How would you like to proceed.");

				toReturn.SetOptionText(0, "Approach the drunks");
				toReturn.SetOptionText(1, "Approach the bartender");


				toReturn.LinkPage(0, new PageReference("Prologue", 4));
				toReturn.LinkPage(1, new PageReference("Prologue", 5));

				return toReturn;

			case 4:
				toReturn = new Page();

				toReturn.SetBody("The drunks pay you little mind.");

				toReturn.SetOptionText(0, "Shove one of them");
				toReturn.SetOptionText(1, "Go to the bartender");

				toReturn.LinkPage(0, new PageReference("Prologue", 6));
				toReturn.LinkPage(1, new PageReference("Prologue", 5));

				return toReturn;

			case 5:
				toReturn = new Page();

				toReturn.SetBody("\"What can I do for you?\" asks the bartender");

				toReturn.SetOptionText(0, "I need a drink.");
				toReturn.SetOptionText(1, "Looking for work.");


				toReturn.LinkPage(0, new PageReference("Prologue", 9));
				toReturn.LinkPage(1, new PageReference("Prologue", 10));

				return toReturn;

			case 6:
				toReturn = new Page();

				toReturn.SetBody("This enrages the drunks to the point of fighting.\n" +
					 "There's no getting out of this one." +
					 "Prepare for battle!");

				toReturn.SetOptionText(0, "Whelp, here goes nothing...");


				toReturn.GoToCombat(0, "Drunks", new PageReference("Prologue", 7), new PageReference("Prologue", 8));

				return toReturn;

			case 7:
				toReturn = new Page();

				toReturn.SetHeader("Victory!");
				toReturn.SetBody("You have successfully defeated the drunks\n" +
							 "Congratulations on your victory!\n" +
							 "Where to now?");

				toReturn.SetOptionText(0, "Let's start over.");
				toReturn.SetOptionText(1, "Let's Fight again");

				toReturn.LinkPage(0, new PageReference("Prologue", 0));
				toReturn.GoToCombat(1, "Drunks", new PageReference("Prologue", 7), new PageReference("Prologue", 8));

				return toReturn;

			case 8:
				toReturn = new Page();

				toReturn.SetHeader("Defeat!");
				toReturn.SetBody("You have been bested by the drunks\n" +
							 "How embarassing!\n" +
							 "How will you redeem yourself?");

				toReturn.SetOptionText(0, "Let's start over.");
				toReturn.SetOptionText(1, "Let's Fight again");

				toReturn.LinkPage(0, new PageReference("Prologue", 0));
				toReturn.GoToCombat(1, "Drunks", new PageReference("Prologue", 7), new PageReference("Prologue", 8));

				return toReturn;

			case 9:
				toReturn = new Page();

				toReturn.SetBody("The bartender gives you a drink\n" +
					 "While you sip it, the bartender says," +
					 "\"You look like the kind of guy who can fight well.\"");

				toReturn.SetOptionText(0, "Go on...");
				//page.SetOptionText(10, "Looking for work.");


				toReturn.LinkPage(0, new PageReference("Prologue", 10));

				return toReturn;

			case 10:
				toReturn = new Page();

				toReturn.SetBody("\"I have a rat infestation,\n" +
					 "think you could lend a hand?\" asks the bartender");

				toReturn.SetOptionText(0, "Sure thing. Glad to help.");
				toReturn.SetOptionText(1, "I'm not feeling it today");


				toReturn.LinkPage(0, new PageReference("Prologue", 11));
				toReturn.LinkPage(1, new PageReference("Prologue", 14));

				return toReturn;

			case 11:
				toReturn = new Page();

				toReturn.SetBody("\"Thank you kindly.\n" +
					 "Drink is on me. Now get to it.\" asks the bartender");

				toReturn.SetOptionText(0, "On my way!");

				toReturn.GoToCombat(0, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));

				return toReturn;

			case 12:
				toReturn = new Page();

				toReturn.SetHeader("Victory!");
				toReturn.SetBody("You have successfully defeated the rats\n" +
							 "Congratulations on your victory!\n" +
							 "Where to now?");

				toReturn.SetOptionText(0, "Let's start over.");
				toReturn.SetOptionText(1, "Let's Fight again");

				toReturn.LinkPage(0, new PageReference("Prologue", 0));
				toReturn.GoToCombat(1, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));

				return toReturn;

			case 13:
				toReturn = new Page();

				toReturn.SetHeader("Defeat!");
				toReturn.SetBody("You have been bested by the rats\n" +
							 "How embarassing!\n" +
							 "How will you redeem yourself?");

				toReturn.SetOptionText(0, "Let's start over.");
				toReturn.SetOptionText(1, "Let's Fight again");

				toReturn.LinkPage(0, new PageReference("Prologue", 0));
				toReturn.GoToCombat(1, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));

				return toReturn;

			case 14:
				toReturn = new Page();

				toReturn.SetBody("\"That wasn't really a question,\"\n" +
					 "the bartender growls." +
					 "\"You fight me or them\n" +
					 "Take your pick\"");

				toReturn.SetOptionText(0, "Rats it is...");
				toReturn.SetOptionText(1, "You've made a huge mistake");

				toReturn.GoToCombat(0, "Rats", new PageReference("Prologue", 12), new PageReference("Prologue", 13));
				toReturn.GoToCombat(1, "Bartender", new PageReference("Prologue", 15), new PageReference("Prologue", 16));

				return toReturn;

			case 15:
				toReturn = new Page();

				toReturn.SetHeader("Victory!");
				toReturn.SetBody("You have successfully defeated the Bartender\n" +
							 "As you stand over his corpse, you feel pride\n" +
							 "And more than a little confusion." +
							 "What will you do now?");

				toReturn.SetOptionText(0, "Let's start over.");
				toReturn.SetOptionText(1, "Let's Fight again");

				toReturn.LinkPage(0, new PageReference("Prologue", 0));
				toReturn.GoToCombat(1, "Bartender", new PageReference("Prologue", 15), new PageReference("Prologue", 16));

				return toReturn;

			case 16:
				toReturn = new Page();

				toReturn.SetHeader("Defeat!");
				toReturn.SetBody("You have been bested by the bartender\n" +
							 "How embarassing!\n" +
							 "He spits on your bloodied body and laughs.\n" +
							 "How will you redeem yourself?");

				toReturn.SetOptionText(0, "Let's start over.");
				toReturn.SetOptionText(1, "Let's Fight again");

				toReturn.LinkPage(0, new PageReference("Prologue", 0));
				toReturn.GoToCombat(1, "Bartender", new PageReference("Prologue", 15), new PageReference("Prologue", 16));

				return toReturn;




			default:
				break;
		}

		return toReturn;
	}

	#region Old Code

	//protected override void Switches(int i)
	//{
	//	switch (i)
	//	{
	//		case 0:
	//			Pages.Add(i, GetPage0);
	//			break;
	//		case 1:
	//			Pages.Add(i, GetPage1);
	//			break;
	//		case 2:
	//			Pages.Add(i, GetPage2);
	//               break;
	//		case 3:
	//			Pages.Add(i, GetPage3);
	//               break;
	//		case 4:
	//			Pages.Add(i, GetPage4);
	//               break;
	//		case 5:
	//			Pages.Add(i, GetPage5);
	//               break;
	//		case 6:
	//               Pages.Add(i, GetPage6);
	//               break;
	//		case 7:
	//               Pages.Add(i, GetPage7);
	//               break;
	//		case 8:
	//               Pages.Add(i, GetPage8);
	//               break;
	//		case 9:
	//               Pages.Add(i, GetPage9);
	//               break;
	//		case 10:
	//               Pages.Add(i, GetPage10);
	//               break;
	//		case 11:
	//               Pages.Add(i, GetPage11);
	//               break;
	//		case 12:
	//               Pages.Add(i, GetPage12);
	//               break;
	//		case 13:
	//               Pages.Add(i, GetPage13);
	//               break;
	//		case 14:
	//               Pages.Add(i, GetPage14);
	//               break;
	//		case 15:
	//               Pages.Add(i, GetPage15);
	//               break;
	//		case 16:
	//               Pages.Add(i, GetPage16);
	//               break;


	//		default:
	//			Debug.LogError("Case " + i.ToString() + " not found in Section " + SectionName);
	//			break;
	//	}
	//}

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

#endregion
}
