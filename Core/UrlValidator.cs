using System.Net;

namespace Core;

public static class UrlValidator
{
    public static bool Validate(WebPageState pageState)
    {
        pageState.ProcessSuccessful = false;

        var request = (HttpWebRequest)WebRequest.Create(pageState.Uri);
        request.Method = "GET";
        WebResponse? response = null;

        try
        {
            response = request.GetResponse();

            if (response is HttpWebResponse)
                pageState.StatusCode = ((HttpWebResponse)response).StatusCode.ToString();
            else if (response is FileWebResponse)
                pageState.StatusCode = HttpStatusCode.OK.ToString();

            if (pageState.StatusCode.Equals(HttpStatusCode.OK.ToString()))
                pageState.ProcessSuccessful = true;
        }
        catch (Exception ex)
        {
            pageState.StatusCode = ex.Message.Split(':').Last();
        }
        finally
        {
            if (response is not null)
                response.Close();
        }

        return pageState.ProcessSuccessful;
    }
}