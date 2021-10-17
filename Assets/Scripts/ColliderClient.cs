using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderClient : MonoBehaviour
{

    public void SetColor(Color32 color)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer)
        {
            renderer.material = new Material(renderer.material.shader);
            renderer.material.SetColor("_Color", color);
        }
    }


}
