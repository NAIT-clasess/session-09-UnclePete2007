
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Rectangle _BallRect;

    // Sprites

        // Background
        private Texture2D _BackgroundCourt;

        // Paddles
        private Texture2D _PaddleL;
        private Texture2D _PaddleR;

        // Ball
        private Texture2D _Ball;

    // Vectors
        
        private Vector2 _PaddleLeft;
        private Vector2 _PaddleRight; 


        

    private const int _WindowWidth = 750, _WindowHeight = 450;
    private const int _PlayAreaEdgeLineWidth = 12, _BallWidthHeight = 21;

    private float _BallSpeed;

    private Vector2 _BallPosition, _BallDirection;

    private Rectangle _playAreaBoundingBox;

    


    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _BallPosition.X = 350;
        _BallPosition.Y = 225;
        _BallSpeed = 100;

        _BallDirection.X = 1;
        _BallDirection.Y = 1;

        _playAreaBoundingBox = new(0,0,_WindowWidth,_WindowHeight);

          // Sets Paddle Positions

            // X
            _PaddleLeft.X = 50;
            _PaddleRight.X = 700;

            // Y
            _PaddleLeft.Y = 225;
            _PaddleRight.Y = 225;



        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        // Loads Background
            _BackgroundCourt = Content.Load<Texture2D>("Court");

        // Loads Paddle Left and Right
            _PaddleL = Content.Load<Texture2D>("Paddle");
            _PaddleR = Content.Load<Texture2D>("Paddle");

        // Loads Ball
            _Ball = Content.Load<Texture2D>("Ball");
        

      

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
         _BallPosition += _BallDirection * _BallSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        _BallRect = new Rectangle((int)_BallPosition.X, (int)_BallPosition.Y,_BallWidthHeight, _BallWidthHeight);

        // Checks Collision for the walls

            // If Ball Touches the Left or Right
            if(_BallRect.Left <= _playAreaBoundingBox.Left ||
            _BallRect.Right >= _playAreaBoundingBox.Right)
            {
                _BallDirection.X*=-1;
            }
            // If Ball Touches the Top or Bottom
            if(_BallRect.Bottom >= _playAreaBoundingBox.Bottom ||
            _BallRect.Top <= _playAreaBoundingBox.Top)
            {
                _BallDirection.Y*=-1;
            }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // TODO: Add your drawing code here

        // _spriteBatch.Draw(_BackgroundCourt, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0.1f);
        _spriteBatch.Draw(_BackgroundCourt, _playAreaBoundingBox, Color.SeaGreen);

        _spriteBatch.Draw(_PaddleL, _PaddleLeft, null,  Color.White, 0f, new Vector2(1,9), 6, SpriteEffects.None, 0.1f);
        _spriteBatch.Draw(_PaddleR, _PaddleRight, null,  Color.White, 0f, new Vector2(1,9), 6, SpriteEffects.None, 0.1f);

        // Draws Ball
            
            _spriteBatch.Draw(_Ball,_BallRect, Color.White);

        base.Draw(gameTime);

        _spriteBatch.End();
    }
}
