using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToInstantiate;
    public void InstantiatePrefab(Transform parentTransform)
    {
        Instantiate(objectToInstantiate, parentTransform.position, Quaternion.identity);
    }
public void InstantiatePrefab(Vector3 position)
    {
        Instantiate(objectToInstantiate, position, Quaternion.identity);
    }
}
