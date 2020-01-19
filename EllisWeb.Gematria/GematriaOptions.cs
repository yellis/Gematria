namespace EllisWeb.Gematria
{
    /// <summary>
    /// Options when converting a number into its Gematria representation
    /// </summary>
    public class GematriaOptions
    {
        /// <summary>
        /// Should separators between thousands-groupings be included in the string that is returned
        /// </summary>
        public bool IncludeSeparators { get; set; } = true;

        /// <summary>
        /// Value to use separating between thousands-groupings. Defaults to a single quote (')
        /// </summary>
        public char ThousandsSeparator { get; set; } = '\'';

        /// <summary>
        /// Value to use separating between the tens and single digit letters. Defaults to a double quote (")
        /// </summary>
        public char TensSeparator { get; set; } = '"';

        /// <summary>
        /// <para>When the result is a single char, Should we place a single quote (true) or a double-quote (false)</para>
        /// <para>The default is true - adding a single quote</para>
        /// </summary>
        public bool AddQuoteAfterSingleChar { get; set; } = true;
    }
}