using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EllisWeb.Gematria
{
    /// <summary>
    /// The Gematria Calculator. Base for main functionality
    /// </summary>
    public static class Calculator
    {
        static Calculator()
        {
            ForceNumericStrictMode = false; // set the default
        }

        /// <summary>
        /// Calculates the gematria value for all Hebrew letters in the given string.
        /// Ignores all characters that are not Hebrew letters.
        /// </summary>
        /// <param name="sourceString">String to evaluate</param>
        /// <param name="gematriaType"><see cref="T:EllisWeb.Gematria.GematriaType"/> to use for calculation (defaults to Absolute)</param>
        /// <returns></returns>
        public static long GetGematriaValue(string sourceString, GematriaType gematriaType = GematriaType.Absolute)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                throw new ArgumentNullException("sourceString");
            }
            long value = 0;
            var dict = LookupFactory.GetDictionary(gematriaType);
            foreach (char c in sourceString)
            {
                if (dict.ContainsKey(c))
                {
                    value += dict[c];
                }
            }
            return value;
        }

        /// <summary>
        /// Should strict mode always be used when calculating numeric values (defaults to false). When set to true, 
        /// </summary>
        public static bool ForceNumericStrictMode { get; set; }

        private static readonly Dictionary<string, int> KnownNumericValues = new Dictionary<string, int>
                                                                             {
                                                                                 {"רחצ", 298},
                                                                                 {"ער", 270}
                                                                             };

        /// <summary>
        /// Calculates the gematria value for a string that is intended to represent a number (example: a year in the Hebrew calendar or page in a Hebrew book).
        /// This function expects that the given string will contain only one word, and will throw an error if more than one word is included 
        /// (this is because a string of Hebrew characters representing a number will never consist of multiple words).
        /// Ignores non-Hebrew characters and punctuation in the given word. 
        /// </summary>
        /// <param name="sourceString">The string to evaluate</param>
        /// <param name="gematriaType"><see cref="T:EllisWeb.Gematria.GematriaType"/> to use for calculation (defaults to Absolute)</param>
        /// <param name="isStrictMode">
        /// Should the numeric gematria be evaluated with Strict Mode turned on. Defaults to the global setting 
        /// defined in <see cref="ForceNumericStrictMode"/>. When true this will throw a <see cref="FormatException"/> whenever the numbers at 
        /// the end of the string that are under 100 (ק) are not included in descending numeric order, and do not appear on the exceptions list.
        /// </param>
        /// <returns>Number equal to the numeric gematria value of the string provided</returns>
        /// <remarks>
        /// This function will infer the division between thousands-groupings of the number by using the following rule:
        /// Evaluate characters one at a time. It is expected that gematria values within a thousands-grouping will always be the same or equal to the previous value.
        /// If a value is encountered that is greater than the previous value, it signifies the start of a new thousands-grouping.
        /// </remarks>
        public static long GetNumericGematriaValue(string sourceString, GematriaType gematriaType = GematriaType.Absolute, bool? isStrictMode = null)
        {
            sourceString = sourceString.Trim();

            bool currentStrictMode = isStrictMode ?? ForceNumericStrictMode;
            if (currentStrictMode && KnownNumericValues.ContainsKey(sourceString))
            {
                return KnownNumericValues[sourceString];
            }

            if (Regex.IsMatch(sourceString, @"[\s]"))
            {
                throw new ArgumentException("Source string contains more than one word", "sourceString");
            }

            var dict = LookupFactory.GetDictionary(gematriaType);

            List<List<int>> numberStacks = new List<List<int>>();
            var currentStack = new List<int>();
            numberStacks.Add(currentStack);

            int prevNum = 0;
            foreach (char c in sourceString)
            {
                if (dict.ContainsKey(c))
                {
                    int currentVal = dict[c];
                    if (currentVal > prevNum && currentStack.Any())
                    {
                        currentStack = new List<int>();
                        numberStacks.Add(currentStack);
                    }
                    currentStack.Add(currentVal);
                    prevNum = currentVal;
                }
            }

            // At this point, the first number stack is the highest thousands-grouping. Need to reverse them in order to go from lowest to highest when evaluating.
            numberStacks.Reverse();

            // Go through the number stacks. Multiply the sum of each stack by 1000^X where X is the zero-index value of the current stack
            int currentStackIndex = 0;
            long value = 0;
            bool inHundreds = false;
            long maxStackSum = 0;
            foreach (List<int> numberStack in numberStacks)
            {
                long stackSum = numberStack.Sum();
                if (currentStrictMode)
                {
                    numberStack.Reverse(); // need to reverse the current stack, in order to preserve the order of items being evaluated
                    foreach (var number in numberStack)
                    {
                        if (number >= 100)
                        {
                            inHundreds = true;
                        }
                        if (!inHundreds && number < maxStackSum)
                        {
                            throw new FormatException("In Strict Mode, trailing values less than 100 (ק) must appear in the proper order");
                        }
                        maxStackSum = Math.Max(maxStackSum, number);
                    }
                }
                var stackMultiplier = Math.Pow(1000, currentStackIndex++);
                var adjustedStackSum = Convert.ToInt64(stackSum*stackMultiplier);
                value += adjustedStackSum;
            }

            return value;
        }

        /// <summary>
        /// Convert a number into its Gematria Numeric representation
        /// </summary>
        /// <param name="number">The non-negative number to evaluate</param>
        /// <param name="includeSeparators">Should separators between thousands-groupings be included in the string that is returned</param>
        /// <param name="thousandsSeparator">Value to use separating between thousands-groupings. Defaults to a single quote (')</param>
        /// <param name="tensSeparator">Value to use separating between the tens and single digit letters. Defaults to a double quote (")</param>
        /// <example>
        /// 8 ==> ח
        /// 15 ==> ט"ו
        /// 245 ==> רמ"ה
        /// 5,767 ==> ה'תשס"ז
        /// 1,024,999 ==> א'כד'תתרצ"ט
        /// </example>
        /// <remarks>
        /// Will evaluate each thousands-grouping separately, inserting separators if needed.
        /// A value of 15 will always be represented as ט"ו and 16 will be represented as ט"ז, following Jewish custom. 
        /// </remarks>
        /// <returns>Gemtria Numeric representation of given number</returns>
        public static string ConvertToGematriaNumericString(long number, bool includeSeparators = true, char thousandsSeparator = '\'', char tensSeparator = '"')
        {
            if (number == 0)
            {
                return string.Empty;
            } else if (number < 0)
            {
                throw new ArgumentException("Number is less than zero", "number");
            }
            var originalNumber = number;

            // Separate number into groupings of thousands, each one to be evaluated separately
            List<int> numberGroupings = new List<int>();
            while (number > 0)
            {
                int currentGrouping = Convert.ToInt32(number % 1000);
                numberGroupings.Add(currentGrouping);
                number = number - currentGrouping;
                number = number / 1000;
            }

            // Number groupings now have the smallest groupings (0-999) first, and the largest groupings last. 
            // Need to evaluate the groupings in reverse order so that the highest grouping goes first
            numberGroupings.Reverse();

            // Evaluate each grouping, appending its Gematria representation to the string. Add in separators if needed
            StringBuilder str = new StringBuilder();
            foreach (int numberGrouping in numberGroupings)
            {
                if (includeSeparators && str.Length > 0)
                {
                    str.Append(thousandsSeparator);
                }
                string groupingStr = GetNumericString(numberGrouping);
                str.Append(groupingStr);
            }

            // If needed, add in final quotation separator between tens and singles letter
            if (includeSeparators && originalNumber >= 10)
            {
                // add in a quotation separator between the second-to-last and last characters
                str.Insert(str.Length - 1, tensSeparator);
            }
            
            return str.ToString();
        }

        /// <summary>
        /// Gives the string representation of a number up between 1 and 999
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string GetNumericString(int number)
        {
            if (number < 1 || number > 999)
            {
                throw new ArgumentException("Number not between 1 and 999");
            }
            var dict = LookupFactory.GetDictionary(GematriaType.AbsoluteNoSofiyot);
            // Remove sofiyot letters to avoid dictionary collissions. These are never used in numeric strings
            var reverseDict = dict.ToDictionary(k => k.Value, v => v.Key); // create a dict looking up letter by value
            var valueList = dict.Select(x => x.Value).ToList(); // get list of all available values
            valueList.Sort();
            valueList.Reverse(); // get value list in reverse order - highest number first. Speeds up evaluations.
            
            StringBuilder str = new StringBuilder();
            while (number > 0)
            {
                // first handle special cases
                if (number == 15)
                {
                    str.Append("טו");
                    break;
                } else if (number == 16)
                {
                    str.Append("טז");
                    break;
                }
                var nextValToUse = valueList.Where(x => x <= number).Max();
                str.Append(reverseDict[nextValToUse]);
                number = number - nextValToUse;
            }
            return str.ToString();
        }

        /// <summary>
        /// Strip separator characters (single and double-quotes, appostrophes) from a string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="stripSpaces"></param>
        /// <returns></returns>
        public static string StripSeparatorCharacters(string str, bool stripSpaces = true)
        {
            string pattern = stripSpaces ? "[`'\" ]" : "[`'\"]";
            return Regex.Replace(str, pattern, "");
        }
    }
}
