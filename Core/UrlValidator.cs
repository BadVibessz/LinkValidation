using System.Net;

namespace Core;

public static class UrlValidator
{
    public static bool Validate(WebPageState pageState)
    {
        pageState.ProcessSuccessful = false;

        var request = (HttpWebRequest)WebRequest.Create(pageState.Uri);
        request.Method = "GET";
        request.Credentials = CredentialCache.DefaultCredentials;
        
        // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
        ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true); 
        
        WebResponse? response = null;

        try
        {
            response = request.GetResponse();

            if (response is HttpWebResponse)
                pageState.StatusCode = (int)((HttpWebResponse)response).StatusCode;
            else if (response is FileWebResponse)
                pageState.StatusCode = (int)HttpStatusCode.OK;

            if (pageState.StatusCode < 400)
                pageState.ProcessSuccessful = true;
        }
        catch (WebException ex)
        {
            var status = ((ex.Response as HttpWebResponse)?.StatusCode).Value;
            pageState.StatusCode = (int)status;

            if ((int)status < 400)
                pageState.ProcessSuccessful = true;
        }
        finally
        {
            if (response is not null)
                response.Close();
        }

        return pageState.ProcessSuccessful;
    }
}