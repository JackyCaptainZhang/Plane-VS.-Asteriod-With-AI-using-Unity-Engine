using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Players player;
    public int lives = 4;
    public int reBornTime = 3;
    public ParticleSystem Explosion;
    public int score = 0;
    public int DifficultyLevel = 1;
    public int AsteriodNum = 5;
    public Text scoreText;
    public Text livesText;
    public Text AsteriodText;
    public Text DifficultyText;
    public GameObject gameOverUI;
    public GameObject CongratesUI;
    public GameObject StartUI;



    private void Start()
    {
        StartUI.SetActive(true);
        gameOverUI.SetActive(false);
        CongratesUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartUI.SetActive(false);
            StartGame();  // Start the new game.
        }
    }

    private void StartGame() {       // This method is for starting the new game at the beginning when restarting
        gameOverUI.SetActive(false);  // Set the GameOverUI to false
        CongratesUI.SetActive(false);
        SetLives(4);                  // Initiate the lives to 3
        SetScore(0);                 // Initiate the score to 0
        SetDifficulty(1);
        AsteriodNumber(5);
        reBorn();                   // Reborn the player
    }

    public void AsteriodDestroyed(Asteriod asteriod) {                    // This method is for the collsion effect and update the score
        this.Explosion.transform.position = asteriod.transform.position;  // Play the explosion effect at the position of explosion
        this.Explosion.Play();
        this.score += 100;
        SetScore(score);
    }
    private void SetScore(int score)  // The method for setting the score to the screen
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)  // The method for setting the lives to the screen
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

    public void SetDifficulty(int DifficultyLevel) {   // The method for setting the difficulty level to the screen
        this.DifficultyLevel = DifficultyLevel;
        DifficultyText.text = DifficultyLevel.ToString();
    }

    public void AsteriodNumber(int AsteriodNum)  // The method for setting the asteriod number to the screen
    {
        this.AsteriodNum = AsteriodNum;
        AsteriodText.text = AsteriodNum.ToString();
    }

    public void PlayerDied() {  // This method will be called when the player is collide with asteriods
        this.Explosion.transform.position = this.player.transform.position;  // Play the explosion effect at the position of explosion
        this.Explosion.Play();
        this.lives --;  // Minus one live
        SetLives(lives); // Set the new life to screen
        if (this.lives == 0){  // Check whether the live is equal to zero
            GameOver();       // If live zero, than call the GAmeOver() method.
        }
        else { 
        Invoke(nameof(reBorn), this.reBornTime);  // If live bigger than zero, than call the Reborn() method.The reborn will occure after reBornTime seconds.
        }
    }

    private void reBorn() {     // If live bigger than zero, than make the player reborn
        this.player.transform.position = Vector3.zero;  // Reborn position to the (0,0,0)
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");  // thrn off the collision detection, reasons are as mentioned as below next comment
        this.player.gameObject.SetActive(true);
        Invoke(nameof(turnonCollisions), 3.0f);           // After reborned, there are 3 seconds for player to react and escape in avoidance of immediate collision with asteroids after reborn
    }

    private void turnonCollisions() {         // thrn on the collision back again
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()  // GameOver will call the gameoverUI and stop the main camera. Play the gameover camera instead.
    {
        gameOverUI.SetActive(true);
    }
    public void Congrates()  // GameOver will call the gameoverUI and stop the main camera. Play the gameover camera instead.
    {
        CongratesUI.SetActive(true);
    }
}
