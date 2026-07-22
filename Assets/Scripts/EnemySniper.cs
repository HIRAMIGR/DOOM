using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class EnemySniper : Enemy
{
[SerializeField]
private float range = 10f;
[SerializeField]
private float fireRate = 3f;
[SerializeField]
private float aimTime = 4f;
[SerializeField]
private Text timerText;
[SerializeField]
private LaserBeam laserBeam;
private float nextFireTime;
private bool IsInRange => Vector3.Distance(transform.position, player.position) <= range;
private bool isShooting = false;
public override void OnEnable()
    {
        isShooting = false;
        base.OnEnable();
        laserBeam.ActivateLaser(false);
        nextFireTime = 0f;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        animator.Play("Idle", 0, 0f);
        SoundManager.instance.Play("sniper_appear");
    }
    private void Update()
    {
        if (health.IsDead) return;
        if (CheckWin()) return;
        transform.LookAt(player);
        if (!isShooting && IsInRange && Time.time >= nextFireTime)
        {
            isShooting = true;
            StartCoroutine(AimAndShoot());
        }
    }
    private IEnumerator AimAndShoot()
    {
        laserBeam.Target = player;
        laserBeam.ActivateLaser(true);
        SoundManager.instance.Play("sniper_spotted");
        animator.Play("Aim", 0, 0f);
        yield return animator.WaitForCurrentAnimation();
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot ()
    {
        float duration = aimTime;
        while (duration > 0f)
        {
            duration--;
            timerText.text = duration.ToString();
            yield return new WaitForSeconds(1f);
        }
        animator.Play("Fire", 0, 0f);
        SoundManager.instance.Play("sniper_shoot");
        laserBeam.ActivateLaser(false);
        player.GetComponent<Health>().TakeDamage(damage);
        isShooting = false;
        nextFireTime = Time.time + fireRate;
    }
    public override void Die()
    {
        laserBeam.ActivateLaser(false);
        base.Die();
        SoundManager.instance.Play("sniper_death");
    }
}