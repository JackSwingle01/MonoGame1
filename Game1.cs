using MonoGame1.Source;
using MonoGame1.Source.Engine;

namespace MonoGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        public World world;

        //for game scale
        public float scale = .44444f; //not sure why this value, I got it online 

        //textures
        Basic2D playerSprite;
        Texture2D grassBackground;

        Vector2 playerPos; //player position

        
        public float deltaTime; //time elapsed each update
        public Vector2 acceleration;
        public Vector2 velocity;
        public Vector2 deltaPos;
        //public Vector2 gravity;

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
            
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.content = this.Content;
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            scale = 1F / (1080f / _graphics.GraphicsDevice.Viewport.Height);
   

            world = new World();

            //playerPos = new Vector2((_renderTarget.Width / 2) - (playerSprite.model.Width / 2), (_renderTarget.Height / 2) - (playerSprite.model.Height / 2)); //adds player in center
            //playerSprite = new Basic2D("Images/Sprites/redhead", Vector2.Zero, Vector2.Zero);
            //grassBackground = Content.Load<Texture2D>("Images/backgrounds/grass");
            //velocity = Vector2.Zero;

        }

        protected override void Update(GameTime gameTime)
        {
            world.Update();
            KeyboardState keyboardState = Keyboard.GetState();
         
            deltaTime = (float)gameTime.ElapsedGameTime.Milliseconds; //calculates time elapsed
            System.Console.WriteLine(deltaTime);
            float movementSpeed = 50f; //movement speed not dependent on framerate //movement speed is px/second

            //get input direction
            Vector2 direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.W)) { direction += new Vector2(0, -1.0f); }
            if (keyboardState.IsKeyDown(Keys.S)) { direction += new Vector2(0, 1.0f); }
            if (keyboardState.IsKeyDown(Keys.A)) { direction += new Vector2(-1.0f, 0); }
            if (keyboardState.IsKeyDown(Keys.D)) { direction += new Vector2(1.0f, 0); }
            
            if (direction != Vector2.Zero) { direction.Normalize(); } //normalize direction 

            //acceleration = (direction * movementSpeed); // + gravity; //a = g + a0;
            //velocity += acceleration * (deltaTime/1000); //v = vi + at;

            velocity = (direction * movementSpeed) * (deltaTime / 1000); //movement speed is px/second

            //border collision detection 
            /*
            if (playerPos.Y > (_renderTarget.Height - playerSprite.model.Height))
            {
                playerPos.Y = _renderTarget.Height - playerSprite.model.Height;
                velocity.Y =  -velocity.Y * .75f; //Vector2.Zero;
            }
            if (playerPos.Y < 0)
            {
                playerPos.Y = 0;
                velocity.Y = -velocity.Y * .75f;
            }
            if (playerPos.X > (_renderTarget.Width - playerSprite.model.Width))
            {
                playerPos.X = _renderTarget.Width - playerSprite.model.Width;
                velocity.X = -velocity.X * .75f; //Vector2.Zero;
            }
            if (playerPos.X < 0)
            {
                playerPos.X =0;
                velocity.X = -velocity.X * .75f;
            }
            deltaPos = velocity * deltaTime; //dr = vt
            playerPos += deltaPos; //r = ri + dr 
            */
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //set the res scale

            

            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin();
            //put sprites to render here
            world.Draw();
            //Globals.spriteBatch.Draw(grassBackground, Vector2.Zero, Color.White);
            //Globals.spriteBatch.Draw(playerSprite, playerPos, Color.White);
            Globals.spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //this scales the sprites for given resolution
            Globals.spriteBatch.Begin();
            Globals.spriteBatch.Draw(_renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f); //add scale to scale
            Globals.spriteBatch.End();
            //dont add stuff here ^

            base.Draw(gameTime);
        }
    }
}