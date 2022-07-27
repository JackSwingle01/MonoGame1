
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
        public Vector2 acceleration;
        public Vector2 velocity;
        public Vector2 deltaPos;
        
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

            playerPos = new Vector2((_renderTarget.Width / 2) - (playerSprite.Width / 2), (_renderTarget.Height / 2) - (playerSprite.Height / 2)); //adds player in center
            //acceleration = new Vector2(0, 0.01f); //gravity
            velocity = Vector2.Zero;
        }

        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
         
            deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds; //calculates time elapsed
            System.Console.WriteLine(deltaTime);
            float movementSpeed = .02f; //movement speed not dependent on framerate
            
            //get input direction
            Vector2 direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W)) { direction += new Vector2(0, -1.0f); }
            if (keyboardState.IsKeyDown(Keys.S)) { direction += new Vector2(0, 1.0f); }
            if (keyboardState.IsKeyDown(Keys.A)) { direction += new Vector2(-1.0f, 0); }
            if (keyboardState.IsKeyDown(Keys.D)) { direction += new Vector2(1.0f, 0); }
            direction = Vector2.Normalize(direction);

            velocity = velocity + direction * movementSpeed;
            velocity = velocity + acceleration * deltaTime; //v = vi + a dt
            if (playerPos.Y > (_renderTarget.Height - playerSprite.Height) || playerPos.Y < 0)
            {
                velocity.Y =  -velocity.Y * .75f; //Vector2.Zero;
            }
            if (playerPos.X > (_renderTarget.Width - playerSprite.Width) || playerPos.X < 0)
            {
                velocity.X = -velocity.X * .75f; //Vector2.Zero;
            }
            deltaPos = velocity * deltaTime; //dr = vt
            playerPos += deltaPos; //r = ri + dr 

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
            _spriteBatch.Draw(_renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f); //add scale to scale
            _spriteBatch.End();
            //dont add stuff here ^

            base.Draw(gameTime);
        }
    }
}