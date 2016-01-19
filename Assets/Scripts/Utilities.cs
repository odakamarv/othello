public class Utilities
{
    // テキストフォーマットに黒白挿入
    public static string ReplaceTextWithColorString(string replaceFromText, string replaceToText)
    {
        return replaceFromText.Replace("$", replaceToText);
    }
}