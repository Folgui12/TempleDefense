using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoxController : MonoBehaviour
{
    [SerializeField] private GameObject SmokeVFX;
    [SerializeField] private GameObject LightsVFX;
    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name != "TutorialScene")
            WaveSpawner.Instance.RemoveEnemy(other.gameObject);

        if(other.gameObject.layer == 3 || other.gameObject.layer == 17)
        {
            other.gameObject.GetComponent<BaseEnemyModel>().TakeDamage(99999);
            Instantiate(SmokeVFX, other.transform.position, transform.rotation);
            Instantiate(LightsVFX, other.transform.position, transform.rotation);
        }
    }
}
