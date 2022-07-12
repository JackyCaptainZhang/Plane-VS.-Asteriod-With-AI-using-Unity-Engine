using UnityEngine;

public class AsteroidAI : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask PlayerLayer;
    public float speedBullet;
    private float chaseSpeed;
    private float Addspeed;
    private float AttackInterval = 0.5f;
   
    //Attacked state flag
    bool alreadyAttacked = false;

    //States parameter
    private float chaseRange, attackRange;
    private bool playerInChaseRange, playerInAttackRange;


    private void Update()
    {
        ModeSelect();             // Enter the Mode select mode
    }

    private void ModeSelect() {                               
        if (FindObjectOfType<GameManager>().lives >= 2 && FindObjectOfType<GameManager>().lives <= 4) gradualMode();  //When 2 <= lives <= 4, gradual mode will be activated to make the game more and more difficult
        if (FindObjectOfType<GameManager>().lives == 1) easyMode();                                                   //When lives = 1, then easy mode will activated to make the game easier
    }

    private void gradualMode() {                            // Gradual mode
        int score = FindObjectOfType<GameManager>().score;

        if (score <= 1200) {                                                                               // Chase 1 and Attack 1 (D1) setting
            int num = FindObjectOfType<GameManager>().DifficultyLevel = 1;
            FindObjectOfType<GameManager>().SetDifficulty(num);
            speedBullet = 2000.0f;
            chaseSpeed = 0.5f;
            chaseRange = 4.0f;
            Addspeed = 1.5f;
            attackRange = 1.0f;
            playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, PlayerLayer);  // Check if the player enter into chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer); // Check if the player enter into attack range
            if (playerInChaseRange && !playerInAttackRange) ChasePlayer();  // Activate coresponding behaviour based on the range flag
            if (playerInChaseRange && playerInAttackRange) AttackPlayer();
        }

        if (score > 1200 && score <= 2000)                                              // Chase 2 and Attack 2 (D2) setting
        {
            int num = FindObjectOfType<GameManager>().DifficultyLevel = 2;
            FindObjectOfType<GameManager>().SetDifficulty(num);  // update and display the difficulty level
            speedBullet = 500.0f;
            chaseSpeed = 2.0f;
            chaseRange = 7.0f;
            Addspeed = 3.0f;
            attackRange = 3.0f;
            playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, PlayerLayer);  // Check if the player enter into chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);  // Check if the player enter into attack range
            if (playerInChaseRange && !playerInAttackRange) ChasePlayer();  // Activate coresponding behaviour based on the range flag
            if (playerInChaseRange && playerInAttackRange) AttackPlayer();
        }

        if (score > 2000)                                                              // Chase 3 and Attack 3 (D3) setting
        {
            int num = FindObjectOfType<GameManager>().DifficultyLevel = 3;
            FindObjectOfType<GameManager>().SetDifficulty(num);  // update and display the difficulty level
            speedBullet = 300.0f;
            chaseSpeed = 4.0f;
            chaseRange = 10.0f;
            Addspeed = 5.0f;
            attackRange = 4.5f;
            playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, PlayerLayer);  // Check if the player enter into chase range
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);  // Check if the player enter into attack range
            if (playerInChaseRange && !playerInAttackRange) ChasePlayer();  // Activate coresponding behaviour based on the range flag
            if (playerInChaseRange && playerInAttackRange) AttackPlayer();
        }      
    }

    private void easyMode()                                    // Easy Mode and its setting
    {
        int num = FindObjectOfType<GameManager>().DifficultyLevel = 1;
        FindObjectOfType<GameManager>().SetDifficulty(num);  // update and display the difficulty level
        speedBullet = 2000.0f;
        chaseSpeed = 0.5f;
        chaseRange = 4.0f;
        Addspeed = 1.5f;
        attackRange = 1.0f; 
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, PlayerLayer);  // Check if the player enter into chase range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);  // Check if the player enter into attack range
        if (playerInChaseRange && !playerInAttackRange) ChasePlayer();  // Activate coresponding behaviour based on the range flag
        if (playerInChaseRange && playerInAttackRange) AttackPlayer();
    }


    private void ChasePlayer() {        
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);  // Change the velocity to the direction of the player
    }

    private void AttackPlayer() {      
        if (!alreadyAttacked)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (chaseSpeed + Addspeed) * Time.deltaTime);
        }
        alreadyAttacked = true;
        Invoke(nameof(resetAttack), AttackInterval);  // The atack will be recalled at the interval
    }


    private void resetAttack() {
        alreadyAttacked = false;
    }

}
