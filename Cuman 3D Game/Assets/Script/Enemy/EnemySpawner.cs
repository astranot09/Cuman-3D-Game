using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] bool alreadySpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadySpawned)
        {
            alreadySpawned = true;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
