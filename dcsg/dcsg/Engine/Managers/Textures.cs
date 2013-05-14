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
				string textureName = pngfile.Substring(pngfile.LastIndexOf('\\') + 1);
				_loadedTextures.Add(new LoadedTexture(textureName, tex));
				fStream.Close();
			}
		}

		/// <summary>
		/// Finds a texture file depending on filename (usually stored as texture.png/.jpg/.gif
		/// </summary>
		/// <param name="fileName">the file to search for</param>
		/// <param name="caseSensitive">specify if search should be case sensitive</param>
		/// <param name="exactMatch">specify if search should be an exact match as input (remember .png etc)</param>
		/// <returns></returns>
		static public Texture2D Find(string fileName, bool caseSensitive = false, bool exactMatch = false)
		{
			for (int i = 0; i < _loadedTextures.Count; i++)
			{
				if (exactMatch)
				{
					if (_loadedTextures[i].fileName == fileName)
					{
						return _loadedTextures[i].texture;
					}
				}
				else
				{
					if (caseSensitive)
					{
						if (_loadedTextures[i].fileName.Contains(fileName))
						{
							return _loadedTextures[i].texture;
						}
					}
					else
					{
						if (_loadedTextures[i].fileName.ToLower().Contains(fileName.ToLower()))
						{
							return _loadedTextures[i].texture;
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Devides all loaded textures up into sprites for usage by the sprite renderer
		/// </summary>
		public void SetupSprites(string XMLFolder)
		{
			if (_sprites == null) _sprites = new List<Sprite>();

			string [] xmlFiles = Directory.GetFiles(XMLFolder, "*.xml", SearchOption.AllDirectories);
			foreach (string xmlFile in xmlFiles)
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlFile);

				XmlNode sprite = xmlDoc.FirstChild;
				if (sprite.Name == "xml") { sprite = xmlDoc.ChildNodes[1]; }
				if (sprite.Name != "texture") throw new XmlException("Wrong first node in texture file");

				if (sprite.Attributes["filename"] == null)
					throw new XmlException("Texture XML document needs to specify a filename to use");
				string textureFile = sprite.Attributes["filename"].Value;
				

				foreach (XmlNode spriteBase in sprite.ChildNodes)
				{
					// Set the name
					if (spriteBase.Attributes["name"] == null)
						throw new XmlException("error in xml file: " + xmlFile + ". Sprite needs a name attribute");
					string name = spriteBase.Attributes["name"].Value;

					// Helper bools, since rects and vector2's aint nullable...
					bool hasOrigin = false;
					bool hasSourceRect = false;

					// initialize variables we're going to use
					Rectangle sourcerect = Rectangle.Empty;
					Vector2 origin = Vector2.Zero;
					int framecount = -1;
					float framesPerSecond = -1;
					List<AnimationFrame> animationFrames = null;
				
					foreach (XmlNode spriteProps in spriteBase.ChildNodes)
					{
						// <texture>
						//	<sprite>
						//	 <Sourcerect / origin / animaton>
						switch (spriteProps.Name)
						{
							case "sourcerect":
								// Parse rect
								if (spriteProps.Attributes["rect"] == null)
									throw new XmlException("error in xml file: " + xmlFile + ". Spritename: " + name + ". sourcerect needs to be of type rect");

								sourcerect = parseRect(spriteProps.Attributes[0].Value);
								hasSourceRect = true;
								break;
							case "origin":
								// Parse vector2
								if (spriteProps.Attributes["vector2"] == null)
									throw new XmlException("error in xml file: " + xmlFile + ". Spritename: " + name + ". origin needs to be of type vector2");

								origin = parseVector2(spriteProps.Attributes[0].Value);
								hasOrigin = true;
								break;
							case "animation":
								// Parse animation?....
								// if we have an animation node, it HAS to tell us the desired frames per second
								if (spriteProps.Attributes["fps"] == null)
									throw new XmlException("Spritename: " + name + ". Need to assign a desired fps attribute to animation node.");
								framesPerSecond = float.Parse(spriteProps.Attributes["fps"].Value);

								// If the node has children, we have some frames. check subnode and fill inn animationframes
								if (spriteProps.HasChildNodes)
								{
									animationFrames = new List<AnimationFrame>();
									foreach (XmlNode animationFrame in spriteProps.ChildNodes)
									{
										// We need both origin and source rect for each frame, if one is missing this wont work
										if (animationFrame.Attributes["origin"] == null || animationFrame.Attributes["sourcerect"] == null)
											throw new XmlException("Spritename: " + name + ". Need both origin and sourcerect for a non-uniform animation.");

										// Now thats out of the way, parse and add a animation frame.
										Vector2 aOrig = parseVector2(animationFrame.Attributes["origin"].Value);
										Rectangle aRect = parseRect(animationFrame.Attributes["sourcerect"].Value);
										
										animationFrames.Add(new AnimationFrame(aOrig, aRect));
									}
								}
								else // if not it has to have AT LEAST attribute numframes and sourcerect for a uniformanimation
								{
									// If it hasent got a source rect set
									if (!hasSourceRect)
										throw new XmlException("Spritename: " + name + ". Need to set sourcerect before animation when doing a uniform animation.");
									if (spriteProps.Attributes["framecount"] == null)
										throw new XmlException("Spritename: " + name + ". Uniform animations need a framecount attribute.");

									// Get framecount
									framecount = int.Parse(spriteProps.Attributes["framecount"].Value);

									// If origin is not set, just calculate it from the center of sourcerect
									if (!hasOrigin)
									{
										origin = new Vector2(sourcerect.Width / 2, sourcerect.Height / 2);
										hasOrigin = true;
									}

									int sourcerectWidth = sourcerect.Width;
									animationFrames = new List<AnimationFrame>();
									// Now just to add the animationframes based on framecount and the width of the sourcerect
									for (int i = 0; i < framecount; i++)
									{
										animationFrames.Add(new AnimationFrame(origin, sourcerect));
										// Increment X position of the source rect by one width every frame
										sourcerect = new Rectangle(sourcerect.X + sourcerectWidth, sourcerect.Y, sourcerect.Width, sourcerect.Height);
									}
								}
								break;
							default:
								// This is basicly just a "load the whole damn texturefile into a sprite"
								break;
						}
					}

					// we have an animation sprite
					if (animationFrames != null)
					{
						_sprites.Add(new Sprite(Find(textureFile), animationFrames.ToArray(), framesPerSecond));
					}
					else // we have a static sprite
					{
						if (!hasSourceRect)
						{
							sourcerect = Find(textureFile).Bounds;
						}
						
						if (!hasOrigin)
						{
							origin = new Vector2(sourcerect.Width / 2, sourcerect.Height / 2);
							hasOrigin = true;
						}

						_sprites.Add(new Sprite(Find(textureFile), sourcerect, origin));
					}
					
					// Sprite Loop
				}
				
				// Find XML files loop
			}
			// Function
		}

		private Vector2 parseVector2(string str)
		{
			string[] xy = str.Split(',');
			float x = float.Parse(xy[0]);
			float y = float.Parse(xy[1]);
			return new Vector2(x, y);
		}

		private Rectangle parseRect(string str)
		{
			string[] xywh = str.Split(',');
			int x = int.Parse(xywh[0]);
			int y = int.Parse(xywh[1]);
			int w = int.Parse(xywh[2]);
			int h = int.Parse(xywh[3]);
			return new Rectangle(x, y, w, h);
		}
		

		// CLASS
    }
	// NAMESPACE
}