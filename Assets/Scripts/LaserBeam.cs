using UnityEngine;
 
public class LaserBeam : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;
    private Transform target;
    public Transform Target { set { target = value; } }
    [SerializeField]
    private Transform laserMesh;
    [SerializeField]
    private float radius = 0.1f;
    [SerializeField]
    private Vector3 targetOffset=new Vector3(0f, 0.5f, 0f);
    private bool followTarget = true;
    private bool isActive = false;
    private void Awake()
    {
        ActivateLaser(false);
    }
    private void LateUpdate()
    {
        if (!isActive || !followTarget) return;
        UpdateLaser();
    }
    public void ActivateLaser(bool active)
    {
        isActive = active;
        laserMesh.gameObject.SetActive(active);
        if (active)
        {
            UpdateLaser();
        }
    }
    public void UpdateLaser()
    {
        if (!startPoint || !target) return;
        Vector3 direction = target.position + targetOffset - startPoint.position;
        float distance = direction.magnitude;
        if (distance <=     Mathf.Epsilon)
        {
            laserMesh.localScale = Vector3.zero;
            return;
        }
        laserMesh.position = startPoint.position+ direction * 0.5f;
        laserMesh.rotation = Quaternion.FromToRotation(Vector3.up, direction.normalized);
        laserMesh.localScale = new Vector3(radius * 2f, distance * 0.5f, radius * 2f);
    }
}
 
 