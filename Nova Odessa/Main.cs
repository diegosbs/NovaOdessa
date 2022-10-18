using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NovaOdessa;

namespace Nova_Odessa
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        World world;
        Basic2d cursor;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.screenWidth = 800;
            Globals.screenHeight = 500;

            _graphics.PreferredBackBufferHeight = Globals.screenHeight;
            _graphics.PreferredBackBufferWidth = Globals.screenWidth;

            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            cursor = new Basic2d("2d\\Misc\\CursorArrow", new Vector2(0,0), new Vector2(28,28));

            Globals.keyboard = new McKeyboard();
            Globals.mouse = new McMouseControl();
            world = new World();
        }

        protected override void UnloadContent()
        {
            //base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Globals.gameTime  =  gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            world.Update();

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            world.Draw(Vector2.Zero);

            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) , new Vector2(0,0));

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

#if WINDOWS || LINUX

    public static class Program
    {
        static void Main()
        {
            using (var game = new Nova_Odessa.Main())
            {
                game.Run();
            }
        }
    }
#endif