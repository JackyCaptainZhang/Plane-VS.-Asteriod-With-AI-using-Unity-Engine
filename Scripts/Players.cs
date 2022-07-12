using UnityEngine;

public class Players : MonoBehaviour
{
    private float RotateInput;
    private bool FowardInput;
    private bool BackwardInput;
    private float VerticalInput;
    private Rigidbody RigidbodyComponent;
    public Bullet bulletPrefab;
    public float SpeedPlayer = 2.0f;


    // Start is called before the first frame update
    private void Start()
    {
        RigidbodyComponent = GetComponent<Rigidbody>();  // call the GetComponent<Rigidbody>() at the start of the game for only once to improve the performance
        
    }

    // Update is called once per frame
    private void Update()
    {
        FowardInput = Input.GetKey(KeyCode.W);                    //check whether the "W" is pressed
        BackwardInput = Input.GetKey(KeyCode.S);                  //check whether the "S" is pressed
        VerticalInput = Input.GetAxis("Vertical") * SpeedPlayer; //check whether the "v" or "c" is pressed
        if (Input.GetKey(KeyCode.A))                            // Define the A to rotate clockwise
        {
            RotateInput = 1.0f;
        }else if (Input.GetKey(KeyCode.D))                      // Define the D to rotate underclockwise
        {
            RotateInput = -1.0f;
        }else
        {
            RotateInput = 0.0f;
        }       
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))  //check the shoot input and shoot
        {
            Shoot();
        }

    }
    private void Shoot()   // shoot methods 
    {
            float speedBullet = FindObjectOfType<AsteroidAI>().speedBullet;           //Get the speed bullet according to the states in AsteroidAI class
            Bullet bullet = Instantiate(this.bulletPrefab, RigidbodyComponent.transform.position, RigidbodyComponent.transform.rotation);  // generate the bullet at the head of plane before firing
            Vector3 direction = RigidbodyComponent.transform.forward;  // define the head direction of the plane
            bullet.GetComponent<Rigidbody>().AddForce(direction * speedBullet);  // shoot the bullet along the head direction of the plane
        
    }

    // update is called onced any physic update occures
    private void FixedUpdate()
    {
        if (FowardInput)
        {
            this.transform.Translate(Vector3.forward * SpeedPlayer);   // move forwards
        }
        if (BackwardInput)                                            //move backwards
        {
            this.transform.Translate(Vector3.back * SpeedPlayer);        
        }
        if (RotateInput != 0.0f)                                     // rotate
        {
            this.transform.Rotate(Vector3.up * -RotateInput);
        }
        RigidbodyComponent.velocity = new Vector3(RigidbodyComponent.velocity.x, VerticalInput * 50, RigidbodyComponent.velocity.z); // move up and down
        if (FindObjectOfType<GameManager>().AsteriodNum == 0)
        {
            FindObjectOfType<GameManager>().Congrates();
        }
    }

    private void OnCollisionEnter(Collision other) {                 // Detect whether the plane is collided with asteriod
        if (other.gameObject.tag == "Asteriods") {
            RigidbodyComponent.velocity = Vector3.zero;
            RigidbodyComponent.angularVelocity = Vector3.zero;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();          // If collision occurred, the PlayerDied() method in the GameManager will be called
        }


    }


}
