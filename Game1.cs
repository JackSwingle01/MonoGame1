
namespace MonoGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        //for game scale
        public float scale = .44444f; //not sure why this value, I got it online 

        //textures
        Texture2D playerSprite;
        Texture2D grassBackground;

        Vector2 playerPos; //player position

        public float deltaTime; //time elapsed each update
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            //sets time step to 60fps - non frame rate dependent 
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0f);
            IsFixedTimeStep = true;

            base.Initialize();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("Images/Sprites/tiger");
            grassBackground = Content.Load<Texture2D>("Images/backgrounds/grass");

            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            playerPos = new Vector2((_renderTarget.Width / 2) - (playerSprite.Width / 2), (_renderTarget.Height / 2) - (playerSprite.Height / 2));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //^^^ is default code, not sure if I should delete
         
            deltaTime = gameTime.ElapsedGameTime.Milliseconds; //calculates time elapsed
            float movementSpeed = deltaTime / 2.38f; //movement speed not dependent on framerate

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
            _spriteBatch.Draw(playerSprite, playerPos, Color.White);
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