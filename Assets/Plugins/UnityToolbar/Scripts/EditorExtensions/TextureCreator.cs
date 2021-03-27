using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Plugins.UnityToolbar
{
    public static class TextureCreator
    {
        public static Texture2D GetTextureOfColor(Vector2Int dimensions, Color color)
        {
            var texture2d = new Texture2D(dimensions.x, dimensions.y, TextureFormat.RGBA32, false);
            texture2d.SetPixels(GetColors(color, dimensions.x * dimensions.y).ToArray());
            texture2d.Apply();
            return texture2d;
        }

        private static IEnumerable<Color> GetColors(Color color, int size)
        {
            for (var i = 0; i < size; i++)
            {
                yield return color;
            }
        }
    }
}