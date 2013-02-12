Gematria
========

A .net Class Library for calculating the Gematria value of strings of Hebrew text, or convert numbers into Hebrew Text.

> Gematria or gimatria (Hebrew: גימטריא/גימטריה‎ gēmaṭriyā) is a traditional Jewish system of assigning numerical value to a word or phrase ([Wikipedia](http://en.wikipedia.org/wiki/Gematria))

You can learn more about Gematria in one of these sites ([1](http://en.wikipedia.org/wiki/Gematria), [2](http://www.i18nguy.com/unicode/hebrew-numbers.html))

This library exposes the following methods, all available through the static `Calculator` class:

* `GetGematriaValue`
  * Calculates the gematria value for all Hebrew letters in the given string. 
  * Ignores all characters that are not Hebrew letters.
* `GetNumericGematriaValue`
  * Calculates the gematria value for a string that is intended to represent a number (example: a year in the Hebrew calendar or page in a Hebrew book).
  * This function expects that the given string will contain only one word, and will throw an error if more than one word is included 
  * (this is because a string of Hebrew characters representing a number will never consist of multiple words).
  * Ignores non-Hebrew characters and punctuation in the given word. 
* `ConvertToGematriaNumericString`
  * Convert a number into its Gematria Numeric Representation

As explained in the links above, there are different systems that can be used for translating Hebrew letters into numeric equivalents. The Gematria library allows use of the following four methods:

1. Absolute Value (מספר הכרחי): 
  * Alef (א) through Tet (ט) are 1-9
  * Yud (י) through Tzadik (צ) are 10-90, increasing in increments of 10
  * Kuf (ק) through Tav (ת) are 100-400, increasing in increments of 100
  * The five final forms (sofiyot | סופיות) in the alphabet are given the equivalent values to their non-final analogs
  * This is the most standard method, used by default
2. Absolute Alternate Value
  * The same as the Absolute Value, except that the Final Forms continue from 500-900
3. Ordinal Value (מספר סידורי)
  * Alef starts at 1. Each following letter continues in sequence, with the final forms continuing the sequence (Tav = 22, Final Tzadik = 27)
4. Reduced Value (מספר קטן)
  * Calculated the value of each letter using the absolute system, truncating all zeros
  * Leads to a sequence of values in order of letters: 1-9, 1-9, 1-9

All code is (c) Ellis Web Development, Ltd (http://ellisweb.net) and is released under the MIT License (http://opensource.org/licenses/MIT)
For more information, please contact Yaakov Ellis (yaakov@ellisweb.net)