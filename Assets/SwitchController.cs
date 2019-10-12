using UnityEngine.Events;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Animator animator;
    public UnityEvent onToggle;

    private bool toggled = false;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Toggle()
    {
        toggled = !toggled;
        onToggle.Invoke();
    }


    public bool getToggled() { return toggled; }
}
