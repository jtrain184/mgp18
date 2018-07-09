using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Party_2018 {



  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  ///

  public class MonogameParty : Game {

    // Member variables:
    GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public GameStateManager manager;


    // Graphics:
    public Texture2D bg_titleScreen;
    public Texture2D spr_cloudIcon;
    public SpriteFont ft_mainMenuFont;

    // CONSTRUCTOR:
    public MonogameParty() {

      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      // Pass in a reference to itself
      manager = new GameStateManager(this);

      State mainMenu = new S_MainMenu(manager, manager.eCounter, 0, 0);
      manager.AddState(mainMenu, 0);

      graphics.PreferredBackBufferWidth = 1280;
      graphics.PreferredBackBufferHeight = 720;
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize() {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent() {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      bg_titleScreen = Content.Load<Texture2D>("bg_titleScreen");
      spr_cloudIcon = Content.Load<Texture2D>("spr_cloudIcon");
      ft_mainMenuFont = Content.Load<SpriteFont>("ft_mainMenu");


      // TODO: use this.Content to load your game content here
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent() {
      // !- TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    protected override void Update(GameTime gameTime) {

      // Exit Game:
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      manager.Update(gameTime);

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      manager.Draw(gameTime);

      base.Draw(gameTime);
    }
  }
}
