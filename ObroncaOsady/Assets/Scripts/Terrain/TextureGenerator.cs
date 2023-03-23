using System.Collections;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height); // This is the texture
        texture.filterMode = FilterMode.Point; // Set the filter mode
        texture.wrapMode = TextureWrapMode.Clamp; // Set the wrap mode
        texture.SetPixels(colourMap); // Set the colors of the texture
        texture.Apply(); // Apply the texture
        return texture; // Return the texture
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0); // This is the width of the noise map
        int height = heightMap.GetLength(1); // This is the height of the noise map

        Color[] colourMap = new Color[width * height]; // This is the color map

        // Loop through the noise map and set the color at each point
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]); // Set the color at the point
            }
        }

        return TextureFromColourMap(colourMap, width, height); // Return the texture
    }
}
