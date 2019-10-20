using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockable : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public bool locked;
    public int neededCurent = -1;

    private int current = 0;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        locked = current < neededCurent;
        CheckLockState();
    }




    public void AddCurrent(int current = 1)
    {
        this.current += current;
        CheckLockState();
    }

    private void CheckLockState()
    {
        if (locked && current >= neededCurent)
            Unlock();
        else if (!locked && current < neededCurent)
            Lock();
        else
            Debug.Log("Weird Lock state");
        
    }

    protected virtual void Lock()
    {
        if(animator != null)
            animator.Play("");
        if (spriteRenderer != null)
            spriteRenderer.color = new Vector4(1, 0, 0, 1);
        locked = true;
    }

    protected virtual void Unlock()

    {
        if (animator != null)
            animator.Play("");
        if(spriteRenderer != null)
            spriteRenderer.color = new Vector4(0, 1, 0, 1);
        locked = false;
    }
}
