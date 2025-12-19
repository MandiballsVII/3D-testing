using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float waitTime = 3f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.time >= waitTime)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
