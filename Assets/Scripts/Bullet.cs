using UnityEngine;

public class Bullet : MonoBehaviour
{
[SerializeField]
private float speed = 20f;
protected float damage = 10f;
public float Damage { set { damage = value; } }
void OnEnable()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }
public virtual void OnCollisionEnter(Collision collision)
    
        {
            Destroy(gameObject);
        }
        
}
