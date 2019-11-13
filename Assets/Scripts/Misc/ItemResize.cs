using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemResize : MonoBehaviour
{
    public float x = 0.1f;
    public float y = 0.1f;
    public float z = 0.1f;
    void Update()
    {
        // Widen the object by x, y, and z values
        transform.localScale += new Vector3(x, y, z);
    }
}
