using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dcsg.Managers
{
    public static class Fonts
    {
        static XmlDocument FontDocument;
        static List<Font> LoadedFonts = new List<Font>();
        public static void LoadFont(string fontName)
        {
            if (!System.IO.File.Exists(DCSG.Contents.RootDirectory + "\\" + fontName + ".fnt"))
                throw new System.IO.FileNotFoundException("No font file associated with " + fontName);
            if (FontDocument == null)
                FontDocument = new XmlDocument();
            FontDocument.Load(DCSG.Contents.RootDirectory + "\\" + fontName + ".fnt");
            XmlNode font = FontDocument.FirstChild;
            if (font.Name != "font")
                throw new XmlException("Wrong first node in font file");

            Font nf = new Font();
            foreach (XmlElement item in font.ChildNodes)
            {
                switch (item.Name)
                {
                    case "info":
                        nf.Facename = item.GetAttribute("face");
                        nf.Size = Convert.ToInt32(item.GetAttribute("size"));
                        break;
                    case "common":

                        break;
                    case "chars":
                        int count = Convert.ToInt32(item.GetAttribute("count"));
                        for (int i = 0; i < item.ChildNodes.Count; i++)
                        {
                            int w = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("width"));
                            int h = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("height"));
                            int x = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("xadvance"));
                            int yp = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("y"));
                            int xp = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("x"));
                            int yoff = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("yoffset"));
                            int xoff = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("xoffset"));
                            int id = Convert.ToInt32(((XmlElement)item.ChildNodes[i]).GetAttribute("id"));
                            string let = ((XmlElement)item.ChildNodes[i]).GetAttribute("letter");
                            FontLetter fl = new FontLetter() { id = id, name = let, xadvance = x, xOff = xoff, yOff = yoff, rect = new Microsoft.Xna.Framework.Rectangle(xp, yp, w, h) };
                            nf.Letters.Add(fl);
                        }
                        break;
                    case "kernings":

                        break;
                }
            }
            nf.texture = DCSG.Contents.Load<Texture2D>(fontName);
            nf.Name = new System.IO.FileInfo(DCSG.Contents.RootDirectory + "\\" + fontName + ".fnt").Name.Replace(".fnt", "");
            LoadedFonts.Add(nf);
            Console.WriteLine(nf.Name);
        }
        public static Font GetFont(string fontName)
        {
            return LoadedFonts.FirstOrDefault(s => s.Name == fontName);
        }
    }
    public class Font
    {
        int _writerPos = 0;
        public Texture2D texture { get; set; }
        public string Facename { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int XOffset { get { return _writerPos; } }
        public List<FontLetter> Letters { get; set; }
        public Font()
        {
            Letters = new List<FontLetter>();
        }
        public void ResetWriter()
        {
            _writerPos = 0;
        }
        public void AdvanceHead(FontLetter fl, float scale = 1f)
        {
            _writerPos += fl.xadvance;
        }
        public FontLetter GetLetter(char let)
        {
            FontLetter fn = Letters.FirstOrDefault(s => s.id == (int)let);
            if (fn == null) { fn = Letters.FirstOrDefault(s => s.id == -1); }
            if (fn == null) { throw new MissingFieldException("Font missing default letter"); }
            return fn;
        }
        public int LengthOfString(string text, float scale = 1f)
        {
            _writerPos = 0;
            for (int i = 0; i < text.Length; i++)
            {
                FontLetter fn = GetLetter(text[i]);
                AdvanceHead(fn);
            }
            return (int)((float)_writerPos * scale);
        }
        //public void WriteString(string text, Microsoft.Xna.Framework.Graphics.SpriteBatch sb,
    }
    public class FontLetter
    {
        public int xOff { get; set; }
        public int yOff { get; set; }
        public int xadvance { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Rectangle rect { get; set; }
    }
}
