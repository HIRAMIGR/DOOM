using System.Collections.Generic;
using UnityEngine;
 
public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
 
    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
 
    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    private Vector3 knockbackVelocity = Vector3.zero;
    [SerializeField]
    private float knockbackDamping = 8f;
 
    public void AddKnockback(Vector3 direction, float force)
    {
        direction.y = 0f;
        direction.Normalize();
        knockbackVelocity += direction * force;
    }
 
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }
 
    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);
 
        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }
 
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 movementVelocity = transform.rotation * new Vector3(input.x, 0, input.y) * targetMovingSpeed;
        Vector3 finalVelocity = movementVelocity + knockbackVelocity;
        finalVelocity.y = rigidbody.linearVelocity.y;
        rigidbody.linearVelocity = finalVelocity;
        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDamping * Time.fixedDeltaTime);
    }
}