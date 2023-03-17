using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private int[,] map;
        private Texture2D tileTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = new MapGenerator(20, 20, 123).GenerateMap();

            tileTexture = new Texture2D(GraphicsDevice, 1, 1);
            tileTexture.SetData(new Color[] { Color.White });
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 0)
                    {
                        spriteBatch.Draw(tileTexture, new Vector2(x * 32, y * 32), Color.Green);
                    }
                    else
                    {
                        spriteBatch.Draw(tileTexture, new Vector2(x * 32, y * 32), Color.Brown);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}