
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoTemplate;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    

    

    // Background Sprite
    private Texture2D _BackgroundCourt;

       
       

    // Ball

        // Ball Texture
        private Texture2D _Ball;
        
        // Ball Hixbox Rectangle
        private Rectangle _BallRect;

        // Ball Width & Height
        private const int _BallWidthHeight = 21;

        // Ball Speed
        private float _BallSpeed;

        // Ball Position & Direction
        private Vector2 _BallPosition, _BallDirection;


    // Paddle

        // Paddle Texture
        private Texture2D _Paddle;

        // Paddle Left and Right Vecotors
        private Vector2 _PaddleLeft;
        private Vector2 _PaddleRight; 

        // Paddle Hitbox Rectangles
        private Rectangle _PaddleRectL;
        private Rectangle _PaddleRectR;

        // Paddle Width & Height
        private const int _PaddleWidth = 16, _PaddleHeight = 125;

    
    // Game Manager

        // Sets Window Size Paramaters
        private const int _WindowWidth = 750, _WindowHeight = 450;

        // Screen Edge Collision Detection
        private Rectangle _playAreaBoundingBox;


        

    
    

    


    

    

    

  

   

    


    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Sets Window Width and Height
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        // Sets Ball Starting Position and speed
        _BallPosition.X = 350;
        _BallPosition.Y = 225;
        _BallSpeed = 100;

        // Sets Ball Starting Direction
        _BallDirection.X = 1;
        _BallDirection.Y = 1;

        _playAreaBoundingBox = new(0,0,_WindowWidth,_WindowHeight);

          // Sets Paddle Positions

            // X
            _PaddleLeft.X = 40;
            _PaddleRight.X = 696;

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
          
            _Paddle = Content.Load<Texture2D>("Paddle");

        // Loads Ball
            _Ball = Content.Load<Texture2D>("Ball");
        

      

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        // Updates the balls position over time
        _BallPosition += _BallDirection * _BallSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        // Creates ball Hitbox
        _BallRect = new Rectangle((int)_BallPosition.X, (int)_BallPosition.Y,_BallWidthHeight, _BallWidthHeight);

        // Creates Paddles Left & Right Hitbox
        _PaddleRectL = new Rectangle((int)_PaddleLeft.X, (int)_PaddleLeft.Y, _PaddleWidth, _PaddleHeight);
        _PaddleRectR = new Rectangle((int)_PaddleRight.X, (int)_PaddleRight.Y, _PaddleWidth, _PaddleHeight);

        // Checks Player 1 (WASD) & 2 (Arrow Keys) Keyboard input
        checkInputs();
        // Checks if the Ball or Paddles are Touching the wall
        wallCollisionCheck();
        // Checks if Ball is Touching the Paddle
        paddleCollisionCheck();

   

        

        

        base.Update(gameTime);
    }

    private void checkInputs()
    {
        // Player 1 (WASD)
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            // Left Paddle Up
            _PaddleLeft.Y -= 5;
        }
         if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            // Left Paddle Down
            _PaddleLeft.Y += 5;
        }
        // Player 2 (Arrow Keys)
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            // Right Paddle Up
            _PaddleRight.Y -= 5;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            // Right Paddle Down
            _PaddleRight.Y += 5;
        }
       
    }
    private void wallCollisionCheck()
    {
    // Ball Bounds

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

    }

    private void paddleCollisionCheck()
    {
        // Checks Collision for paddles


            //Right Paddle

            // If Ball Touches the Left or Right Side of Paddle
            if(_BallRect.Right >= _PaddleRectR.Left &&
            _BallRect.Left <= _PaddleRectR.Right)
            {
                // If Ball Touches the Top or Bottom of Paddle
                if(_BallRect.Top <= _PaddleRectR.Bottom &&
                _BallRect.Bottom >= _PaddleRectR.Top)
            {
                _BallDirection.Y*=-1;
                _BallDirection.X*=-1;
            }
           
            }
            
           

            // Left Paddle

            // If Ball Touches the Left or Right Side of Paddle
            if(_BallRect.Left <= _PaddleRectL.Right &&
            _BallRect.Right >= _PaddleRectL.Left)
            {
                // If Ball Touches the Top or Bottom of Paddle
                if(_BallRect.Top <= _PaddleRectL.Bottom &&
                _BallRect.Bottom >= _PaddleRectL.Top)
            {
                _BallDirection.Y*=-1;
                _BallDirection.X*=-1;
            }
            
            }
           
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // TODO: Add your drawing code here

        // _spriteBatch.Draw(_BackgroundCourt, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0.1f);
        _spriteBatch.Draw(_BackgroundCourt, _playAreaBoundingBox, Color.SeaGreen);

        _spriteBatch.Draw(_Paddle, _PaddleRectL, Color.White);
        _spriteBatch.Draw(_Paddle, _PaddleRectR, Color.White);

        // Draws Ball
            
            _spriteBatch.Draw(_Ball,_BallRect, Color.White);

        base.Draw(gameTime);

        _spriteBatch.End();
    }
}
