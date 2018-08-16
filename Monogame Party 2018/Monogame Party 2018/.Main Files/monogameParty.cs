using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

using MonoGame.Extended;


namespace Monogame_Party_2018
{
    public class MonogameParty : Game
    {

        // Member variables:
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GameStateManager manager;

        // Graphics: ------------------------------------------------->>
        // BoardGameComponenents:
        public Texture2D spr_cameraCrosshair;
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
        public Texture2D spr_glove;

        // Input Keys:
        public Texture2D key_inputs;
        public Texture2D keys_move;
        public Texture2D keys_move2;
        public Texture2D keys_enter;

        // PirateBay:
        public Texture2D bg_pirateBay;

        // playerUI:
        public Texture2D spr_SueCloseup;
        public Texture2D spr_LouieCloseup;
        public Texture2D spr_WilberCloseup;
        public Texture2D spr_FrankCloseup;
        public Texture2D spr_VelmaCloseup;
        public Texture2D spr_ManfordCloseup;
        public Texture2D spr_playerBox;
        public Texture2D spr_playerBoxFrame;
        public Texture2D spr_boxInner;
        public Texture2D spr_firstPlace;
        public Texture2D spr_secondPlace;
        public Texture2D spr_thirdPlace;
        public Texture2D spr_fourthPlace;

        // Spaces:
        public Texture2D piece_blue64;
        public Texture2D piece_green64;
        public Texture2D piece_purple64;
        public Texture2D piece_red64;
        public Texture2D piece_white64;
        public Texture2D piece_star64;
        public Texture2D piece_orange64;
        public Texture2D piece_crystal64;

        // Other:
        public Texture2D noSprite; // used as default and errors
        public Texture2D confirmPlayerFade; // used for transition in confirm player state

        // Minigames
        public Texture2D spr_miniGameInstructionBox;
        public Texture2D minigame_one_background;
        public Texture2D minigame_one_explosion;
        public Texture2D minigame_one_currPlayer;
        public Texture2D minigame_one_plungerDown;
        public Texture2D minigame_one_plungerUp;
        public Texture2D minigame_two_background;
        public Texture2D minigame_two_racetrack;
        public Texture2D minigame_two_up_arrow;
        public Texture2D minigame_two_down_arrow;
        public Texture2D minigame_two_right_arrow;
        public Texture2D minigame_two_left_arrow;
        public Texture2D mg2Alt;

        // Result Stars
        public Texture2D spr_star1;
        public Texture2D spr_star2;
        public Texture2D spr_star3;
        public Texture2D spr_star4;
        public Texture2D spr_star5;

        // Chance Time
        public Texture2D bg_chanceTime;
        public Texture2D spr_chanceBlock;
        public Texture2D spr_chanceBlockLight;
        public Texture2D spr_chanceArrowL;
        public Texture2D spr_chanceArrowR;
        public Texture2D spr_chanceArrowUp;
        public Texture2D spr_chanceArrowDown;
        public Texture2D spr_chanceArrowSwap;
        public Texture2D spr_chance1;
        public Texture2D spr_chance2;
        public Texture2D spr_chance10;
        public Texture2D spr_chance20;
        public Texture2D spr_chance30;
        public Texture2D spr_chanceCoin;
        public Texture2D spr_chanceStar;
        public Texture2D spr_chanceManford;
        public Texture2D spr_chanceLouie;
        public Texture2D spr_chanceFrank;
        public Texture2D spr_chanceWilber;
        public Texture2D spr_chanceSue;
        public Texture2D spr_chanceVelma;

        // Test graphic
        public Texture2D spr_testDice;
        public Texture2D spr_coin;
        public Texture2D spr_star;
        public Texture2D spr_diceBox;

        // --------------------------------------------------END GRAPHICS

        // Fonts:
        public SpriteFont ft_mainMenuFont;
        public SpriteFont ft_menuDescriptionFont;
        public SpriteFont ft_debugSmall;
        public SpriteFont ft_debugMedium;
        public SpriteFont ft_playerUIdata;
        public SpriteFont ft_confirmPlayer;
        public SpriteFont ft_confirmPlayer_Bold;
        public SpriteFont ft_confirmPlayer_s27;
        public SpriteFont ft_confirmPlayer_sm;
        public SpriteFont ft_rollDice_sm;
        public SpriteFont ft_rollDice_lg;
        public SpriteFont ft_confirmPlayer_s32;

