using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snollygosters_Universal_App
{
    class Cash
    {
        public Cash(int x, int y)
        {
            this.rectangle.X = x;
            this.rectangle.Y = y;
        }

        public int amount = 100;
        public int speed = 4;
        public Rectangle rectangle = new Rectangle(50, 50, 50, 50);

        public void Draw(SpriteBatch spriteBatch, Texture2D cashTexture)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cashTexture, rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
