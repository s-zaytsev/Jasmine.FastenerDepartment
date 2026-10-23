using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Templates.Emails.Constants;

internal static class CommonConstants
{
    private readonly static IReadOnlyDictionary<int, LocalizedString> POSITIONS = new Dictionary<int, LocalizedString>
    {
        {0, new("positions", "позиций") },
        {1, new("position", "позиция") },
        {2, new("positions", "позиции") },
        {3, new("positions", "позиции") },
        {4, new("positions", "позиции") },
        {5, new("positions", "позиций") },
        {6, new("positions", "позиций") },
        {7, new("positions", "позиций") },
        {8, new("positions", "позиций") },
        {9, new("positions", "позиций") },
        {11, new("positions", "позиций") }
    };

    internal readonly static LocalizedString OTHERS = new("Others", "Остальное");

    internal readonly static string INVISIBLE_SPACES =
        string.Join("", Enumerable.Repeat("&#8204;&nbsp;&zwnj;&nbsp;&thinsp;&zwnj;&nbsp;", 200));

    internal static LocalizedString GetPositionText(int position)
    {
        if (POSITIONS.TryGetValue(position, out LocalizedString text))
            return text;

        var count = position % 10;
        return POSITIONS[count];
    }
}
