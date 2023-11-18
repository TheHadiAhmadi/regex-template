using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

string template = @"
<div class=""flex"">
    <Component dynamic attr1=""value1"" attr2=""value2"" attr3></Component>
    <Component dynamic battr1=""bvalue1"" battr2=""bvalue2"" attr3></Component>
</div>";

List<Dictionary<string, object>> expected = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                { "attr1", "value1" },
                { "attr2", "value2" },
                { "attr3", true}
            },
            new Dictionary<string, object>
            {
                { "battr1", "bvalue1" },
                { "battr2", "bvalue2" },
                { "attr3", true}

            }
        };

Run(template).ForEach(attributes =>
{
    foreach (var attribute in attributes)
    {
        Console.WriteLine($"{attribute.Key}: {attribute.Value}");
    }
    Console.WriteLine();
});

// Compare the result with the expected output
// Note: For simplicity, this comparison assumes the order of dictionaries in the lists is the same.
Console.WriteLine("Expected:");
foreach (var attributes in expected)
{
    foreach (var attribute in attributes)
    {
        Console.WriteLine($"{attribute.Key}: {attribute.Value}");
    }
    Console.WriteLine();
}



static List<Dictionary<string, object>> Run(string template)
{
    List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

    Regex regex = new Regex(@"<\s*([a-zA-Z0-9]+)\s*dynamic\s*([^>]*)\s*>", RegexOptions.Multiline);
    MatchCollection matches = regex.Matches(template);

    foreach (Match match in matches)
    {
        string attributesStr = match.Groups[2].Value;
        Dictionary<string, object> attributes = new Dictionary<string, object>();

        Regex attributePattern = new Regex(@"(\w+)(?:=""([^""]*)"")?", RegexOptions.Multiline);
        MatchCollection attributeMatches = attributePattern.Matches(attributesStr);

        foreach (Match attributeMatch in attributeMatches)
        {
            string key = attributeMatch.Groups[1].Value;
            string value = attributeMatch.Groups[2].Success ? attributeMatch.Groups[2].Value : true.ToString();

            attributes[key] = value;
        }

        result.Add(attributes);
    }

    return result;
}
