using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Core;

public static class LinkExtracter
{
    public static Uri BaseUri { get; set; }

    public static void ExtractLinksFromPage(string uri, List<string> list)
    {
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
                        if (Uri.TryCreate(BaseUri, value, out Uri? temp))
                            if (!list.Contains(temp.ToString()) && temp.Host == BaseUri.Host)
                            {
                                list.Add(temp.ToString());
                                ExtractLinksFromPage(temp.ToString(), list);
                            }
                    }
                }
            }
    }
}