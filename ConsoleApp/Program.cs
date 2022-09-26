using Core;

const string pathToValidOutput = "valid_output.txt";
const string pathToInvalidOutput = "invalid_output.txt";
const string Uri = "http://links.qatl.ru/";

var links = LinkExtracter.ExtractAllLinks(Uri);

var pages = links.Select(l => new WebPageState(l));

var validOutput = new List<string>();
var invalidOutput = new List<string>();
foreach (var page in pages)
{
    if (UrlValidator.Validate(page))
        validOutput.Add($"link: {page.Uri}, status: {page.StatusCode}");
    else invalidOutput.Add($"link: {page.Uri}, status: {page.StatusCode}");
}

FileManager.WriteLineIntoFile(pathToValidOutput,validOutput);
FileManager.WriteLineIntoFile(pathToInvalidOutput,invalidOutput);