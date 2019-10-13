
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PressureController : MonoBehaviour
{

    public List<Lockable> lockables;

    public Animator animator;

    public int power = 1;

    public int neededPressure = 1;

    public int currentPressure = 0;

    private bool toggled = false;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Toggle()
    {
        toggled = !toggled;

        foreach (Lockable l in lockables)
            l.AddCurrent((l.locked) ? power : -power);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Debug.Log("Yiipie");
            currentPressure += collision.GetComponentInChildren<Grabbable>().weight;
            if (currentPressure >= neededPressure)
            {
                Toggle();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            currentPressure -= collision.GetComponentInChildren<Grabbable>().weight;
            Toggle();
        }
    }

    public bool getToggled() { return toggled; }
}
