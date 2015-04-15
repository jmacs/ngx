http://www.craftworkgames.com/blog/tutorial-bmfont-rendering-with-monogame/



Step 1: Generate the texture and font file using BMFont 
If you haven’t already done so, download and install the BMFont tool. When you start 
the tool you’ll be presented with a screen that looks like this:

bmfont.jpg

Setup your Font options, they can be found in the Font Settings dialog from the options 
menu. You can choose whatever you settings you prefer here. Next, you need to select 
the characters you want to generate. Typically this will include all of the top half 
of the character set as shown in the screenshot.

Change the output format to XML in the Export Options dialog. This is important because 
we are going to be using the XML serializer to load our font. While your in the Export 
Options dialog you might also want you change your texture format, I prefer png. To 
keep things simple, I also like to make sure my font is going to fit on one texture. 
You might need to play around with the texture width and height a little to get something 
you’re comfortable with.

Output your font file and texture using the Save bitmap font as option.. You should end 
up with a .fnt file containing some XML and an image with the same name followed by _0. 
The font generator tightly packs the glyphs into the texture atlas to minimize the 
texture size.

—

Step 2: Add the BmFont XML Serializer code to your project 
You can find the BmFont Serializer code on pastebin. It makes me feel a little 
uncomfortable that the code doesn’t live in a proper code repository, but I’ll keep 
a copy of it on my server in case it ever disappears.

The code consists of a handful of classes used for serialization including:

FontChar
FontCommon
FontFile
FontInfo
FontKerning
FontLoader
FontPage

For the most part, you won’t need to change the code at all. If you’re like most 
programmers you’ll probably fiddle with it a little and change the namespaces to your 
liking. I used the refactoring tools in MonoDevelop to split each class into it’s own file.

At the most basic level, loading a font file is easy. If you’ve put your font file and 
texture in the Content folder you can use the following code on Windows.

var fontFilePath = Path.Combine(Content.RootDirectory, "CourierNew32.fnt");
var fontFile = FontLoader.Load(fontFilePath);
var fontTexture = Content.Load<Texture2D>("CourierNew32_0.png");
But this won’t work when your using MonoGame to deploy to other platforms like Android 
or iOS. The solution is to modify or create a new FontLoader.Load method that takes a 
Stream instead of a file path like this:

public static FontFile Load(Stream stream)
{
        XmlSerializer deserializer = new XmlSerializer(typeof(FontFile));
        FontFile file = (FontFile) deserializer.Deserialize(stream);
        return file;
}
Now the code to load the font can use a TitleContainer.OpenStream method to load 
the file in a more generic way. MonoGame supports loading files on any platform 
using the same path through the content manager or the TitleContainer.

var fontFilePath = Path.Combine(Content.RootDirectory, "CourierNew32.fnt");
using(var stream = TitleContainer.OpenStream(fontFilePath))
{
    var fontFile = FontLoader.Load(stream);
    var fontTexture = Content.Load<Texture2D>("CourierNew32_0.png");
    // textRenderer initialization will go here
    stream.Close();
}
—

Step 4: Implementing a basic text renderer 
At this stage you probably just want to get some text on the screen as fast as 
possible. Fortunately, a basic text renderer isn’t hard to implement. Here’s one 
I prepared earlier:

public class FontRenderer
{
        public FontRenderer (FontFile fontFile, Texture2D fontTexture)
        {
                _fontFile = fontFile;
                _texture = fontTexture;
                _characterMap = new Dictionary<char, FontChar>();

                foreach(var fontCharacter in _fontFile.Chars)
                {
                        char c = (char)fontCharacter.ID;
                        _characterMap.Add(c, fontCharacter);
                }
        }

        private Dictionary<char, FontChar> _characterMap;
        private FontFile _fontFile;
        private Texture2D _texture;
        public void DrawText(SpriteBatch spriteBatch, int x, int y, string text)
        {
                int dx = x;
                int dy = y;
                foreach(char c in text)
                {
                        FontChar fc;
                        if(_characterMap.TryGetValue(c, out fc))
                        {
                                var sourceRectangle = new Rectangle(fc.X, fc.Y, fc.Width, fc.Height);
                                var position = new Vector2(dx + fc.XOffset, dy + fc.YOffset);

                                spriteBatch.Draw(_texture, position, sourceRectangle, Color.White);
                                dx += fc.XAdvance;
                        }
                }
        }
}
The FontRenderer class takes a FontFile from the BMFont loader and a MonoGame/XNA 
Texture2D as input. It creates a dictionary mapping for each character in the font 
for fast lookup during rendering. The DrawText method takes a MonoGame/XNA SpriteBatch, 
a postion and string to render. It loops through each character in the string and looks 
up the info for each character. It creates a source rectangle on the texture to find 
the appropriate glyph and offsets the character position because of the tight texture 
packing. The font also specifies how many pixels to advance the cursor for each character.

You can create a new instance of the FontRenderer in your LoadContent method.

_fontRenderer = new FontRenderer(fontFile, fontTexture);

And call the DrawText method from your main Draw method.

_fontRenderer.DrawText(_spriteBatch, 50, 50, "Hello World!");

This implementation is not very sophisticated, it doesn’t take into account any 
kerning, multiple lines or word wrapping. However, all of these features can be 
implemented from the data loaded from the BMFont XML file.