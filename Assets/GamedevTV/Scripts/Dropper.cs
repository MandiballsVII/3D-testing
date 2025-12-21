using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float waitTime = 3f;
    MeshRenderer myMeshRenderer;
    Rigidbody myRigidbody;
    void Start()
    {
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myMeshRenderer.enabled = false;
    }

    void Update()
    {
        if(Time.time >= waitTime)
        {
            myRigidbody.useGravity = true;
            myMeshRenderer.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("Ground"))
        {
            myMeshRenderer.enabled = false;
        }
    }
}
