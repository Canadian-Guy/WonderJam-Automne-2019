
﻿using System.Collections.Generic;
using UnityEngine.Events;

using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public List<Lockable> lockables;

    public Animator animator;

    public int power = 1;


    private bool toggled = false;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Toggle()
    {
        toggled = !toggled;

        
        foreach(Lockable l in lockables)
            l.AddCurrent((l.locked) ? power : -power);

    }


    public bool getToggled() { return toggled; }
}
