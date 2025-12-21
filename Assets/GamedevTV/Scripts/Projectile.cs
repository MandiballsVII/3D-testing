using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 playerPosition;
    float speed;
    void Awake()
    {        
        speed = Random.Range(20, 30);
    }
    private void OnEnable()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
