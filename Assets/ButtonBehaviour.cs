using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        FinishRound();
    }

    private void Update()
    {
        
    }

    public void StartRound()
    {
        animator.SetTrigger("StartRound");
    }

    public void FinishRound()
    {
        animator.SetTrigger("FinishRound");
    }
}
