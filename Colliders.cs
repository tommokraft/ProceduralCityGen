
using UnityEngine;

public class Colliders : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building")) 
        {
            Debug.Log("collison");
            Destroy(collision.gameObject);
        }
    }
}
