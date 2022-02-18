using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    private UIController _uiController;

    private ARTapToPlaceObject _arTapToPlaceObject;
    // Start is called before the first frame update
    void Start()
    {
        _uiController = GameObject.Find("UIDocument").GetComponent<UIController>();
        _arTapToPlaceObject = GameObject.Find("Interaction").GetComponent<ARTapToPlaceObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
