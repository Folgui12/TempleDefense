using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (this.name == "StartGameTP")
                SceneLoader.Instance.LoadGameScene();
            else if (this.name == "ExitGame")
                SceneLoader.Instance.ExitGame();
        }
    }
}
