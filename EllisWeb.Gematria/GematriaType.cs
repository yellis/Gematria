namespace EllisWeb.Gematria
{
    /// <summary>
    /// Enumeration of different Gematria types
    /// </summary>
    public enum GematriaType
    {
        /// <summary>
        /// Absolute Method (מספר הכרחי).  Uses full numerical value of the twenty-two letters. Default method.
        /// </summary>
        Absolute,
        /// <summary>
        /// Absolute Alternate (מספר גדול). Same as absolute, with the final forms (sofiyot) continuing the numeric sequence.
        /// </summary>
        AbsoluteAlternate,
        /// <summary>
        /// Absolute Method, without any of the five sofiyot letters
        /// </summary>
        AbsoluteNoSofiyot,
        /// <summary>
        /// Reduced Value (Mispar Katan - מספר קטן). Takes the first digit from the number for each character as defined in the Absolute method.
        /// </summary>
        Reduced,
        /// <summary>
        /// Ordinal Value (Mispar Siduri - מספר סידורי): each letter its order number in the alphabet. א=1, ב=2, כ=11, ת=22, ץ=27
        /// </summary>
        Ordinal
    }
}