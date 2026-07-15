using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform gunPosition;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private UnityEvent onGunGrabbed;
    [SerializeField]
    private UnityEvent onGunDropped;
    private Health health;
    private Rigidbody rb;
    public float CurrentHealth => health.CurrentHealth;
    private FirstPersonMovement firstPersonMovement;
    private Gun currentGun;
    private void Awake()
    {
     firstPersonMovement = GetComponent<FirstPersonMovement>();    
     rb = GetComponent<Rigidbody>();
     health = GetComponent<Health>();
    }
    private void Start()
    {
     onGunDropped?.Invoke();  
     health.InitializeHealth();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun") && currentGun == null)
        {
            currentGun = other.GetComponent<Gun>();
            currentGun.GrabGun(gunPosition, ammoText);
            onGunGrabbed?.Invoke();
            currentGun.OnGunEmpty.AddListener(DropGun);
        }
    }
    private void Update()
    {
        if (currentGun != null)
        {
            currentGun.HandleFire(inputManager.LeftButtonPressed, inputManager.LeftButtonHeld);
            if (inputManager.RightButtonPressed)
            {
                currentGun.ChargeGun();
            }
                }      
    }
    public void DropGun()
    {
        if (currentGun == null) return;
        Destroy(currentGun.gameObject);
        currentGun = null;
        onGunDropped?.Invoke();
    }
    public void PushBack(Transform enemy, float force)
    {
        Vector3 pushDirection = (transform.position - enemy.position).normalized;
        firstPersonMovement.AddKnockback(pushDirection, force);
    }
    public void Die()
    {
        DropGun();
        GetComponent<FirstPersonMovement>().enabled = false;
        GetComponentInChildren<FirstPersonLook>().enabled  = false;
        rb.isKinematic = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
 
 