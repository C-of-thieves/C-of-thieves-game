using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonogameGame;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _mapTexture;

    private Player _player;
    private List<Enemy> _enemies;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    private Texture2D GenerateMapTexture(int width, int height, float scale)
    {
        Texture2D texture = new Texture2D(GraphicsDevice, width, height);
        Color[] colorData = new Color[width * height];

        float[,] noiseMap = PerlinNoiseGenerator.GenerateNoiseMap(width, height, scale);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                colorData[y * width + x] = new Color(value, value, value);
            }
        }

        texture.SetData(colorData);
        return texture;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        int mapWidth = 256;
        int mapHeight = 256;
        float scale = 25f;
        _mapTexture = GenerateMapTexture(mapWidth, mapHeight, scale);

        Texture2D playerTexture = Content.Load<Texture2D>("player");
        _player = new Player(new Vector2(100, 100), 100, playerTexture);

        Texture2D shipTexture = Content.Load<Texture2D>("player");
        Texture2D monsterTexture = Content.Load<Texture2D>("player");
        _enemies = new List<Enemy>
        {
            new Enemy(new Vector2(200, 200), 50, shipTexture),
            new Enemy(new Vector2(300, 300), 30, monsterTexture)
        };
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.Update(gameTime);

        foreach (Enemy enemy in _enemies)
        {
            enemy.Update(gameTime);
            enemy.PerformAI(_player);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_mapTexture, Vector2.Zero, Color.Red);

        _player.Draw(_spriteBatch);

        foreach (Enemy enemy in _enemies)
        {
            enemy.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

}