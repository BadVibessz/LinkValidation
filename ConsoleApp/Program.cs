using Core;

Console.WriteLine("Program started...");

const string pathToValidOutput = "valid_output.txt";
const string pathToInvalidOutput = "invalid_output.txt";
const string uri = "https://tortishnaya.ru/";

var links = new List<string>();

LinkExtracter.BaseUri = new Uri(uri);
LinkExtracter.ExtractLinksFromPage(uri, links);

var pages = links.Select(l => new WebPageState(l));

var validOutput = new List<string>();
var invalidOutput = new List<string>();
foreach (var page in pages)
{
    if (UrlValidator.Validate(page))
        validOutput.Add($"link: {page.Uri}, status: {page.StatusCode}");
    else invalidOutput.Add($"link: {page.Uri}, status: {page.StatusCode}");
}

validOutput.Add($"Link count: {validOutput.Count}\nExecution date: {DateTime.Now.ToString("f")}");
invalidOutput.Add($"Link count: {invalidOutput.Count}\nExecution date: {DateTime.Now.ToString("f")}");

FileManager.WriteLinesIntoFile(pathToValidOutput, validOutput);
FileManager.WriteLinesIntoFile(pathToInvalidOutput, invalidOutput);

Console.WriteLine("Program finished...");
