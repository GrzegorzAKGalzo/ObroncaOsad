using System.Collections;
using UnityEngine;

public static class FallofGenerator
{
    public static float[,] GenerateFallofMap(int size, float a = 3f, float b = 2.2f) {
        float[,] map = new float[size, size];
        Vector2 center = new Vector2(size / 2f, size / 2f);

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Vector2 pos = new Vector2(i, j);
                float distance = Vector2.Distance(pos, center) / (size / 2f);
                distance = Mathf.Clamp01(distance);
                map[i, j] = Evaluate(distance, a, b);
            }
        }

        return map;
    }

    static float Evaluate(float value, float a = 3f, float b = 2.2f) {
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
