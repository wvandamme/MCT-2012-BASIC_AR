using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracker : MonoBehaviour
{

   
    private ARTrackedImageManager Manager;

    public AppManager App;


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
            GameObject obj = App.GetImagePrefab(i.referenceImage.name);
            if (obj)
            {
                InstanciatedObjects.Add(i.GetInstanceID(), Instantiate(obj, i.transform.position, i.transform.rotation));
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
