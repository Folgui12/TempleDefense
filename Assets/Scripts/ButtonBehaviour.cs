using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    private Animator animator;
    private bool ButtonActive = true;
    public float DeactiveTime;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        FinishRound();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "TutorialScene" && !ButtonActive)
        {
            timer += Time.deltaTime;

            if(timer > DeactiveTime)
            {
                FinishRound();
                timer = 0;
            }
        }
    }

    public void StartRound()
    {
        ButtonActive = false;
        animator.SetTrigger("StartRound");
    }

    public void FinishRound()
    {
        ButtonActive = true;
        animator.SetTrigger("FinishRound");
    }
}
