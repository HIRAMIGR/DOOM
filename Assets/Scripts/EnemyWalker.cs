using UnityEngine;
using System.Collections;
 
public class EnemyWalker : Enemy
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float attackRange = 1.5f;
    [SerializeField]
    private float damageRange = 2f;
    [SerializeField]
    private float attackTime = 1f;
    private enum State
    {
        Appearing,
        Following,
        Attacking,
        Death,
    }
    private State currentState = State.Appearing;
    private bool IsInRange => Vector3.Distance(transform.position, player.position) <= attackRange;
    private bool IsInDamageRange => Vector3.Distance(transform.position, player.position) <= damageRange;
    public override void OnEnable()
    {
        SoundManager.instance.Play("hellKnight_appear");
        base.OnEnable();
        currentState = State.Appearing;
        StartCoroutine(AppearCoroutine());
    }
    private IEnumerator AppearCoroutine()
    {
        animator.Play("Appear", 0, 0f);
        yield return animator.WaitForCurrentAnimation();
        currentState = State.Following;
    }
    private void Update()
    {
        if (health.IsDead) return;
        if (CheckWin()) return;
        if (currentState == State.Following)
        {
            if (IsInRange)
            {
                currentState = State.Attacking;
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                animator.Play("Run");
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
           transform.LookAt(player);
    }
    private IEnumerator AttackCoroutine()
    {
        SoundManager.instance.Play("hellKnight_attack");
        animator.Play("Attack", 0, 0f);
        yield return new WaitForSeconds(attackTime);
        if (IsInDamageRange)
        {
            player.GetComponent<Health>().TakeDamage(damage);
            player.GetComponent<Player>().PushBack(transform, 5f);
        }
        yield return animator.WaitForCurrentAnimation();
        currentState = State.Following;
    }
    public override void Die()
    {
        currentState = State.Death;
        rb.isKinematic = true;
        SoundManager.instance.Play("hellKnight_die");
        base.Die();
    }
}
 
 
 