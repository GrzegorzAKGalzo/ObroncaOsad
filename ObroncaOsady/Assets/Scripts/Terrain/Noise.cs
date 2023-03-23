using System.Collections;
using UnityEngine;

// This class is used to generate a noise map
public static class Noise
{
    public enum NormalizeMode { Local, Global }; // This is the normalize mode

    // This method generates a noise map
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, NormalizeMode normalizeMode) {
        float[,] noiseMap = new float[mapWidth, mapHeight]; // This is the noise map

        System.Random prng = new System.Random(seed); // This is the random number generator
        Vector2[] octaveOffsets = new Vector2[octaves]; // This is the octave offsets

        float maxPossibleHeight = 0; // This is the maximum possible height
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next(-100000, 100000) + offset.x; // This is the x offset
            float offsetY = prng.Next(-100000, 100000) - offset.y; // This is the y offset
            octaveOffsets[i] = new Vector2(offsetX, offsetY); // Set the octave offset

            maxPossibleHeight += amplitude; // Add the persistance to the maximum possible height
            amplitude *= persistance; // Increase the amplitude
        }

        if (scale <= 0) { // If the scale is 0 or less, set it to 0.0001
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue; // This is the maximum noise height
        float minLocalNoiseHeight = float.MaxValue; // This is the minimum noise height
        
        float halfWidth = mapWidth / 2f; // This is half the width of the map
        float halfHeight = mapHeight / 2f; // This is half the height of the map

        // Loop through the noise map and generate a noise value for each point
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x-halfWidth + octaveOffsets[i].x) / scale * frequency ; // This is the x coordinate of the noise map
                    float sampleY = (y-halfHeight + octaveOffsets[i].y) / scale * frequency; // This is the y coordinate of the noise map

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; // This is the noise value at the point
                    noiseHeight += perlinValue * amplitude; // Add the noise value to the noise height
                
                    amplitude *= persistance; // Increase the amplitude
                    frequency *= lacunarity; // Increase the frequency
                }

                if (noiseHeight > maxLocalNoiseHeight) { // If the noise height is greater than the maximum noise height
                    maxLocalNoiseHeight = noiseHeight; // Set the maximum noise height to the noise height
                } else if (noiseHeight < minLocalNoiseHeight) { // If the noise height is less than the minimum noise height
                    minLocalNoiseHeight = noiseHeight; // Set the minimum noise height to the noise height
                }
                noiseMap[x, y] = noiseHeight; // Set the noise value at the point
            }
        }

        // Loop through the noise map and normalize the noise values
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                if (normalizeMode == NormalizeMode.Local) {// If the normalize mode is local
                    noiseMap[x, y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x, y]); // Set the noise value at the point
                }
                else {// If the normalize mode is global
                    float normalizedHeight = (noiseMap[x, y] + 1) / maxPossibleHeight; // This is the normalized height
                    noiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue); // Set the noise value at the point
                }
            }
        } 

        return noiseMap; // Return the noise map
    }
}