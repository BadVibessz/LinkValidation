using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Core;

public static class LinkExtracter
{
    public static List<string> ExtractAllLinks(string uri)
    {
        string domain = new Uri(uri).GetLeftPart(UriPartial.Authority) + "/";
        var list = new List<string>();
        var doc = new HtmlWeb().Load(uri);
        foreach (var link in doc.DocumentNode.SelectNodes("//a[@href]"))
        {
            string value = link.Attributes["href"].Value;
            if (value != "#")
            {
                if (value.Contains(domain) && !list.Contains(value))
                    list.Add(value);
                else if (!value.Contains("http") && !list.Contains(domain + value))
                    list.Add(domain + value);
            }
        }

        return list;
    }
}