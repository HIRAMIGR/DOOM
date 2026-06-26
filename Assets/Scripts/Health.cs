using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Health: MonoBehaviour
{
[SerializeField]
private Slider healthBar;
[SerializeField]
private float maxHealth = 100f;
[SerializeField]
private UnityEvent onDeath;
[SerializeField]
private UnityEvent onDamageTaken;
private float currentHealth;
public float CurrentHealth => currentHealth;
public void InitializeHealth()
{
    currentHealth = maxHealth;
    UpdateHealthBar();
}
private void UpdateHealthBar()
{
    healthBar.value = currentHealth / maxHealth;
}
public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }
public void TakeDamage (float damage)
{
    currentHealth -= damage;
    currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    UpdateHealthBar();
    if (currentHealth <= 0f)
    {
    Die();
    }
    else
    {
    onDamageTaken?.Invoke();
    }
}
public void Die()
    {
        onDeath?.Invoke();
    
    }
}