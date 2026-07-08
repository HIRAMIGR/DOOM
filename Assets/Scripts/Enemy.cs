using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Enemy : MonoBehaviour
{
   [SerializeField]
   protected Animator animator;
   [SerializeField]
   protected float damage = 20f;
   protected Health health;
   protected Transform player;
   private UnityEvent onDied = new UnityEvent();
   public UnityEvent OnDied => onDied;
   protected bool didWin = false;
    protected Health playerHealth;
   private void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();
    }
    protected bool CheckWin()
    {
        if (playerHealth.IsDead && !didWin)
        {
            StopAllCoroutines();
            didWin = true;
            Dance();
        }
        return playerHealth.IsDead;
    }
    public virtual void OnEnable()
    {
        health.InitializeHealth();
        didWin = false;
    }
    public virtual void Dance()
    {
        animator.Play("Dance", 0, 0f);
    }
    public virtual void TakeDamage()
    {
        animator.Play("Damage", 0, 0f);
    }
    public virtual void Die()
    {
        GetComponent<Collider>().enabled = false;
        onDied?.Invoke();
        StopAllCoroutines();
        animator.Play("Death", 0, 0f);
        StartCoroutine(DieCoroutine());

    }
    private IEnumerator DieCoroutine()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}
