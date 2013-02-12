using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllisWeb.Gematria
{

    public static class LookupFactory
    {
        private static readonly Dictionary<GematriaType, Dictionary<char,int>> lookupDict = new Dictionary<GematriaType, Dictionary<char, int>>();

        /// <summary>
        /// Retrieve a letter lookup dictionary for a given calculation method
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<char, int> GetDictionary(GematriaType type)
        {
            // only need to generate each type of dictionary once
            if (!lookupDict.ContainsKey(type))
            {
                lookupDict[type] = GenerateDictionary(type);
            }
            return lookupDict[type];
        }

        private static Dictionary<char, int> GenerateDictionary(GematriaType type)
        {
            Dictionary<char, int> dict;
            if (type == GematriaType.Absolute)
            {
                dict = new Dictionary<char, int>();
                dict.Add('א', 1);
                dict.Add('ב', 2);
                dict.Add('ג', 3);
                dict.Add('ד', 4);
                dict.Add('ה', 5);
                dict.Add('ו', 6);
                dict.Add('ז', 7);
                dict.Add('ח', 8);
                dict.Add('ט', 9);
                dict.Add('י', 10);
                dict.Add('כ', 20);
                dict.Add('ל', 30);
                dict.Add('מ', 40);
                dict.Add('נ', 50);
                dict.Add('ס', 60);
                dict.Add('ע', 70);
                dict.Add('פ', 80);
                dict.Add('צ', 90);
                dict.Add('ק', 100);
                dict.Add('ר', 200);
                dict.Add('ש', 300);
                dict.Add('ת', 400);
                dict.Add('ך', 20);
                dict.Add('ם', 40);
                dict.Add('ן', 50);
                dict.Add('ף', 80);
                dict.Add('ץ', 90);

            }
            else // start by calculating the Absolute method. All other methods can be derived from this.
            {
                dict = new Dictionary<char, int>(GenerateDictionary(GematriaType.Absolute));
                switch (type)
                {
                    case GematriaType.AbsoluteAlternate:
                         // copy contents of absolute dict
                        // just switch the sofiyot
                        dict['ך'] = 500;
                        dict['ם'] = 600;
                        dict['ן'] = 700;
                        dict['ף'] = 800;
                        dict['ץ'] = 900;
                        break;
                    case GematriaType.AbsoluteNoSofiyot:
                        dict.Remove('ך');
                        dict.Remove('ם');
                        dict.Remove('ן');
                        dict.Remove('ף');
                        dict.Remove('ץ');
                        break;
                    case GematriaType.Reduced:
                        dict = new Dictionary<char, int>(GenerateDictionary(GematriaType.AbsoluteAlternate)); // copy contents of alternate dict
                        // go through all items and set to mod 10 of existing value
                        var keysToModify = dict.Where(x => x.Value > 9).Select(x => x.Key).ToList();
                        foreach (char itemKey in keysToModify)
                        {
                            var val = dict[itemKey];
                            if (val%100 == 0) // hundreds
                            {
                                val = val/100;
                            }
                            else if (val%10 == 0) // tens
                            {
                                val = val / 10;
                            } // if is not multiple of 10, then is in range of 1-9, so just use the number
                            dict[itemKey] = val;
                        }
                        break;
                    case GematriaType.Ordinal:
                        int index = 1;
                        keysToModify = dict.Select(x => x.Key).ToList();
                        foreach (char key in keysToModify)
                        {
                            dict[key] = index++;
                        }
                        break;
                }
            }
            return dict;
        }
    }
}
