using System.Collections.Generic;

namespace NgxLib.Text
{
    public class CharacterMap : Dictionary<char, FontChar>
    {
        public CharacterMap(FontFile fontFile)
        {
            foreach (var fontCharacter in fontFile.Chars)
            {
                var c = (char)fontCharacter.ID;
                Add(c, fontCharacter);
            }
        }
    }
}