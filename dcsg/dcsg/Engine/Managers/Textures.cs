using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Xml;

namespace dcsg.Engine
{
	struct LoadedTexture
	{
		public string fileName;
		public Texture2D texture;
		public LoadedTexture(string FileName, Texture2D Texture)
		{
			this.fileName = FileName;
			this.texture = Texture;
		}
	}

    public class Textures
    {
		private static List<Sprite> _sprites = new List<Sprite>();
		private static List<LoadedTexture> _loadedTextures = new List<LoadedTexture>();
        private static Textures mainTex;
		public static Texture2D pointerDefault;
		public static Texture2D NullTexture;

        public Textures()
        {
            if (mainTex != null) { throw new NullReferenceException("Textures object created twice"); }

            mainTex = this;
            NullTexture = new Texture2D(DCSG.MainObject.GraphicsDevice, 1, 1);
            pointerDefault = DCSG.Contents.Load<Texture2D>("Textures\\pointer");


			LoadTextures(DCSG.textureDir, "*.png");
			SetupSprites(DCSG.textureDir);
        }
		
		/// <summary>
		/// Loads all textures at a desired path, that match the specified search pattern
		/// </summary>
		public void LoadTextures(string TextureFolder, string searchPattern)
		{
			// Load inn all textures and place them in some sort of list
			foreach (string pngfile in Directory.GetFiles(TextureFolder, searchPattern, SearchOption.AllDirectories))
			{
				FileStream fStream = new FileStream(pngfile, FileMode.Open);
				Texture2D tex = Texture2D.FromStream(DCSG.MainObject.GraphicsDevice, fStream);
				_loadedTextures.Add(new LoadedTexture(pngfile, tex));
				fStream.Close();
			}
		}

		/// <summary>
		/// Devides all loaded textures up into sprites for usage by the sprite renderer
		/// </summary>
		public void SetupSprites(string XMLFolder)
		{
			string [] xmlFiles = Directory.GetFiles(XMLFolder, "*.xml", SearchOption.AllDirectories);
			foreach (string xmlFile in xmlFiles)
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFile);

				XmlNode sprite = xmlDoc.FirstChild;
				if (sprite.Name == "xml") { sprite = xmlDoc.ChildNodes[1]; }
				if (sprite.Name != "texture") throw new XmlException("Wrong first node in texture file");

				foreach (XmlNode node in sprite.ChildNodes)
				{
					

					foreach (XmlNode subNode in node.ChildNodes)
					{
				
					}
					
				}

	
			}
		}
    }
}

/*
 * Load inn all big texture files
 * Read XML documentation for what sprite goes where, aswell as animation information
 * Assign Texture and AnimationTexture classes based on XML documentation
 * Need a list/hashtable of all textures
*/
