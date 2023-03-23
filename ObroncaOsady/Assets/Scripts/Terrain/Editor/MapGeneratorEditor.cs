using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))] // This is the custom editor for the MapGenerator class
// This class is used to create a custom editor for the MapGenerator class
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target; // Cast the target to a MapGenerator

        if (DrawDefaultInspector()) { // If the default inspector is drawn
            if (mapGen.autoUpdate) {
                mapGen.DrawMapInEditor(); // Generate the map
            }
        }

        if (GUILayout.Button("Generate")) // If the generate map button is pressed
        {
            mapGen.DrawMapInEditor(); // Generate the map
        }
    }
}
