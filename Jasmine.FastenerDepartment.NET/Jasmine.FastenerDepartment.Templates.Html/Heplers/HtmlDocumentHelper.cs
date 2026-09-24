using System.Text;

namespace Jasmine.FastenerDepartment.Templates.Html.Heplers;

internal static class HtmlDocumentHelper
{
    public static void InsertToEnd(StringBuilder sb, string str, string lastSubstring = null)
    {
        ArgumentNullException.ThrowIfNull(sb);

        if (lastSubstring == null)
        {
            sb.Append(str);
            return;
        }

        var index = sb.Length - 1;
        var length = lastSubstring.Length - 1;
        var lastSubstringIndex = length;

        while (index >= 0)
        {
            if (lastSubstring[lastSubstringIndex] == sb[index])
            {
                lastSubstringIndex--;
                if (lastSubstringIndex < 0)
                    break;
            }
            else
            {
                lastSubstringIndex = length;
            }

            index--;
        }

        sb.Insert(index, str);
    }
}
