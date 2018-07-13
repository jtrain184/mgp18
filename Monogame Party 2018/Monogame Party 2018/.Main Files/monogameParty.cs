using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;


namespace Monogame_Party_2018 {
    public class MonogameParty : Game {

        // Member variables:
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GameStateManager manager;

        // Graphics:
        public Texture2D bg_titleScreen;
        public Texture2D bg_pirateBay;

        public Texture2D spr_cloudIcon;
        public Texture2D piece_blue64;
        public Texture2D piece_green64;
        public Texture2D piece_purple64;
        public Texture2D piece_red64;
        public Texture2D piece_white64;

        // Fonts:
        public SpriteFont ft_mainMenuFont;
        public SpriteFont ft_menuDescriptionFont;

        // CameraProperties:
        public Camera2D cameraObject;

        // CONSTRUCTOR:
        public MonogameParty() {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Pass in a reference to itself
            manager = new GameStateManager(this);

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

        }


        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize() {
            cameraObject = new Camera2D(GraphicsDevice);

            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg_titleScreen = Content.Load<Texture2D>("MainMenu/bg_titleScreen");
            bg_pirateBay = Content.Load<Texture2D>("PirateBay/bg_pirateBay");

            spr_cloudIcon = Content.Load<Texture2D>("MainMenu/spr_cloudIcon");
            piece_blue64 = Content.Load<Texture2D>("Spaces/piece_blue64");
            piece_green64 = Content.Load<Texture2D>("Spaces/piece_green64");
            piece_purple64 = Content.Load<Texture2D>("Spaces/piece_purple64");
            piece_red64 = Content.Load<Texture2D>("Spaces/piece_red64");
            piece_white64 = Content.Load<Texture2D>("Spaces/piece_white64");

            ft_mainMenuFont = Content.Load<SpriteFont>("MainMenu/ft_mainMenuFont");
            ft_menuDescriptionFont = Content.Load<SpriteFont>("MainMenu/ft_menuDescriptionFont");

            // TODO: use this.Content to load your game content here
        }

        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        protected override void UnloadContent() {
            // !- TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world
        protected override void Update(GameTime gameTime) {

            // Exit Game:
           // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
             //   Exit();

            manager.Update(gameTime);
            base.Update(gameTime);
            //Exit game from main menu
            //I put it here so that the states would get removed first. Not sure if thats needed
        }


        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            manager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
