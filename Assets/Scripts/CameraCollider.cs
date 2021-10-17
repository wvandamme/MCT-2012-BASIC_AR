using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CameraCollider : MonoBehaviour
{

    public AppManager app;

    private void OnTriggerEnter(Collider other)
    {
        ColliderClient obj = other.GetComponent<ColliderClient>();
        if (obj) app.AddColliderClient(obj);
    }

    private void OnTriggerExit(Collider other)
    {
        ColliderClient obj = other.GetComponent<ColliderClient>();
        if (obj) app.RemoveColliderClient(obj);
    }

}
