﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class General_events : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Event.Invoke();
    }
}
