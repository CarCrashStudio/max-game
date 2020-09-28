using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public InputEvent[] inputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (InputEvent input in inputs)
        {
            if(Input.GetKeyDown(input.primaryKey) || 
               Input.GetKeyDown(input.secondaryKey) || 
               Input.GetKeyDown(input.teritiaryKey))
            {
                input.action.Invoke();
            }
        }   
    }
}

[Serializable]
public class InputEvent
{
    public KeyCode primaryKey;
    public KeyCode secondaryKey;
    public KeyCode teritiaryKey;
    public UnityEvent action;
}
