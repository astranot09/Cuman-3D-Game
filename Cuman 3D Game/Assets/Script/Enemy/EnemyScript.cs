using UnityEngine;

public class EnemyScript : Entity
{
    [Header("Enemy Attack Settings")]
    [SerializeField] private float damageAmount = 10f; // Jumlah damage yang ingin diberikan

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"{gameObject.name} menabrak");


    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<IDamageable>().TakeDamage( damageAmount );
    //    }
    //}


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} menabrak");


        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().TakeDamage(damageAmount);
        }
        
    }


    protected override void Die()
    {
        Debug.Log("Confetti");
        currentHealth = maxHealth;
        healthBar.HealthBarInitiation();
    }


}