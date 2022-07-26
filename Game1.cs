
namespace MonoGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        //for game scale
        public float scale = .44444f; //not sure why this value, I got it online 

        Texture2D face;
        Texture2D grassBackground;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            face = Content.Load<Texture2D>("Images/Sprites/LaughCryFaceSprite");
            grassBackground = Content.Load<Texture2D>("Images/backgrounds/grass");

            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //set the res scale
            scale = 1F / (1080f / _graphics.GraphicsDevice.Viewport.Height);
           

            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //put sprites to render here
            _spriteBatch.Draw(grassBackground, Vector2.Zero, Color.White);
            _spriteBatch.Draw(face, Vector2.Zero, Color.White);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //this scales the sprites for given resolution
            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            _spriteBatch.End();
            //dont add stuff here ^

            base.Draw(gameTime);
        }
    }
}