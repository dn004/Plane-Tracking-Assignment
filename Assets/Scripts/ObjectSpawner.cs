using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject createdObject;
    private bool objectSpawned = false;
    public ColorSwitcher colorSwitcher;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        Vector2 inputPosition = Vector2.zero;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            inputPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }


        if (!objectSpawned && arRaycastManager.Raycast(inputPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            createdObject = Instantiate(objectPrefab, hitPose.position, hitPose.rotation);
            objectSpawned = true;
            colorSwitcher.AssignObject(createdObject);
        }
    }
}
