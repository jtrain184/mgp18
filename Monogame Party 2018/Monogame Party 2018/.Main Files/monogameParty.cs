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

        // Graphics: ------------------------------------------------->>
        // BoardGameComponenents:
        public Texture2D spr_cameraCrosshair;
        public Texture2D spr_playerBox;
        public Texture2D spr_playerBoxFrame;
        public Texture2D spr_messageBox;

        // Characters:
        public Texture2D spr_Manford;
        public Texture2D spr_Louie;
        public Texture2D spr_Sue;
        public Texture2D spr_Velma;
        public Texture2D spr_Frank;
        public Texture2D spr_Wilber;

        // MainMenu:
        public Texture2D bg_titleScreen;
        public Texture2D spr_cloudIcon;

        // PirateBay:
        public Texture2D bg_pirateBay;

        // Spaces:
        public Texture2D piece_blue64;
        public Texture2D piece_green64;
        public Texture2D piece_purple64;
        public Texture2D piece_red64;
        public Texture2D piece_white64;
        public Texture2D piece_star64;

        // Other:
        public Texture2D noSprite; // used as default and errors


        // Test graphic
        public Texture2D spr_testDice;
        public Texture2D spr_coin;
        public Texture2D spr_star;

        // --------------------------------------------------END GRAPHICS

        // Fonts:
        public SpriteFont ft_mainMenuFont;
        public SpriteFont ft_menuDescriptionFont;
        public SpriteFont ft_debugSmall;
        public SpriteFont ft_debugMedium;

        // CameraProperties:
        public Camera2D cameraObject;

        // CONSTRUCTOR:
        public MonogameParty() {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Pass in a reference to itself
            manager = new GameStateManager(this);

            graphics.PreferredBackBufferWidth = MGP_Constants.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = MGP_Constants.SCREEN_HEIGHT;

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


            // BoardGameComponents
            spr_cameraCrosshair = Content.Load<Texture2D>("BoardGameComponents/cameraCrosshair");
            spr_playerBox = Content.Load<Texture2D>("BoardGameComponents/spr_playerBox");
            spr_playerBoxFrame = Content.Load<Texture2D>("BoardGameComponents/spr_playerBoxFrame");
            spr_messageBox = Content.Load<Texture2D>("BoardGameComponents/spr_messageBox");

            // Characters:
            spr_Manford = Content.Load<Texture2D>("Characters/spr_Manford");
            spr_Louie = Content.Load<Texture2D>("Characters/spr_Louie");
            spr_Sue = Content.Load<Texture2D>("Characters/spr_Sue");
            spr_Velma = Content.Load<Texture2D>("Characters/spr_Velma");
            spr_Frank = Content.Load<Texture2D>("Characters/spr_Frank");
            spr_Wilber = Content.Load<Texture2D>("Characters/spr_Wilber");

            // MainMenu:
            bg_titleScreen = Content.Load<Texture2D>("MainMenu/bg_titleScreen");
            spr_cloudIcon = Content.Load<Texture2D>("MainMenu/spr_cloudIcon");

            // PirateBay:
            bg_pirateBay = Content.Load<Texture2D>("PirateBay/bg_pirateBay");

            // Spaces:
            piece_blue64 = Content.Load<Texture2D>("Spaces/piece_blue64");
            piece_green64 = Content.Load<Texture2D>("Spaces/piece_green64");
            piece_purple64 = Content.Load<Texture2D>("Spaces/piece_purple64");
            piece_red64 = Content.Load<Texture2D>("Spaces/piece_red64");
            piece_white64 = Content.Load<Texture2D>("Spaces/piece_white64");
            piece_star64 = Content.Load<Texture2D>("Spaces/piece_star64");

            // Other:
            noSprite = Content.Load<Texture2D>("noSprite");

            // Fonts:
            ft_mainMenuFont = Content.Load<SpriteFont>("MainMenu/ft_mainMenuFont");
            ft_menuDescriptionFont = Content.Load<SpriteFont>("MainMenu/ft_menuDescriptionFont");
            ft_debugSmall = Content.Load<SpriteFont>("MainMenu/ft_debugSmall");
            ft_debugMedium = Content.Load<SpriteFont>("MainMenu/ft_debugMedium");

            // Temp dice object
            spr_testDice = Content.Load<Texture2D>("BoardGameComponents/testDiceBox");

            // Game objects
            spr_coin = Content.Load<Texture2D>("BoardGameComponents/piece_coin64");
            spr_star = Content.Load<Texture2D>("BoardGameComponents/star_icon");

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
