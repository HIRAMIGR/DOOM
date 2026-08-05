using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class Player : MonoBehaviour
{
    [SerializeField]
    private GunManager gunManager;
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
     health.InitializeHealth();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            gunManager.GrabGun(other.GetComponent<Gun>());
        }
    }
    public void PushBack(Transform enemy, float force)
    {
        Vector3 pushDirection = (transform.position - enemy.position).normalized;
        firstPersonMovement.AddKnockback(pushDirection, force);
    }
    public void Die()
    {
        gunManager.DropAllGuns();
        GetComponent<FirstPersonMovement>().enabled = false;
        GetComponentInChildren<FirstPersonLook>().enabled  = false;
        rb.isKinematic = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
 