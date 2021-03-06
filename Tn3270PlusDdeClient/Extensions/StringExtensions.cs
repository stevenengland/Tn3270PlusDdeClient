﻿using System;
using System.Collections.Generic;

namespace StEn.Tn3270PlusDde.Extensions
{
    internal static class StringExtensions
    {
        public static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            if (partLength <= 0)
            {
                throw new ArgumentException("Part length has to be positive.", $"{nameof(partLength)}");
            }

            for (var i = 0; i < s.Length; i += partLength)
            {
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
            }
        }
    }
}
