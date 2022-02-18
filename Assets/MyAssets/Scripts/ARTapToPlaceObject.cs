using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;
    
    private Pose _placementPose;
    private ARRaycastManager _arRaycastManager;
    private bool _placementPoseIsValid = false;

    void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
    void Update()
    {
        if (objectToPlace == null)
        {
            placementIndicator.SetActive(false);
            return;
        }
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        if (hits.Count == 0)
        {
            _placementPoseIsValid = false;
            return;
        }
        var hit = hits[0];

        _placementPoseIsValid = true;
        if (!_placementPoseIsValid)
            return;
        _placementPose = hit.pose;
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void PlaceObject()
    {
        if (!_placementPoseIsValid)
        {
            return;
        }
        Instantiate(objectToPlace, _placementPose.position, _placementPose.rotation);
        objectToPlace = null;
    }
}
