using System.Net.Mime;
using System.Runtime.InteropServices.Swift;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PaddleNamespace
{
    public class Paddle
{
    private Vector2 _position;
    private Rectangle _playAreaBoundingBox;
    private const int _Width = 16;
    private const int _Height = 125;
    private const float _Speed = 300f;
    private Texture2D _texture;
    private Color _color;

    Vector2 _dimensions=> new Vector2(_Width,_Height);
    internal Rectangle BoundingBox => 
        new Rectangle(_position.ToPoint(), _dimensions.ToPoint());

    // Rectangle property for drawing and collision
    public Rectangle Rectangle => new Rectangle((int)_position.X, (int)_position.Y, _Width, _Height);

    internal void Initialize(Vector2 initialPosition, Rectangle playAreaBoundingBox, Color color)
    {
        _position = initialPosition;
        _playAreaBoundingBox = playAreaBoundingBox;
        _color = color;
       
    }

    internal void LoadContent(ContentManager Content)
    {
        _texture = Content.Load<Texture2D>("Paddle");
    }

    internal void Update(GameTime gameTime, Vector2 direction)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Move the paddle
        _position += direction * _Speed * dt;

        // Clamp within the play area (Top and Bottom only)
        if (_position.Y <= _playAreaBoundingBox.Top)
        {
            _position.Y = _playAreaBoundingBox.Top;
        }
        else if (_position.Y + _Height >= _playAreaBoundingBox.Bottom)
        {
            _position.Y = _playAreaBoundingBox.Bottom - _Height;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        
        spriteBatch.Draw(_texture, Rectangle, _color);

       
    }

    
}

}
