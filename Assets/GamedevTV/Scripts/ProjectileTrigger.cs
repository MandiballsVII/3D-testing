using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    int numberOfProjectiles;
    [SerializeField] GameObject projectileInitialObject;
    void Start()
    {
        numberOfProjectiles = Random.Range(5, 10);
    }

    void Update()
    {
        
    }
    public Vector3 GetRandomPoint(BoxCollider box)
    {

        Vector3 extents = box.size * 0.5f;

        Vector3 localPoint = new Vector3(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y),
            Random.Range(-extents.z, extents.z)
        );

        return box.transform.TransformPoint(localPoint);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < numberOfProjectiles; i++)
                Instantiate(projectile, GetRandomPoint(projectileInitialObject.GetComponent<BoxCollider>()), Quaternion.identity);
        }
    }
}

