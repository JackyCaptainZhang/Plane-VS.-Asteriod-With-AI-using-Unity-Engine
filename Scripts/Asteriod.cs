using UnityEngine;

public class Asteriod : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 offset;
    private Vector3 lastDir;
    public float size = 1.0f;
    public float minSize = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        float xoffset = Random.Range(0.0f, 5.0f);  // Generate the random path for each asteriods (Y axis unchanged all the time)
        float zoffset = Random.Range(0.0f, 5.0f);
        offset = new Vector3(xoffset, 0, zoffset);
        rb.velocity = offset * 0.25f;        // give the velocity to the asteriod
    }
    
    private void LateUpdate()
    {
        lastDir = rb.velocity;  // record the velocity before the collision
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Walls" || other.gameObject.tag == "Asteriods")  // for the collision reflection with walls and another asteriod (set the Y axis unchanged all the time)
        {
            Vector3 reflexAngle = Vector3.Reflect(lastDir, other.contacts[0].normal);  // get the reflection angle
            reflexAngle.y = 0;                                                         // we want to make the asteriod fly at a constant height all the time, so set y reflection axis to 0 all the time
            rb.velocity = reflexAngle.normalized * lastDir.magnitude;          // give the velocity to the asteriod
        }


        if (other.gameObject.tag == "Bullets")  // for the collision with bullets

            
                    if (this.size > this.minSize)   // If the size is larger than 1.0f , than devide the asteriod into half
            {     
                        Splite();                      // Two new asteriod will be generated
                        Splite();
                        FindObjectOfType<GameManager>().AsteriodDestroyed(this);   // Call the AsteriodDestroyed() method in GameManager to play the explosion effect and the will set the score
                        Destroy(this.gameObject);   // the original asteroid will be destoryed
                        int num1 = FindObjectOfType<GameManager>().AsteriodNum += 1;  // Calculate the number of the left asteriod.We got two new and one destroyed, so plus one in all.
                        FindObjectOfType<GameManager>().AsteriodNumber(num1);  // update the number to the screen
                    }
                    else
                    {                         // If the size is smaller than 1.0f, destroy the asteriod directly
                        FindObjectOfType<GameManager>().AsteriodDestroyed(this);  // Call the AsteriodDestroyed() method in GameManager to play the explosion effect and the will set the score
                        Destroy(this.gameObject);  // the original asteroid will be destoryed
                        int num2 = FindObjectOfType<GameManager>().AsteriodNum -= 1;  // Calculate the number of the left asteriod.Here we got one destroyed, so minus one in all.
                        FindObjectOfType<GameManager>().AsteriodNumber(num2);  // update the number to the screen
            } 
    }

    public void Splite() {   // The Splite() method will generate the two smaller asteroids
        Asteriod half;
        Vector3 position = this.transform.position;  // Get the position of the orignal asteriod
        half = Instantiate(this, position, this.transform.rotation);  // Copy two new asteriods at the position of the original asteriod
        half.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);    // Set two new asteriods' size to the half of the original asteriod
        half.size = this.size * 0.5f;                                 // Update he new size parameter to the system for the next collision detection 
    }
}