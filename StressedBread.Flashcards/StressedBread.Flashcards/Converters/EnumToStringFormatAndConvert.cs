using System.Text.RegularExpressions;

namespace StressedBread.Flashcards.Converters;

internal static class EnumToStringFormatAndConvert
{
    internal static string Convert<TEnum>(TEnum value) where TEnum : Enum
    {
        var name = value.ToString();
        var formattedName = Regex.Replace(name, "(?<!^)([A-Z])", " $1");
        return formattedName;
    }
}