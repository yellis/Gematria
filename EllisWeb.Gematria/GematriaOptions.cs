namespace EllisWeb.Gematria
{
    /// <summary>
    /// Options when converting a number into its Gematria representation
    /// </summary>
    public class GematriaOptions
    {
        /// <summary>
        /// default value for <see cref="IncludeSeparators"/>
        /// </summary>
        public const bool DEFAULT_SHOULD_INCLUDE_SEPARATORS = true;

        /// <summary>
        /// default value for <see cref="ThousandsSeparator"/>
        /// </summary>
        public const char DEFAULT_THOUSANDS_SEPARATOR = '\'';

        /// <summary>
        /// default value for <see cref="TensSeparator"/>
        /// </summary>
        public const char DEFAULT_TENS_SEPARATOR = '"';

        /// <summary>
        /// default value for <see cref="AddQuoteAfterSingleChar"/>
        /// </summary>
        public const bool DEFAULT_ADD_QUOTE_AFTER_SINGLE_CHAR = true;

        /// <summary>
        /// Should separators between thousands-groupings be included in the string that is returned
        /// </summary>
        public bool IncludeSeparators { get; } = DEFAULT_SHOULD_INCLUDE_SEPARATORS;

        /// <summary>
        /// Value to use separating between thousands-groupings. Defaults to a single quote (')
        /// </summary>
        public char ThousandsSeparator { get; } = DEFAULT_THOUSANDS_SEPARATOR;

        /// <summary>
        /// Value to use separating between the tens and single digit letters. Defaults to a double quote (")
        /// </summary>
        public char TensSeparator { get; } = DEFAULT_TENS_SEPARATOR;

        /// <summary>
        /// <para>When the result is a single char, Should we place a single quote (true) or a double-quote (false)</para>
        /// <para>The default is true - adding a single quote</para>
        /// </summary>
        public bool AddQuoteAfterSingleChar { get; } = DEFAULT_ADD_QUOTE_AFTER_SINGLE_CHAR;

        /// <summary>
        /// new instance of options to convert a number into its Gematria representation
        /// </summary>
        /// <param name="includeSeparators">Should separators between thousands-groupings be included in the string that is returned</param>
        /// <param name="thousandsSeparator">Value to use separating between thousands-groupings. Defaults to a single quote (')</param>
        /// <param name="tensSeparator">Value to use separating between the tens and single digit letters. Defaults to a double quote (")</param>
        /// <param name="addQuoteAfterSingleChar">When the result is a single char, Should we place a single quote (true) or a double-quote (false)</param>
        public GematriaOptions(bool includeSeparators = DEFAULT_SHOULD_INCLUDE_SEPARATORS,
            char thousandsSeparator = DEFAULT_THOUSANDS_SEPARATOR,
            char tensSeparator = DEFAULT_TENS_SEPARATOR,
            bool addQuoteAfterSingleChar = DEFAULT_ADD_QUOTE_AFTER_SINGLE_CHAR)
        {
            IncludeSeparators = includeSeparators;
            ThousandsSeparator = thousandsSeparator;
            TensSeparator = tensSeparator;
            AddQuoteAfterSingleChar = addQuoteAfterSingleChar;
        }
    }
}