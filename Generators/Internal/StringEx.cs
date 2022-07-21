using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diabase.StrongTypes.Generators.Internal
{
    internal static class StringEx
    {
        public enum Required
        {
            No,
            Yes
        }

        public enum TextSpanMode
        {
            Inclusive,
            Exclusive,
            OuterLineBreaks,
            InnerLineBreaks
        }

        public static string RemoveDefines(this string s)
        {
            return s.RemoveLinesStartingWith("#define ");
        }

        public static string Undefine(this string s, string name)
        {
            var search = $"#define {name}";
            var replace = $"#define not_{name}";
            //0s.IndexOf(search, Required.Yes); // ensure it exists
            return s.Replace(search, replace);
        }

        public static string RemoveLinesStartingWith(this string s, string startsWith)
        {
            var lines = s.AsLines()
                .Where(l => !l.StartsWith(startsWith));
            return AsDelimited(lines, "\r\n");
        }


        public static IEnumerable<string> AsLines(this string s)
        {
            return s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        }

        public static string AsDelimited(IEnumerable<string> items, string delimiter)
        {
            return items
                .Aggregate(
                    string.Empty,
                    (accum, item) => $"{accum}{item}{delimiter}"
                );
        }

        public static string RemoveBetween(this string s, string start, string end, TextSpanMode mode, Required required = Required.No)
        {
            var span = s.FindSpanBetween(start, end, mode, required);
            if (span.Length == 0) return s;
            var result = s.Remove(span.Start, span.Length);
            return result.RemoveBetween(start, end, mode, Required.No);
        }

        public static TextSpan FindSpanBetween(this string s, string start, string end, TextSpanMode mode, Required required = Required.No)
        {
            TextSpan Find()
            {
                var startIndex = s.IndexOf(start);
                if (startIndex < 0) return new TextSpan();

                startIndex = mode switch
                {
                    TextSpanMode.Exclusive => startIndex + start.Length,
                    TextSpanMode.InnerLineBreaks => s.StartOfNextLineIndex(startIndex),
                    TextSpanMode.OuterLineBreaks => s.StartOfLineIndex(startIndex),
                    _ => startIndex
                };
                if (startIndex < 0) return new TextSpan();

                var endIndex = s.IndexOf(end, startIndex);
                if (endIndex < 0) return new TextSpan();

                endIndex = mode switch
                {
                    TextSpanMode.Inclusive => endIndex + end.Length,
                    TextSpanMode.InnerLineBreaks => s.StartOfLineIndex(endIndex),
                    TextSpanMode.OuterLineBreaks => s.StartOfNextLineOrEndIndex(endIndex),
                    _ => endIndex
                };
                if (endIndex < 0) return new TextSpan();

                return new TextSpan(startIndex, endIndex - startIndex);
            }

            var result = Find();

            if (result.Length == 0 && required == Required.Yes)
                throw new Exception($"{nameof(FindSpanBetween)} failure: One or both search values were not found: '{start}', '{end}'.");
            return result;
        }

        public static int StartOfLineIndex(this string s, int startIndex)
        {
            for (int i = startIndex; i >= 0; i--)
                if (s[i] == '\n') return i + 1;
            return 0;
        }

        public static int StartOfNextLineIndex(this string s, int startIndex)
        {
            for (int i = startIndex; i < s.Length; i++)
                if (s[i] == '\n') return i + 1;
            return -1;
        }

        public static int StartOfNextLineOrEndIndex(this string s, int startIndex)
        {
            for (int i = startIndex; i < s.Length; i++)
                if (s[i] == '\n') return i + 1;
            return s.Length;
        }

        public static int IndexOf(this string s, string value, Required required)
        {
            var i = s.IndexOf(value);
            if (i == -1 && required == Required.Yes) throw new Exception($"{nameof(IndexOf)} failure: '{value}' not found.");
            return i;
        }

        public static bool ContainedIn(this string s, string[] values)
        {
            return values.Contains(s);
        }
    }
}
