using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField]
   protected Animator animator;
   [SerializeField]
   protected float damage = 20f;
   protected Health health;
   protected Transform player;
   private UnityEvent onDied = new UnityEvent();
   public UnityEvent OnDied => onDied;
   private void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public virtual void OnEnable()
    {
        health.InitializeHealth();
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
