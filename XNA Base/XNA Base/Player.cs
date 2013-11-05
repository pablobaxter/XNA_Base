using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace XNA_Base
{
    class Player
    {
        Texture2D spriteSheet;
        Vector2 pos;
        Vector2 dir;
        GamePadState currState;
        float speed;
        Rectangle[] rectFrame;
        int frames = 0;
        int delay = 0;
        int fWidth;
        int fHeight;

        /// <summary>
        /// Constructor for player object.
        /// </summary>
        /// <param name="sheet">Player sheet to be used for draw method.</param>
        /// <param name="startPos">Vector starting position for the player.</param>
        /// <param name="numFrames">Number of frames in the sprite sheet.</param>
        public Player(Texture2D sheet, Vector2 startPos, int numFrames)
        {
            spriteSheet = sheet;
            fWidth = sheet.Width/numFrames;
            fHeight = sheet.Height;
            pos = startPos;
            speed = 5;
            rectFrame = new Rectangle[numFrames];  //Used for the draw method to locate where on the sheet to draw from.
            for (int i = 0; i < numFrames; i++)
            {
                rectFrame[i] = new Rectangle(i * fWidth, 0,
                    fWidth, sheet.Height);
            }
        }

        /// <summary>
        /// Draw method for player.
        /// Due to XNA having approx 50+ FPS, the delay was added to keep player
        /// from animating too quickly.
        /// </summary>
        /// <param name="batch">SpriteBatch object from Game1.cs.</param>
        public void Draw(SpriteBatch batch)
        {
            if (frames < rectFrame.Length)
            {
                if (delay < 6)
                {
                    delay++;
                    batch.Draw(
                        spriteSheet,
                        pos,
                        rectFrame[frames], 
                        Color.White);
                }
                else
                {
                    batch.Draw(
                        spriteSheet, 
                        pos,
                        rectFrame[frames], 
                        Color.White);
                    frames++;
                    delay = 0;
                }
            }

            else
            {
                frames = 0;
                batch.Draw(
                    spriteSheet, 
                    pos,
                    rectFrame[frames], 
                    Color.White);
            }
        }

        public Vector2 Position
        {
            get { return pos; }
        }

        public int Width
        {
            get { return fWidth; }
        }

        public int Height
        {
            get { return fHeight; }
        }

        /// <summary>
        /// Converts key or gamepad input into a unit vector,
        /// and multiplies the speed variable to it, then returns value.
        /// If no input is read, returns a zero vector.
        /// </summary>
        public Vector2 Velocity
        {
            get 
            {
                dir = Vector2.Zero;
                currState = GamePad.GetState(PlayerIndex.One);

                //If sensitivity control is wanted, comment out dir.Normalize().  Only works for joystick.
                dir = new Vector2(currState.ThumbSticks.Left.X,
                    -currState.ThumbSticks.Left.Y);

                if (Keyboard.GetState().IsKeyDown(Keys.Up) || currState.DPad.Up == ButtonState.Pressed)
                    dir.Y--;
                if (Keyboard.GetState().IsKeyDown(Keys.Down) || currState.DPad.Down == ButtonState.Pressed)
                    dir.Y++;
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || currState.DPad.Left == ButtonState.Pressed)
                    dir.X--;
                if (Keyboard.GetState().IsKeyDown(Keys.Right) || currState.DPad.Right == ButtonState.Pressed)
                    dir.X++;
                
                if (dir != Vector2.Zero)
                {
                    dir.Normalize();
                    return dir * speed;
                }

                else
                    return Vector2.Zero;
            }
        }

        /// <summary>
        /// Updates position of player, bounding it to the map.
        /// </summary>
        /// <param name="map">Map to bound player to.</param>
        public void Update(TileMap map)
        {
            pos += Velocity;

            pos.X = MathHelper.Clamp(pos.X, 0f, map.Width - fWidth);  //Remove "fWidth" to allow player to move offscreen at far right.
            pos.Y = MathHelper.Clamp(pos.Y, 0f, map.Height - fHeight);  //Remove "fHeight" to allow player to move offscreen at far bottom.
        }
    }
}
