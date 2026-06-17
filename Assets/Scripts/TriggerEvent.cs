using UnityEngine;
using UnityEngine.Events;
public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onTriggered?.Invoke();
            Destroy(gameObject);
        }
    }
}
