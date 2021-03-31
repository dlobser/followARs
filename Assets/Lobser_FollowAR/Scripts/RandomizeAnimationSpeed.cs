using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimationSpeed : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        animator.speed = Random.Range(.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
