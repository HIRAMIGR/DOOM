using UnityEngine;

public class HealthItem : MonoBehaviour
{
[SerializeField]
private float healAmount = 20f;
private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null && playerHealth.CurrentHealth < playerHealth.MaxHealth)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
