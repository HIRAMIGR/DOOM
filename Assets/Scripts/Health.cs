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
[SerializeField]
private UnityEvent<Transform> onHeal;
private float currentHealth;
public float CurrentHealth => currentHealth;
public float MaxHealth => maxHealth;
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
        onHeal?.Invoke(transform);
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