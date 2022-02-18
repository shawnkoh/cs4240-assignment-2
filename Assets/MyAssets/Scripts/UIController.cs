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
    private Button _furnitureButton0;
    private Button _furnitureButton1;
    private Button _furnitureButton2;
    private Button _primaryButton;

    private ARTapToPlaceObject _arTapToPlaceObject;
    private ARRaycastManager _arRaycastManager;

    private GameObject _editingFurniture;

    // Start is called before the first frame update
    void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        _furnitureButton0 = root.Q<Button>("furniture0");
        _furnitureButton1 = root.Q<Button>("furniture1");
        _furnitureButton2 = root.Q<Button>("furniture2");
        _primaryButton = root.Q<Button>("primary");

        _furnitureButton0.clicked += FurnitureButton0Pressed;
        _furnitureButton1.clicked += FurnitureButton1Pressed;
        _furnitureButton2.clicked += () => SelectFurniture(furniture2Prefab);
        _primaryButton.clicked += PrimaryButtonPressed;
        
        _arTapToPlaceObject = GameObject.Find("Interaction").GetComponent<ARTapToPlaceObject>();
    }

    private bool _isDraggable = false;

    // Update is called once per frame
    void Update()
    {
        if (_arTapToPlaceObject.objectToPlace != null || Input.touchCount == 0)
            return;
        
        var touch = Input.GetTouch(0);
        
        RaycastHit hit;
        Ray ray = Camera.current.ScreenPointToRay(touch.position);
        var hits = new List<ARRaycastHit>();

        if (!_arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            return;
        
        if (touch.phase == TouchPhase.Began)
        {
            if (!Physics.Raycast(ray, out hit))
                return;
            if (hit.collider.gameObject.tag != "Spawnable")
                return;
            _editingFurniture = hit.collider.gameObject;
            _isDraggable = true;

        } else if (_isDraggable && touch.phase == TouchPhase.Moved && _editingFurniture != null)
        {
            _editingFurniture.transform.position = hits[0].pose.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            _isDraggable = false;
        }
    }

    void FurnitureButton0Pressed()
    {
        SelectFurniture(furniture0Prefab);
        // FIXME: Highlight button
    }
    
    void FurnitureButton1Pressed()
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
        _primaryButton.SetEnabled(_arTapToPlaceObject.objectToPlace != null);
    }

    void PrimaryButtonPressed()
    {
        _arTapToPlaceObject.PlaceObject();
        _primaryButton.SetEnabled(false);
    }
}