        public Dictionary<MGP_Constants.music, Song> songs = new Dictionary<MGP_Constants.music, Song>();
        public Dictionary<MGP_Constants.soundEffects, SoundEffect> sfx = new Dictionary<MGP_Constants.soundEffects, SoundEffect>();

        // CameraProperties:
        public Camera2D cameraObject;

        // CONSTRUCTOR:
        public MonogameParty()
        {

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
        protected override void Initialize()
        {
            cameraObject = new Camera2D(GraphicsDevice);

            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // BoardGameComponents
            spr_cameraCrosshair = Content.Load<Texture2D>("BoardGameComponents/cameraCrosshair");
            spr_messageBox = Content.Load<Texture2D>("BoardGameComponents/spr_messageBox");

            // playerUI
            spr_playerBox = Content.Load<Texture2D>("playerUI/spr_playerBox");
            spr_playerBoxFrame = Content.Load<Texture2D>("playerUI/spr_playerBoxFrame");
            spr_boxInner = Content.Load<Texture2D>("playerUI/spr_boxInner");
            spr_ManfordCloseup = Content.Load<Texture2D>("playerUI/spr_ManfordCloseup");
            spr_LouieCloseup = Content.Load<Texture2D>("playerUI/spr_LouieCloseup");
            spr_SueCloseup = Content.Load<Texture2D>("playerUI/spr_SueCloseup");
            spr_VelmaCloseup = Content.Load<Texture2D>("playerUI/spr_VelmaCloseup");
            spr_FrankCloseup = Content.Load<Texture2D>("playerUI/spr_FrankCloseup");
            spr_WilberCloseup = Content.Load<Texture2D>("playerUI/spr_WilberCloseup");
            spr_firstPlace = Content.Load<Texture2D>("playerUI/spr_firstPlace");
            spr_secondPlace = Content.Load<Texture2D>("playerUI/spr_secondPlace");
            spr_thirdPlace = Content.Load<Texture2D>("playerUI/spr_thirdPlace");
            spr_fourthPlace = Content.Load<Texture2D>("playerUI/spr_fourthPlace");

            // Characters:
            spr_Manford = Content.Load<Texture2D>("Characters/spr_Manford");
            spr_Louie = Content.Load<Texture2D>("Characters/spr_Louie");
            spr_Sue = Content.Load<Texture2D>("Characters/spr_Sue");
            spr_Velma = Content.Load<Texture2D>("Characters/spr_Velma");
            spr_Frank = Content.Load<Texture2D>("Characters/spr_Frank");
            spr_Wilber = Content.Load<Texture2D>("Characters/spr_Wilber");

            // MainMenu:
            bg_titleScreen = Content.Load<Texture2D>("MainMenu/bg_titleScreen");
            spr_cloudIcon = Content.Load<Texture2D>("MainMenu/spr_cloudIcon_Alt");
            spr_glove = Content.Load<Texture2D>("MainMenu/spr_glove");

            // Input Keys:
            key_inputs = Content.Load<Texture2D>("KB_Keys/Key_Input");
            keys_move = Content.Load<Texture2D>("KB_Keys/Key_Move");
            keys_move2 = Content.Load<Texture2D>("KB_Keys/Key_Move2");
            keys_enter = Content.Load<Texture2D>("KB_Keys/Key_Enter");

            // PirateBay:
            bg_pirateBay = Content.Load<Texture2D>("PirateBay/bg_pirateBay");

            // Spaces:
            piece_blue64 = Content.Load<Texture2D>("Spaces/piece_blue64");
            piece_green64 = Content.Load<Texture2D>("Spaces/piece_green64");
            piece_purple64 = Content.Load<Texture2D>("Spaces/piece_purple64");
            piece_red64 = Content.Load<Texture2D>("Spaces/piece_red64");
            piece_white64 = Content.Load<Texture2D>("Spaces/piece_white64");
            piece_star64 = Content.Load<Texture2D>("Spaces/piece_star64");
            piece_orange64 = Content.Load<Texture2D>("Spaces/piece_orange64");
            piece_crystal64 = Content.Load<Texture2D>("Spaces/piece_crystal64");

            // Other:
            noSprite = Content.Load<Texture2D>("noSprite");
            confirmPlayerFade = Content.Load<Texture2D>("transitions/confirmPlayerFade");   // Black box used for transitions

            // Fonts:
            ft_mainMenuFont = Content.Load<SpriteFont>("MainMenu/ft_mainMenuFont");
            ft_menuDescriptionFont = Content.Load<SpriteFont>("MainMenu/ft_menuDescriptionFont");
            ft_debugSmall = Content.Load<SpriteFont>("MainMenu/ft_debugSmall");
            ft_debugMedium = Content.Load<SpriteFont>("MainMenu/ft_debugMedium");
            ft_playerUIdata = Content.Load<SpriteFont>("playerUI/playerUIdata");
            ft_confirmPlayer = Content.Load<SpriteFont>("transitions/confirmPlayerFont");
            ft_confirmPlayer_Bold = Content.Load<SpriteFont>("transitions/confirmPlayerFont_Bold");
            ft_confirmPlayer_s27 = Content.Load<SpriteFont>("transitions/confirmPlayerFont_s27");
            ft_confirmPlayer_sm = Content.Load<SpriteFont>("transitions/confirmPlayerFont_sm");
            ft_rollDice_sm = Content.Load<SpriteFont>("transitions/confirmPlayerFont_size16");
            ft_rollDice_lg = Content.Load<SpriteFont>("transitions/confirmPlayerFont_size18");
            ft_confirmPlayer_s32 = Content.Load<SpriteFont>("transitions/confirmPlayerFont_size32");

            // Temp dice object
            spr_testDice = Content.Load<Texture2D>("BoardGameComponents/testDiceBox");
            spr_diceBox = Content.Load<Texture2D>("BoardGameComponents/spr_diceBox");

            // Game objects
            spr_coin = Content.Load<Texture2D>("BoardGameComponents/piece_coin64");
            spr_star = Content.Load<Texture2D>("BoardGameComponents/star_icon");

            // Minigame objects
            spr_miniGameInstructionBox = Content.Load<Texture2D>("Minigames/spr_minigameInstructionBox");
            minigame_one_background = Content.Load<Texture2D>("Minigames/background");
            minigame_one_currPlayer = Content.Load<Texture2D>("Minigames/curr_player");
            minigame_one_explosion = Content.Load<Texture2D>("Minigames/Explosion");
            minigame_one_plungerUp = Content.Load<Texture2D>("Minigames/plunger_up");
            minigame_one_plungerDown = Content.Load<Texture2D>("Minigames/plunger_down");
            minigame_two_background = Content.Load<Texture2D>("Minigames/bg_minigame2");
            minigame_two_racetrack = Content.Load<Texture2D>("Minigames/spr_racetrack");
            minigame_two_up_arrow = Content.Load<Texture2D>("Minigames/spr_up_arrow");
            minigame_two_down_arrow = Content.Load<Texture2D>("Minigames/spr_down_arrow");
            minigame_two_right_arrow = Content.Load<Texture2D>("Minigames/spr_right_arrow");
            minigame_two_left_arrow = Content.Load<Texture2D>("Minigames/spr_left_arrow");
            mg2Alt = Content.Load<Texture2D>("Minigames/mg2bg");




            // Game Result Stars
            spr_star1 = Content.Load<Texture2D>("Results/result_Star1");
            spr_star2 = Content.Load<Texture2D>("Results/result_Star2");
            spr_star3 = Content.Load<Texture2D>("Results/result_Star3");
            spr_star4 = Content.Load<Texture2D>("Results/result_Star4");
            spr_star5 = Content.Load<Texture2D>("Results/result_Star5");


            // Chance Time:
            bg_chanceTime = Content.Load<Texture2D>("ChanceTime/bg_ChanceTime");
            spr_chanceBlock = Content.Load<Texture2D>("ChanceTime/spr_chanceBlock");
            spr_chanceBlockLight = Content.Load<Texture2D>("ChanceTime/spr_chanceBlockLight");
            spr_chanceArrowL = Content.Load<Texture2D>("ChanceTime/spr_chanceArrowL");
            spr_chanceArrowR = Content.Load<Texture2D>("ChanceTime/spr_chanceArrowR");
            spr_chanceArrowUp = Content.Load<Texture2D>("ChanceTime/spr_chanceArrowUp");
            spr_chanceArrowDown = Content.Load<Texture2D>("ChanceTime/spr_chanceArrowDown");
            spr_chanceArrowSwap = Content.Load<Texture2D>("ChanceTime/spr_chanceArrowSwap");
            spr_chance1 = Content.Load<Texture2D>("ChanceTime/spr_chance1");
            spr_chance2 = Content.Load<Texture2D>("ChanceTime/spr_chance2");
            spr_chance10 = Content.Load<Texture2D>("ChanceTime/spr_chance10");
            spr_chance20 = Content.Load<Texture2D>("ChanceTime/spr_chance20");
            spr_chance30 = Content.Load<Texture2D>("ChanceTime/spr_chance30");
            spr_chanceCoin = Content.Load<Texture2D>("ChanceTime/spr_chanceCoin");
            spr_chanceStar = Content.Load<Texture2D>("ChanceTime/spr_chanceStar");
            spr_chanceManford = Content.Load<Texture2D>("ChanceTime/spr_chanceManford");
            spr_chanceLouie = Content.Load<Texture2D>("ChanceTime/spr_chanceLouie");
            spr_chanceFrank = Content.Load<Texture2D>("ChanceTime/spr_chanceFrank");
            spr_chanceWilber = Content.Load<Texture2D>("ChanceTime/spr_chanceWilber");
            spr_chanceSue = Content.Load<Texture2D>("ChanceTime/spr_chanceSue");
            spr_chanceVelma = Content.Load<Texture2D>("ChanceTime/spr_chanceVelma");



            // Music:
            songs.Add(MGP_Constants.music.pirateBay, Content.Load<Song>("Music/mus_pirateBay"));
            songs.Add(MGP_Constants.music.mainMenu, Content.Load<Song>("Music/mus_mainTheme"));

            // SFX:
            sfx.Add(MGP_Constants.soundEffects.pressStart, Content.Load<SoundEffect>("SFX/sfx_pressStart"));
            sfx.Add(MGP_Constants.soundEffects.space, Content.Load<SoundEffect>("SFX/sfx_space"));
            sfx.Add(MGP_Constants.soundEffects.dicePre, Content.Load<SoundEffect>("SFX/sfx_dicePre"));
            sfx.Add(MGP_Constants.soundEffects.diceRolling, Content.Load<SoundEffect>("SFX/sfx_diceRolling"));
            sfx.Add(MGP_Constants.soundEffects.diceHit, Content.Load<SoundEffect>("SFX/sfx_diceHit"));
            sfx.Add(MGP_Constants.soundEffects.spaceBlue, Content.Load<SoundEffect>("SFX/sfx_spaceBlue"));
            sfx.Add(MGP_Constants.soundEffects.spaceRed, Content.Load<SoundEffect>("SFX/sfx_spaceRed"));
            sfx.Add(MGP_Constants.soundEffects.chanceTimeDrum, Content.Load<SoundEffect>("SFX/sfx_chanceTimeDrum"));
            sfx.Add(MGP_Constants.soundEffects.chanceTimeHigh, Content.Load<SoundEffect>("SFX/sfx_chanceTimeHigh"));
            sfx.Add(MGP_Constants.soundEffects.chanceTimeLow, Content.Load<SoundEffect>("SFX/sfx_chanceTimeLow"));
            sfx.Add(MGP_Constants.soundEffects.chanceTimeCymbal, Content.Load<SoundEffect>("SFX/sfx_chanceTimeCymbal"));
            sfx.Add(MGP_Constants.soundEffects.menuSelect, Content.Load<SoundEffect>("SFX/sfx_menuSelect"));
            sfx.Add(MGP_Constants.soundEffects.menuCancel, Content.Load<SoundEffect>("SFX/sfx_menuCancel"));

        }

        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        protected override void UnloadContent()
        {
            // !- TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world
        protected override void Update(GameTime gameTime)
        {

            // Exit Game:
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //   Exit();

            manager.Update(gameTime);
            base.Update(gameTime);
            //Exit game from main menu
            //I put it here so that the states would get removed first. Not sure if thats needed
        }


        /// This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            manager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
