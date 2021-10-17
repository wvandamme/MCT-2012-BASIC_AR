using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppManager : MonoBehaviour
{
    [System.Serializable]
    public struct ImagePrefab
    {
        public string name;
        public GameObject prefab;
    }

    public Camera ARCamera;
    public Canvas Menu;
    public GameObject ARCursorPrefab;
    public ImagePrefab[] ImagePrefabs;
    public GameObject TouchPrefab;

    private HashSet<ColliderClient> CollidingObjects = new HashSet<ColliderClient>();
    private TouchPhase last_phase = TouchPhase.Began;
    private GameObject ARCursor;

    public void OnEnable()
    {
        ARCursor = Instantiate(ARCursorPrefab, transform);
        ARCursor.SetActive(false);
        Menu.gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        Object.Destroy(ARCursor);
    }

    public void AddColliderClient(ColliderClient client)
    {
        CollidingObjects.Add(client);
        Debug.Log(CollidingObjects.Count);
        Menu.gameObject.SetActive(CollidingObjects.Count > 0);
    }

    public void RemoveColliderClient(ColliderClient client)
    {
        CollidingObjects.Remove(client);
        Menu.gameObject.SetActive(CollidingObjects.Count > 0);
    }

    public GameObject GetImagePrefab(string name)
    {
        foreach (ImagePrefab i in ImagePrefabs)
        {
            if (i.name == name)
            {
                return i.prefab;
            }
        }
        return null;
    }

    public void OnDrawObjectsRed()
    {
        foreach (var obj in CollidingObjects)
        {
            obj.SetColor(Color.red);
        }
    }

    public void OnDrawObjectsGreen()
    {
        foreach (var obj in CollidingObjects)
        {
            obj.SetColor(Color.green);
        }
    }

    public void OnDrawObjectsBlue()
    {
        foreach (var obj in CollidingObjects)
        {
            obj.SetColor(Color.blue);
        }
    }

    public void OnDrawObjectsWhite()
    {
        foreach (var obj in CollidingObjects)
        {
            obj.SetColor(Color.white);
        }
    }

    public void EnableARCursor(Vector3 position, Quaternion rotation)
    {
        ARCursor.SetActive(true);
        ARCursor.transform.position = position;
        ARCursor.transform.rotation = rotation;
    }

    public void DisableARCursor()
    {
        //ARCursor.SetActive(false);
    }

    void Update()
    {
      
        if (Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        if ((touch.phase == TouchPhase.Ended) && (last_phase != TouchPhase.Ended))
        {
            if (ARCursor.activeSelf)
            {
                Instantiate(TouchPrefab, ARCursor.transform.position, ARCursor.transform.rotation);
            }
        }

        last_phase = touch.phase;

    }

}
