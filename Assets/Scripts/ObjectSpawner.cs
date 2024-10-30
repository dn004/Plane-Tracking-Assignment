using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    private GameObject placedObject;
    private bool hasSpawnedObject = false;
    public ColorChanger colorChanger;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        Vector2 screenPosition = GetTouchOrClickPosition();
        if (screenPosition == Vector2.zero || hasSpawnedObject)
            return;

        if (raycastManager.Raycast(screenPosition, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose placementPose = raycastHits[0].pose;
            placedObject = Instantiate(prefabToSpawn, placementPose.position, placementPose.rotation);
            hasSpawnedObject = true;
            colorChanger.SetTargetObject(placedObject);
        }
    }

    private Vector2 GetTouchOrClickPosition()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            return Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            return Mouse.current.position.ReadValue();
        }
        return Vector2.zero;
    }
}
