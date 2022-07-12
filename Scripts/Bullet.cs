using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bullet;   // define the bullet rigidbody
    public float MaxTime = 8.5f;
    
    private void Start()
    {
        bullet = GetComponent<Rigidbody>();  // get the rigid body component
    }

    private void OnCollisionEnter(Collision collision)  // Destory the bullet immediately once it collide with anything
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        Destroy (this.gameObject, this.MaxTime);  // Destroy the bullet existing beyound the max time limit to prevent overflow
    }
}
