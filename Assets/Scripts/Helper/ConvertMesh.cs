using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertMesh : MonoBehaviour
{

    [ContextMenu("Convert to regular mesh")]
    void convert()
    {
        //gets the skinned mesh renderer and creates a mesh renderer and mesh filter
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        //sets the shared mesh and materials to that of the skinned mesh renderer
        meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
        meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;

        DestroyImmediate(skinnedMeshRenderer);
        DestroyImmediate(this);
    }
}
