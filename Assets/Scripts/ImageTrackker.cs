using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTrackker : MonoBehaviour
{

    private ARTrackedImageManager Manager;

    public GameObject Prefab1;
    public GameObject Prefab2;

    private Dictionary<int, GameObject> InstanciatedObjects = new Dictionary<int, GameObject>();


    void OnEnable()
    {
        Manager = GetComponent<ARTrackedImageManager>();
        Manager.trackedImagesChanged += OnImageEvent;
    }

    void OnDisable()
    {
        Manager.trackedImagesChanged -= OnImageEvent;
    }

    void OnImageEvent(ARTrackedImagesChangedEventArgs Args)
    {
    
        foreach (ARTrackedImage i in Args.added)
        {
            if (i.referenceImage.name == "one")
            {
                InstanciatedObjects.Add(i.GetInstanceID(), Instantiate(Prefab1, i.transform.position, i.transform.rotation));
            }
            if (i.referenceImage.name == "two")
            {
                InstanciatedObjects.Add(i.GetInstanceID(), Instantiate(Prefab2, i.transform.position, i.transform.rotation));
            }
        }

        foreach (ARTrackedImage i in Args.updated)
        {
            int id = i.GetInstanceID();
            if (InstanciatedObjects.ContainsKey(id))
            {
                InstanciatedObjects[id].transform.position = i.transform.position;
                InstanciatedObjects[id].transform.rotation = i.transform.rotation;
            }
        }

        foreach (ARTrackedImage i in Args.removed)
        {
            int id = i.GetInstanceID();
            if (InstanciatedObjects.ContainsKey(id))
            {
                Object.Destroy(InstanciatedObjects[id]);
                InstanciatedObjects.Remove(id);
            }
        }

    }

}
