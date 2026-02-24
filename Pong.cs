
using System;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PaddleNamespace;
using BallNamespace;

namespace MonoTemplate;

public class Pong : Game
{
 private const int _WindowWidth = 750, _WindowHeight = 450;
 private const int _PlayAreaEdgeLineWidth = 12;
 private GraphicsDeviceManager _graphics;
 private SpriteBatch _spriteBatch;
 private Texture2D _BackgroundCourt;

 private Rectangle _playAreaBoundingBox
 {
     get{
         return  new Rectangle(
         _PlayAreaEdgeLineWidth,
         _PlayAreaEdgeLineWidth,
         _WindowWidth- (_PlayAreaEdgeLineWidth*2),
         _WindowHeight - (_PlayAreaEdgeLineWidth * 2)
     );
     }
 }

 private Ball _ball;

 private Paddle _rightPaddle, _leftPaddle;
 

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
        

        

        

        // Initialize Paddles
        _leftPaddle = new Paddle();
        _leftPaddle.Initialize(new Vector2(60, 146), _playAreaBoundingBox, Color.White);

        _rightPaddle = new Paddle();
        _rightPaddle.Initialize(new Vector2(674, 146), _playAreaBoundingBox, Color.White);

        _ball = new Ball();
        _ball.Initialize(new Vector2(150, 195), new Vector2(-1, 1), _playAreaBoundingBox , 5);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        // Loads Background
            _BackgroundCourt = Content.Load<Texture2D>("Court");

            _leftPaddle.LoadContent(Content);
            _rightPaddle.LoadContent(Content);
          
            
           

            // Loads Ball
            _ball.LoadContent(Content);
           

        

      

    }

    protected override void Update(GameTime gameTime)
    {
                KeyboardState kbState = Keyboard.GetState();
        
        // 1. Update Ball
        _ball.Update(gameTime);
    
        Vector2 rightDir =Vector2.Zero;
        Vector2 leftDir = Vector2.Zero;

        // 2. Update Left Paddle (W/S Keys)
       
        if (kbState.IsKeyDown(Keys.W)) leftDir.Y = -1;
        else if (kbState.IsKeyDown(Keys.S)) leftDir.Y = 1;
        _leftPaddle.Update(gameTime, leftDir);

        // 3. Update Right Paddle (Up/Down Keys)
        
        if (kbState.IsKeyDown(Keys.Up)) rightDir.Y = -1;
        else if (kbState.IsKeyDown(Keys.Down)) rightDir.Y = 1;
        _rightPaddle.Update(gameTime, rightDir);

        _ball.ProcessCollision(_rightPaddle.BoundingBox);
        _ball.ProcessCollision(_leftPaddle.BoundingBox);
       

                                    // left paddle
        // if(_ball.ProcessCollision(_leftPaddle.Rectangle)) // you can use the returing value to do add othe functionalities to your game.
        // {
        //     //_hud.Paddle01Score++;
        //     //_paddle.Glow();
        //     // playe an animation ...
        // }
        //                             // right paddle
        // _ball.ProcessCollision(_rightPaddle.Rectangle); // here We will not use the result of the method and it is ok.

        base.Update(gameTime);
    }

    
   



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SeaGreen);

        _spriteBatch.Begin();

        // TODO: Add your drawing code here

     
        _spriteBatch.Draw(_BackgroundCourt, _playAreaBoundingBox, Color.SeaGreen);


        // Draws Ball
            
            
         
            _leftPaddle.Draw(_spriteBatch);
            _rightPaddle.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);
            

        

        base.Draw(gameTime);

        _spriteBatch.End();
    }
}
