using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Testing if this comment makes it through the git push :) *04/10/2021*

    // Game objects.
    GameObject instantiatedSquare;
    public GameObject cloneSquare;
    public GameObject completeLevelUI;

    // Player stats.
    public Text Player_Clicks;
    public Text Squares_Clicked;
    public Text Accuracy;
    public Text timerText;

    // Game variables.
    public static bool roundIsOver = false;
    public static bool squareWasClicked = false;
    public static int gameScore = 0;
    private int playerClicks = 0;
    public float roundTimer = 10;
    private int seconds;
    private int highscore;

    // Start is called before the first frame update
    void Start()
    {
        // Set game variables to default every time scene is loaded.
        roundIsOver = false;
        gameScore = 0;
        squareWasClicked = false;
        playerClicks = 0;

        //completeLevelUI.SetActive(false);

        // Always spawn the first square in the middle.
        Vector2 StartPosition = new Vector2(0, 0);
        instantiatedSquare = (GameObject)Instantiate(cloneSquare, StartPosition, Quaternion.identity);

        // Get player high score.
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        Debug.Log("Highscore: " + highscore);
    }

    // Update is called once per frame
    void Update()
    {
        // If timer has not reached 0.
        if (!roundIsOver)
        {
            // If player successfully hit a square
            if (squareWasClicked)
            {
                squareWasClicked = false;
                Destroy(instantiatedSquare);
                SpawnSquare();
            }

            // Record total amount of player clicks
            if (Input.GetMouseButtonDown(0))
                playerClicks++;
        }
        else
        {
            // Set UI stats for player to view.
            Player_Clicks.text = playerClicks.ToString();
            Squares_Clicked.text = gameScore.ToString();

            // Need to do all this conversion for accuracy because the original
            // values are integers.
            float gs = (float)gameScore;
            float pc = (float)playerClicks;
            float accuracyf = (gs / pc) * 100;
            int accuracy = Mathf.RoundToInt(accuracyf);
            Accuracy.text = accuracy.ToString() + "%";

            // Reset the game timer.
            //roundTimer = 4;

            // Get rid of unnecessary objects.
            Destroy(instantiatedSquare);

            // Save player score, clicks, and accuracy.
            if (gameScore > highscore)
            {
                PlayerPrefs.SetInt("highscore", highscore);
                Debug.Log("Highscore: " + highscore);
            }
          
            // Bring up the "Round complete" screen.
            completeLevelUI.SetActive(true);
        }

        // Round time controller
        if (roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
            seconds = Mathf.RoundToInt(roundTimer);
        }
        else
        {
            roundIsOver = true;
        }
        timerText.text = seconds.ToString();
    }

    public void SpawnSquare()
    {
        // Instantiate square in a new position between a random range.
        Vector2 RanPosition = new Vector2(Random.Range(-7.4f, 7.4f), Random.Range(-2.8f, 2.8f));
        instantiatedSquare = (GameObject)Instantiate(cloneSquare, RanPosition, Quaternion.identity);

        // Randomly change the square size.
        float ranNum = Random.Range(0.3f, 1);
        instantiatedSquare.transform.localScale = Vector3.one * ranNum;

        // Change square colour randomly.
        instantiatedSquare.GetComponent<SpriteRenderer>().color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1
            );

        // Give the instantiated square a name for the scene hierarchy.
        instantiatedSquare.transform.name = "Square-" + (gameScore + 1).ToString();
    }
}
