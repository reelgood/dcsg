using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace dcsg.Engine
{
    // This entire class should be replaced with a standard importer, like the spriteimporter Ole is making.
    public class Sounds
    {
        static Sounds _mainSound;
        static XmlDocument SoundDocument;
        static Hashtable soundTable;
        public Sounds()
        {
            if (_mainSound != null) { throw new NullReferenceException("Sounds object created twice"); }
            _mainSound = this;

            soundTable = new Hashtable();
            if (!System.IO.File.Exists(DCSG.Contents.RootDirectory + "\\Sounds\\soundloader.xml"))
                throw new System.IO.FileNotFoundException("Soundloader file does not exist!");
            if (SoundDocument == null)
                SoundDocument = new XmlDocument();
            SoundDocument.Load(DCSG.Contents.RootDirectory + "\\Sounds\\soundloader.xml");
            foreach (XmlElement soundNode in SoundDocument.ChildNodes)
            {
                if (soundNode.Name == "sounds")
                {
                    foreach (XmlElement soundEffect in soundNode)
                    {
                        FileStream fs;
                        try
                        {
                            fs = new FileStream(DCSG.Contents.RootDirectory + "\\Sounds\\" + soundEffect.GetAttribute("path") + "\\" + soundEffect.GetAttribute("filename") + "." + soundEffect.GetAttribute("type"), FileMode.Open);
                            SoundEffect se = SoundEffect.FromStream(fs);
                            soundTable.Add(soundEffect.GetAttribute("id").ToLowerInvariant(), se);
                            fs.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Soundeffect not found to load: " + soundEffect.GetAttribute("id"));
                        }
                    }
                }
            }
        }
        public static SoundEffect GetSound(string id)
        {
            if (soundTable.ContainsKey(id.ToLowerInvariant()))
            {
                return (SoundEffect)soundTable[id.ToLowerInvariant()];
            }
            Console.WriteLine("Sound " + id + " does not exist!");
            return null;
        }
        public static void SimplePlay(string id)
        {
            SoundEffect se = GetSound(id);
            if (se == null) { return; }
            se.Play();
        }
    }
}
