using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNA_Base
{
    class TileMap
    {
        int[,] map;  //2D array used for simple map making

        //Dimensions of each individual tile for this map
        int tileWidth;
        int tileHeight;


        /// <summary>
        /// Constructor for TileMap.
        /// </summary>
        /// <param name="width">Width of the entire map in terms of tiles.</param>
        /// <param name="height">Height of the entire map in terms of tiles.</param>
        /// <param name="tWidth">Width of each individual tile.</param>
        /// <param name="tHeight">Height of each individual tile.</param>
        public TileMap(int width, int height, int tWidth, int tHeight)
        {
            map = new int[height, width];
            tileWidth = tWidth;
            tileHeight = tHeight;
        }

        /// <summary>
        /// Gets height of map in pixels.
        /// </summary>
        public int Height
        {
            get { return map.GetLength(0) * tileHeight; }
        }

        /// <summary>
        /// Gets width of map in pixels.
        /// </summary>
        public int Width
        {
            get { return map.GetLength(1) * tileWidth; }
        }

        /// <summary>
        /// Draw method for TileMap.  Must know where on the list desired tile is.
        /// Map array is seen as map[HEIGHT, WIDTH], hence map[j, i], as height is in the zeroeth position.
        /// </summary>
        /// <param name="batch">Required spritebatch object from Game1.cs</param>
        /// <param name="textures">List of all textures.  Can be treated as 1-D array.</param>
        public void Draw(SpriteBatch batch, List<Texture2D> textures)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    batch.Draw(
                        textures[map[j, i]],
                        new Rectangle(
                                i * tileWidth,
                                j * tileHeight,
                                tileWidth,
                                tileHeight),
                            Color.White);
                }
            }
        }
    }
}
