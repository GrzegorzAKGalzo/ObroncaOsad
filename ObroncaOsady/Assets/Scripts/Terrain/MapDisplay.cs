using System.Collections;
using UnityEngine;

// This class is used to display a map
public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender; // This is the renderer for the texture
    public MeshFilter meshFilter; // This is the mesh filter
    public MeshRenderer meshRenderer; // This is the mesh renderer

    // This method is used to draw a noise map
    public void DrawTexture(Texture2D texture) {
        

        textureRender.sharedMaterial.mainTexture = texture; // Set the texture of the renderer
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height); // Set the scale of the renderer
    }

    // This method is used to draw a mesh
    public void DrawMesh(MeshData meshData, Texture2D texture) {
        meshFilter.sharedMesh = meshData.CreateMesh(); // Set the mesh of the mesh filter
        meshRenderer.sharedMaterial.mainTexture = texture; // Set the texture of the mesh renderer
    }
}
