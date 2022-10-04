using System.Net;

namespace Core;

public class WebPageState
{
    public Uri Uri { get; set; }
    public bool ProcessSuccessful { get; set; } = false;
    public string StatusCode { get; set; }


    public WebPageState(Uri uri)
    {
        Uri = uri;
    }

    public WebPageState(string uri)
        : this(new Uri(uri))
    {
    }
}