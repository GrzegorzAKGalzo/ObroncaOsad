using System.Collections;
using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

// This class is used to generate a map
public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColourMap, Mesh, FallofMap}; // This is the draw mode
    public DrawMode drawMode; // This is the draw mode
    public Noise.NormalizeMode normalizeMode; // This is the normalize mode

    public bool useFlatShading; // This is whether or not to use flat shading
    [Range(0, 6)]
    public int editorPreviewLOD; // This is the level of detail
    public float scale; // This is the scale of the map

    [Range(1, 17)]
    public int octaves; // This is the number of octaves
    [Range(0, 1)]
    public float persistance; // This is the persistance
    public float lacunarity; // This is the lacunarity

    public int seed; // This is the seed
    public Vector2 offset; // This is the offset

    public bool useFallof; // This is whether or not to use the fallof
    [Range(-10, 10)]
    public float fallofCurve = 1; // This is the fallof curve
    [Range(0, 25)]
    public float fallofShift = 1; // This is the fallof shift

    public float meshHeightMultiplier; // This is the mean height
    public AnimationCurve meshHeightCurve; // This is the height curve

    public bool autoUpdate; // This is whether or not to auto update the map

    public TerrainType[] regions; // This is the terrain types
    static MapGenerator instance; // This is the map generator instance

    float[,] fallofMap; // This is the fallof map

    Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>(); // This is the map data thread info queue
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>(); // This is the mesh data thread info queue

    void Awake() { // This method is called when the script is first loaded
        fallofMap = FallofGenerator.GenerateFallofMap(mapChunkSize, fallofCurve, fallofShift); // Generate the fallof map
    }

    public static int mapChunkSize {
        get {
            if (instance == null) {
                instance = FindObjectOfType<MapGenerator>();
            }
            if (instance.useFlatShading) {
                return 95;
            } else {
                return 239;
            }
        }
    }

    public void DrawMapInEditor() {
        MapData mapData = GenerateMapData(Vector2.zero); // This is the map data
        MapDisplay display = FindObjectOfType<MapDisplay>(); // This is the map display
        if (drawMode == DrawMode.NoiseMap) { // If the draw mode is noise map
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap)); // Draw the noise map
        } else if (drawMode == DrawMode.ColourMap) { // If the draw mode is colour map
            display.DrawTexture(TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize)); // Draw the colour map
        } else if (drawMode == DrawMode.Mesh) { // If the draw mode is mesh
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorPreviewLOD, useFlatShading), TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize)); // Draw the mesh
        } else if (drawMode == DrawMode.FallofMap) { // If the draw mode is fallof map
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FallofGenerator.GenerateFallofMap(mapChunkSize, fallofCurve, fallofShift))); // Draw the fallof map
        }
    }

    public void RequestMapData(Vector2 centre, Action<MapData> callback) { // This method is used to request the map data
        ThreadStart threadStart = delegate { // This is the thread start
            MapDataThread(centre, callback); // Call the map data thread
        };

        new Thread(threadStart).Start(); // Start the thread
    }

    public void MapDataThread(Vector2 centre, Action<MapData> callback) { // This method is used to generate the map data
        MapData mapData = GenerateMapData(centre); // This is the map data
        lock (mapDataThreadInfoQueue) { // Lock the map data thread info queue
            mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData)); // Add the map data to the queue
        }
    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback) { // This method is used to request the mesh data
        ThreadStart threadStart = delegate { // This is the thread start
            MeshDataThread(mapData, lod, callback); // Call the mesh data thread
        };

        new Thread(threadStart).Start(); // Start the thread
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback) { // This method is used to generate the mesh data
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod, useFlatShading); // This is the mesh data
        lock (meshDataThreadInfoQueue) { // Lock the mesh data thread info queue
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData)); // Add the mesh data to the queue
        }
    }

    void Update() {
        if (mapDataThreadInfoQueue.Count > 0) { // If there is something in the queue
            for (int i = 0; i < mapDataThreadInfoQueue.Count; i++) { // Loop through the queue
                MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue(); // This is the thread info
                threadInfo.callback(threadInfo.parameter); // Call the callback
            }
        }

        if (meshDataThreadInfoQueue.Count > 0) { // If there is something in the queue
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++) { // Loop through the queue
                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue(); // This is the thread info
                threadInfo.callback(threadInfo.parameter); // Call the callback
            }
        }
    }
    // This method is called when the script is loaded
    MapData GenerateMapData(Vector2 centre) {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, scale, octaves, persistance, lacunarity, centre+offset, normalizeMode); // This is the noise map

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize]; // This is the colour map
        for (int y = 0; y < mapChunkSize; y++) { // Loop through the noise map
            for (int x = 0; x < mapChunkSize; x++) {
                if (useFallof) { // If the fallof is enabled
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - fallofMap[x, y]); // Apply the fallof
                }
                float currentHeight = noiseMap[x, y]; // This is the current height
                for (int i = 0; i < regions.Length; i++) { // Loop through the terrain types
                    if (currentHeight >= regions[i].height) { // If the current height is less than the terrain type height
                        colourMap[y * mapChunkSize + x] = regions[i].color; // Set the color at the point
                    } else {
                        break;
                    }
                }
            }
        }
    
        return new MapData(noiseMap, colourMap); // Return the map data
    }

    void OnValidate() { // This is called when the script is loaded or a value is changed in the inspector
        if (lacunarity < 1) { // If the lacunarity is less than 1
            lacunarity = 1; // Set the lacunarity to 1
        }
        if (octaves < 0) { // If the octaves is less than 0
            octaves = 0; // Set the octaves to 0
        }
        fallofMap = FallofGenerator.GenerateFallofMap(mapChunkSize, fallofCurve, fallofShift); // Generate the fallof map
    }

    struct MapThreadInfo<T> {
        public readonly Action<T> callback; // This is the callback
        public readonly T parameter; // This is the parameter

        public MapThreadInfo(Action<T> callback, T parameter) { // This is the constructor
            this.callback = callback; // Set the callback
            this.parameter = parameter; // Set the parameter
        }
    }
}

[System.Serializable] // This allows the struct to be seen in the inspector
public struct TerrainType
{
    public string name; // This is the name of the terrain type
    public float height; // This is the height of the terrain type
    public Color color; // This is the color of the terrain type    
}

public struct MapData {
    public readonly float[,] heightMap; // This is the height map
    public readonly Color[] colourMap; // This is the colour map

    public MapData(float[,] heightMap, Color[] colourMap) { // This is the constructor
        this.heightMap = heightMap; // Set the height map
        this.colourMap = colourMap; // Set the colour map
    }
}