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
    public GameObject furniture2Prefab;
    private Button furniture0;
    private Button furniture1;
    private Button furniture2;
    private Button _placeButton;
    private Button _deleteButton;

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
        _placeButton = root.Q<Button>("place");

        furniture0.clicked += Furniture0Pressed;
        furniture1.clicked += Furniture1Pressed;
        // furniture2.clicked += SelectFurniture(furniture2Prefab);
        _placeButton.clicked += PlaceButtonPressed;
        
        _arTapToPlaceObject = GameObject.Find("Interaction").GetComponent<ARTapToPlaceObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_arTapToPlaceObject.objectToPlace != null || Input.touchCount == 0)
            return;
        
        var touch = Input.GetTouch(0);
        
        RaycastHit hit;
        Ray ray = Camera.current.ScreenPointToRay(touch.position);
        var hits = new List<ARRaycastHit>();
        // FIXME: Unsure if should filter here
        if (_arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            if (touch.phase == TouchPhase.Began && _editingFurniture == null)
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Spawnable")
                {
                    _editingFurniture = hit.collider.gameObject;
                }
            } else if (touch.phase == TouchPhase.Moved && _editingFurniture != null)
            {
                _editingFurniture.transform.position = hits[0].pose.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _editingFurniture = null;
            }
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
        _placeButton.SetEnabled(_arTapToPlaceObject.objectToPlace != null);
    }

    void PlaceButtonPressed()
    {
        _arTapToPlaceObject.PlaceObject();
        _placeButton.SetEnabled(false);
    }
}
