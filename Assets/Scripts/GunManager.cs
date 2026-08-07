using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class GunManager : MonoBehaviour
{
[SerializeField]
private UnityEvent onGunGrabbed;
[SerializeField]
private UnityEvent onGunDropped;
[SerializeField]
private Transform gunPosition;
[SerializeField]
private Text ammoText;
[SerializeField]
private Image gunIcon;
[SerializeField]
private Scope scope;
[SerializeField]
private InputManager inputManager;
private Gun currentGun;
private List<Gun> guns = new List<Gun>();
private int currentGunIndex = 0;
private void Awake()
    {
        onGunDropped?.Invoke();
    }
public void GrabGun(Gun gun)
    {
        Gun existingGun = guns.Find(listGun => listGun.Gundata == gun.Gundata);
        if (existingGun != null)
        {
            if(existingGun.IsGunFull)
            {
                return;
            }
            existingGun.ChargeTotalBullets();
            Destroy(gun.gameObject);
            return;
        }
        guns.Add(gun);
        currentGun?.gameObject.SetActive(false);
        currentGun = gun;
        currentGun.GrabGun(gunPosition, ammoText);
        currentGun.OnGunEmpty.AddListener(DropGun);
        currentGun.OnGunShoot.AddListener(scope.PlayFireAnimation);
        onGunGrabbed?.Invoke();
        currentGunIndex = guns.IndexOf(currentGun);
        gunIcon.sprite = currentGun.Gundata.sprite;
        gunIcon.SetNativeSize();
    }
    public void SwitchUpGun()
    {
        currentGunIndex++;
        if (currentGunIndex >= guns.Count)
        {
            currentGunIndex = 0;
        }
        SwitchGun();
    }
    public void SwitchDownGun()
    {
        currentGunIndex--;
        if (currentGunIndex < 0)
        {
            currentGunIndex = guns.Count - 1;
        }
        SwitchGun();
    }
    private void SwitchGun()
    {
        if (guns.Count <= 1) return;
        currentGun.gameObject.SetActive(false);
        SetGun();
    }
    public void SetGun()
    {
    currentGun = guns[currentGunIndex];
    currentGun.gameObject.SetActive(true);
    currentGun.GrabGun(gunPosition, ammoText,false);
    SetIcon(currentGun.Gundata.sprite);
    }
    public void SetIcon(Sprite sprite)
    {
        gunIcon.sprite = sprite;
        gunIcon.SetNativeSize();
    }
    public void DropAllGuns()
    {
        foreach (Gun gun in guns)
        {
            Destroy(gun.gameObject);
        }
        guns.Clear();
        currentGun = null;
        onGunDropped?.Invoke();
    }
    public void DropGun()
    {
        currentGun.OnGunEmpty.RemoveListener(DropGun);
        currentGun.OnGunShoot.RemoveListener(scope.PlayFireAnimation);
        guns.Remove(currentGun);
        Destroy(currentGun.gameObject);
        if(guns.Count > 0)
        {
            currentGunIndex = guns.Count - 1;
            SetGun();
        }
        else
        {
            onGunDropped?.Invoke();
            currentGun = null;
        }
    }
   private void Update()
    {
        if (currentGun == null) return;
        currentGun.HandleFire(inputManager.LeftButtonPressed,
            inputManager.LeftButtonHeld);
        if (inputManager.RightButtonPressed)
        {
            currentGun.ChargeGun();
        }
        if (currentGun.IsAimingEnemy())
        {
            scope.ChangeToAimingColor();
        }
        else
        {
            scope.ChangeToIdleColor();
        }
    }
}
