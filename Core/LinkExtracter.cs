using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Core;

public static class LinkExtracter
{
    public static void ExtractLinksFromPage(string uri, List<string> list)
    {
        string domain = new Uri(uri).GetLeftPart(UriPartial.Authority) + "/";

        if (list.Count == 0)
            list.Add(uri);

        var nodes = new HtmlWeb().Load(uri).DocumentNode.SelectNodes("//a[@href]");
        
        if (nodes is not null)
            foreach (var link in nodes)
            {
                if (link is not null)
                {
                    string value = link.Attributes["href"].Value;

                    if (value != "#")
                    {
                        Uri temp;
                        if (value.Contains(domain) && !list.Contains(value))
                        {
                            list.Add(value);
                            ExtractLinksFromPage(value, list);
                        }

                        else if (!value.Contains("http") && !list.Contains(domain + value))
                        {
                            list.Add(domain + value);
                            ExtractLinksFromPage(domain + value, list);
                        }
                    }
                }
            }
    }
    
}