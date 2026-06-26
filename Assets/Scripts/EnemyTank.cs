using UnityEngine;
using System.Collections;
public class EnemyTank : Enemy
{
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private float fireRate = 3f;
    [SerializeField]
    private Transform shootPivot;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float speed = 10f;
    private bool IsShooting = false;
    private float nextFireTime = 0f;

    public override void OnEnable()
    {
        base.OnEnable();
        nextFireTime = 0f;
        animator.Play("Appear", 1, 0f);
        SoundManager.instance.Play("MancubusAppear");
    }
    private bool IsInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }
    private void Update()
    {
        if (health.CurrentHealth <= 0) return;
        if (IsInRange())
        {
            IsShooting = true;
            if (Time.time >= nextFireTime)
            {
                StartCoroutine(ShootCoroutine());
                nextFireTime =  Time.time + fireRate;
            }
        }
        else if (!IsShooting)
        {
            FollowPlayer();
        }
        transform.LookAt(player.transform.position);
    }
    private void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        animator.Play("Walk");
    }
    private IEnumerator ShootCoroutine()
    {
        SoundManager.instance.Play("MancubusPrepareShoot");
        animator.Play("PrepareShoot", 0, 0f);
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SoundManager.instance.Play("MancubusShoot");
        animator.Play("Shoot", 0, 0f);
        Vector3 direction = (player.transform.position - shootPivot.position).normalized;
        shootPivot.forward = direction;
        GameObject bullet = Instantiate(bulletPrefab, shootPivot.position, shootPivot.rotation);
        bullet.transform.LookAt(player.transform.position);
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        IsShooting = false;
    }
    public override void Die()
    {
        base.Die();
        SoundManager.instance.Play("MancubusDie");
    }
}
