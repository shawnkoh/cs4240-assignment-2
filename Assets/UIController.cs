using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button button0;
    public Button button1;
    public Button button2;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        button0 = root.Q<Button>("button0");
        button1 = root.Q<Button>("button1");
        button2 = root.Q<Button>("button2");

        button0.clicked += Button0Pressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Button0Pressed()
    {
        button0.text = "clicked";
    }
}
