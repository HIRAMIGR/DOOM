using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
 
public class Gun: MonoBehaviour
{
    [SerializeField]
private Animator animator;
[SerializeField]
private Rotate rotateScript;
[SerializeField]
private Gundata gundata;
public Gundata Gundata => gundata;
[SerializeField]
private Transform bulletPivot;
[SerializeField]
private GameObject bulletPrefab;
[SerializeField]
private GameObject fireParticlesPrefab;
[SerializeField]
private LayerMask aimLayerMask;
private Text ammoText;
private float nextFireTime;
private int totalBullets;
private int cartridgeBullets;
private UnityEvent onGunEmpty = new UnityEvent();
private Camera gunCamera;
private UnityEvent onGunShoot = new UnityEvent();
public UnityEvent OnGunShoot => onGunShoot;
public bool IsGunFull => totalBullets == gundata.totalBullets;
private float rayDistance = 1000f;
public UnityEvent OnGunEmpty
    {
      set => onGunEmpty = value;
      get => onGunEmpty;  
    }
    private void Awake()
    {
        gunCamera = Camera.main;
    }
private bool TryGetHit(out RaycastHit hit)
    {
        Ray ray = gunCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        return Physics.Raycast(ray, out hit, rayDistance, aimLayerMask);
    }
public bool IsAimingEnemy()
    {
        return TryGetHit(out RaycastHit hit) && hit.collider.CompareTag("Enemy");
    }
public void ChargeTotalBullets()
    {
        totalBullets = gundata.totalBullets;
    }
public void GrabGun (Transform gunPosition, Text bulletsText, bool isNew = true)
{
    ammoText = bulletsText;
    nextFireTime = 0f;
    if (isNew)
    {
        totalBullets = gundata.totalBullets;
        ChargeGun(false);
    }
transform.SetParent(gunPosition);
transform.localPosition = Vector3.zero;
transform.localRotation = Quaternion.identity;
animator. Play ("Idle", 0, 0f);
rotateScript.canRotate = false;
gameObject.GetComponent<Collider>().enabled = false;
UpdateAmmoText();
}
    public void ChargeGun(bool playAnimation = true)
    {
        if (totalBullets <= 0 || cartridgeBullets == gundata.cartridgeSize) return;
        SoundManager.instance.Play(gundata.reloadSoundName);
        if (playAnimation)
        {
            StartCoroutine(ChargeGunCoroutine());
        }
        else
        {
            AddBullets();
        }
    }
private IEnumerator ChargeGunCoroutine()
    {
        animator.Play("Charge", 0, 0f);
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        AddBullets();
    }

private void AddBullets()
    {
        cartridgeBullets = Mathf.Min(gundata.cartridgeSize, totalBullets);
        totalBullets -= cartridgeBullets;
        UpdateAmmoText();
    }
    private void UpdateAmmoText()
    {
        ammoText.text = $"{cartridgeBullets} / {totalBullets}";
    }
    private void DamageEnemy(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemy.GetComponent<Health>().TakeDamage(gundata.damage);
        }
    }
public void Shoot()
    {
        onGunShoot?.Invoke();
        PoolManager.Instance.GetObject(fireParticlesPrefab,bulletPivot.position);
        Vector3 targetPoint;
        if (TryGetHit(out RaycastHit hit))
        {
            targetPoint = hit.point;
            DamageEnemy(hit.collider.gameObject);
        }
        else
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            targetPoint = ray.GetPoint(rayDistance);
        }
        Vector3 direction = (targetPoint - transform.position).normalized;
        bulletPivot.forward = direction;
        GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab, bulletPivot.position);
        bullet.SetActive(false);
        bullet.transform.position = bulletPivot.position;
        bullet.transform.LookAt(targetPoint);
        bullet.SetActive(true);
        SoundManager.instance.Play(gundata.shootSoundName);
        animator.Play("Shoot", 0, 0f);
    }
    public void HandleFire(bool pressed, bool held)
    {
        if (gundata.gunType == GunType.Automatic)
        {
            if (held)
            {
                TryShoot();
            }
        }
        else if (gundata.gunType == GunType.SemiAutomatic)
        {
            if (pressed)
            {
                TryShoot();
            }
        }
    }
    private void TryShoot()
    {
        if (totalBullets <= 0 && cartridgeBullets <= 0)
        {
            SoundManager.instance.Play(gundata.dropSoundName);
            onGunEmpty?.Invoke();
            return;
        }
        if (cartridgeBullets > 0 && Time.time >= nextFireTime)
        {
            Shoot();
            cartridgeBullets--;
            UpdateAmmoText();
            nextFireTime = Time.time + 1f / gundata.fireRate;
        }
    }
}
 
        