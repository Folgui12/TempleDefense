using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoxController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name != "TutorialScene")
            WaveSpawner.Instance.RemoveEnemy(other.gameObject);

        other.gameObject.GetComponent<BaseEnemyModel>().TakeDamage(99999);
    }
}
