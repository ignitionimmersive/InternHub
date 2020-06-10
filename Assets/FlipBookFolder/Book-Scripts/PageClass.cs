using System.Collections;
using System.Collections.Generic;


public class PageClass
{
    public string Title { get; set; }
    public string Text { get; set; }
    public List<string> Pages { get; set; }

    public static List<PageClass> _pageList = null;
    public static PageClass RandomPage;

    public static int CurrentPage1 = 0;
    public static int CurrentPage2 = 1;

    public static PageClass GetRandomPage()
    {
        List<PageClass> pageList = PageClass.PageList;
        int num = UnityEngine.Random.Range(0, pageList.Count);
        PageClass pge = pageList[num];
        pge.Pages = new List<string>();

        string[] words = pge.Text.Split(' ');

        string page = "";
        int wordCount = 0;

        foreach(string word in words)
        {
            wordCount++;
            if(wordCount > 45)
            {
                pge.Pages.Add(page);
                page = "";
                wordCount = 0;
            }
            page += string.Format(" {0} ", word);
        }
        pge.Pages.Add(page);

        RandomPage = pge;
        return pge;

    }

    public static List<PageClass> PageList
    {
        get
        {
            if(_pageList == null)
            {
                _pageList = new List<PageClass>();
                _pageList.Add(new PageClass { Title = "title", Text = "God of War is an action-adventure game franchise created by David Jaffe at Sony's Santa Monica Studio. It began in 2005 on the PlayStation 2 (PS2) video game console, and has become a flagship title for the PlayStation brand, consisting of eight games across multiple platforms. Based in ancient mythology, the story follows Kratos, a Spartan warrior who was tricked into killing his family by his former master, the Greek God of War Ares. This sets off a series of events that leads to wars with the mythological pantheons. The Greek mythology era of the series sees Kratos follow a path of vengeance due to the machinations of the Olympian gods, while the Norse mythology era, which introduces his son Atreus as a secondary protagonist, shows an older Kratos on a path of redemption, which inadvertently brings the two into conflict with the Norse gods." });
            }
            return _pageList;
        }
    }

}
