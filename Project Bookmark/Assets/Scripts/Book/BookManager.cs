using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager  {

	Section CurrentSection;
	Page CurrentPage;
	Dictionary<string, Section> Sections;


	public BookManager(string s)
	{
		Sections = new Dictionary<string, Section>();
		AddSection(s);
	}

    void AddSection(string s)
	{
		Section section;
		switch (s)
		{
			case "Prologue":
				section = new Prologue();
				section.SetupDictionary();
				Sections.Add(s, section);
				break;

			case "TestSection":
				section = new TestSection();
                section.SetupDictionary();
                Sections.Add(s, section);
                break;


			default:
				section = null;
				Debug.LogError("No valid case for Section named " + s + ".");
				break;
		}
		CurrentSection = section;
	}

    public void SetupPage(int i)
	{
		CurrentPage = CurrentSection.GetPage(i);

		UIManager.instance.SetupPage(CurrentPage);
	}
    
	public void SetupPageFromSection(string s, int i)
	{
		if (Sections.ContainsKey(s) == true)
		{
			CurrentSection = Sections[s];
			SetupPage(i);
		}
		else
		{
			AddSection(s);
			SetupPage(i);
		}
	}

    public void SelectedOption(int i)
	{
		PageReference pr = CurrentPage.GetPageReference(i);

		// Same Section
		if (CurrentSection.GetSectionName() == pr.section)
			SetupPage(pr.page);
		else
			SetupPageFromSection(pr.section, pr.page);
	}

}
