using System.Text.RegularExpressions;

namespace FileWatcherBackups.Logic.Utility;

public static class PathPatternMatcher
{
    public static bool CompliesToOneOfPatterns(string filePath, IEnumerable<string> patterns)
    {
        bool result = patterns
            .Select(pattern => CompliesToPattern(filePath, pattern))
            .Any(complies => complies);

        return result;
    }

    private static bool CompliesToPattern(string filePath, string pattern)
    {
        var patternRegexText = Regex.Escape(pattern);
        patternRegexText = patternRegexText.Replace("\\*", ".*");

        var patternRegex = new Regex(patternRegexText);

        return patternRegex.IsMatch(filePath);
    }
}
