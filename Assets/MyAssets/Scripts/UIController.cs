using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class UIController : MonoBehaviour
{
    public GameObject furniture0Prefab;
    public GameObject furniture1Prefab;
    private Button furniture0;
    private Button furniture1;
    private Button place;

    private ARTapToPlaceObject _arTapToPlaceObject;
    private ARRaycastManager _arRaycastManager;

    private GameObject _editingFurniture;

    // Start is called before the first frame update
    void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        furniture0 = root.Q<Button>("furniture0");
        furniture1 = root.Q<Button>("furniture1");
        place = root.Q<Button>("place");

        furniture0.clicked += Furniture0Pressed;
        furniture1.clicked += Furniture1Pressed;
        place.clicked += PlacePressed;
        
        _arTapToPlaceObject = GameObject.Find("Interaction").GetComponent<ARTapToPlaceObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_arTapToPlaceObject.objectToPlace != null || Input.touchCount == 0)
            return;
        
        var touch = Input.GetTouch(0);
        var hits = new List<ARRaycastHit>();
        _arRaycastManager.Raycast(touch.position, hits);
        if (hits.Count == 0)
            return;
        var hit = hits[0];
        var hitObject = hit.trackable.gameObject;
        
        Debug.Log(hitObject.tag);
        if (hitObject.tag != "Spawnable")
            return;
        
        _editingFurniture = hitObject;

        switch (touch.phase)
        {
            case TouchPhase.Began:
            case TouchPhase.Moved:
                _editingFurniture.transform.position = hit.pose.position;
                break;
            case TouchPhase.Ended:
                _editingFurniture = null;
                break;
        }
    }

    void Furniture0Pressed()
    {
        SelectFurniture(furniture0Prefab);
        // FIXME: Highlight button
    }
    
    void Furniture1Pressed()
    {
        SelectFurniture(furniture1Prefab);
    }

    void SelectFurniture(GameObject prefab)
    {
        if (_arTapToPlaceObject.objectToPlace == prefab)
        {
            _arTapToPlaceObject.objectToPlace = null;
        }
        else
        {
            _arTapToPlaceObject.objectToPlace = prefab;
        }
        place.SetEnabled(_arTapToPlaceObject.objectToPlace != null);
    }

    void PlacePressed()
    {
        _arTapToPlaceObject.PlaceObject();
        place.SetEnabled(false);
    }
}
