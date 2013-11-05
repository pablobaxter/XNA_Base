using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA_Base
{
    class Camera
    {
        Vector2 position = Vector2.Zero; //Vector2.Zero is a vector with (0,0)
        int screenWidth = 0;
        int screenHeight = 0;

        /// <summary>
        /// Camera constructor.  Sets width and height of current game window.
        /// </summary>
        /// <param name="viewport">Viewport object from current graphics device.</param>
        public Camera(Viewport viewport)
        {
            screenWidth = viewport.Width;
            screenHeight = viewport.Height;
        }

        /// <summary>
        /// Used to move all drawn objects simultaneously using the overloaded SpriteBatch.Begin() method.
        /// </summary>
        public Matrix transformMatrix
        {
            get { return Matrix.CreateTranslation(new Vector3(-position, 0f)); }  //Z-axix never used.
        }

        /// <summary>
        /// Update method to focus camera on player that is passed.
        /// Point of focus is center of sprite image.
        /// </summary>
        /// <param name="map">Used to keep camera from going off the map.</param>
        /// <param name="player">Player to focus camera on.</param>
        public void Update(TileMap map, Player player)
        {
            position.X = player.Position.X + (player.Width / 2 - screenWidth / 2);
            position.Y = player.Position.Y + (player.Height / 2 - screenHeight / 2);

            position.X = MathHelper.Clamp(position.X, 0.0f, map.Width - screenWidth);
            position.Y = MathHelper.Clamp(position.Y, 0.0f, map.Height - screenHeight);
        }
    }
}
