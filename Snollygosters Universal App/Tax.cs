using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snollygosters_Universal_App
{
    class Tax
    {
        public int speed;
        public Tax(int x, int y, int amount, int inSpeed)
        {
            this.rectangle.X = x;
            this.rectangle.Y = y;
            this.amount = amount;
            speed = inSpeed;
        }

        public int amount;
        public Rectangle rectangle = new Rectangle(300, 0, 50, 50);

        public void Draw(SpriteBatch spriteBatch, Texture2D cashTexture)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(cashTexture, rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
