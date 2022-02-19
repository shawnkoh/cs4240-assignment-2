using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class UIController : MonoBehaviour {
    public GameObject furniture0Prefab;
    public GameObject furniture1Prefab;
    public GameObject furniture2Prefab;
    private Button _furnitureButton0;
    private Button _furnitureButton1;
    private Button _furnitureButton2;
    private Button _primaryButton;
    private Button _deselectButton;

    private ARTapToPlaceObject _arTapToPlaceObject;
    private ARRaycastManager _arRaycastManager;

    private GameObject _editingFurniture;

    private GameObject _selectedIndicator;

    // Start is called before the first frame update
    private void Start() {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        _furnitureButton0 = root.Q<Button>("furniture0");
        _furnitureButton1 = root.Q<Button>("furniture1");
        _furnitureButton2 = root.Q<Button>("furniture2");
        _primaryButton = root.Q<Button>("primary");
        _deselectButton = root.Q<Button>("deselect");

        _furnitureButton0.clicked += () => SelectFurniture(furniture0Prefab);
        _furnitureButton1.clicked += () => SelectFurniture(furniture1Prefab);
        _furnitureButton2.clicked += () => SelectFurniture(furniture2Prefab);
        _primaryButton.clicked += PrimaryButtonPressed;
        _deselectButton.clicked += DeselectButtonPressed;

        _arTapToPlaceObject = GameObject.Find("Interaction").GetComponent<ARTapToPlaceObject>();

        _primaryButton.SetEnabled(false);
        _deselectButton.SetEnabled(false);

        _selectedIndicator = GameObject.Find("SelectedIndicator");
    }

    private bool _isDraggable = false;

    // Update is called once per frame
    private void Update() {
        if (_editingFurniture != null) {
            _selectedIndicator.SetActive(true);
            var offset = _editingFurniture.transform.localScale.y / 2 + 100;
            _selectedIndicator.transform.position = _editingFurniture.transform.position + Vector3.up * offset;
        } else {
            _selectedIndicator.SetActive(false);
        }

        if (_arTapToPlaceObject.objectToPlace != null || Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);

        RaycastHit hit;
        var ray = Camera.current.ScreenPointToRay(touch.position);
        var hits = new List<ARRaycastHit>();

        if (!_arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
            return;

        if (touch.phase == TouchPhase.Began) {
            if (!Physics.Raycast(ray, out hit))
                return;
            if (hit.collider.gameObject.tag != "Spawnable")
                return;
            _editingFurniture = hit.collider.gameObject;
            _isDraggable = true;
        } else if (_isDraggable && touch.phase == TouchPhase.Moved && _editingFurniture != null) {
            _editingFurniture.transform.position = hits[0].pose.position;
        } else if (touch.phase == TouchPhase.Ended) {
            _isDraggable = false;
        }

        if (_editingFurniture != null)
            return;
        _primaryButton.SetEnabled(true);
        _primaryButton.text = "Delete Furniture";
        _deselectButton.SetEnabled(true);
    }

    private void SelectFurniture(GameObject prefab) {
        _editingFurniture = null;
        if (_arTapToPlaceObject.objectToPlace == prefab)
            _arTapToPlaceObject.objectToPlace = null;
        else
            _arTapToPlaceObject.objectToPlace = prefab;

        _primaryButton.SetEnabled(_arTapToPlaceObject.objectToPlace != null);
        _primaryButton.text = "Place Furniture";
        _deselectButton.SetEnabled(false);
    }

    private void PrimaryButtonPressed() {
        if (_editingFurniture == null) {
            _arTapToPlaceObject.PlaceObject();
        } else {
            Destroy(_editingFurniture);
            _editingFurniture = null;
        }

        _primaryButton.SetEnabled(false);
    }

    private void DeselectButtonPressed() {
        _editingFurniture = null;
        _primaryButton.SetEnabled(false);
        _deselectButton.SetEnabled(false);
        _primaryButton.text = "Place Furniture";
    }
}