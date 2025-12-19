using UnityEngine;

public class Move : MonoBehaviour
{   
    [SerializeField] float moveSpeed = 10f;
    int points = 0;
    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal");
        float yValue = 0f;
        float zValue = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xValue, yValue, zValue) * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        points++;
        Debug.Log("Collision detected with " + collision.gameObject.name);
        Debug.Log("Points: " + points);
        collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
