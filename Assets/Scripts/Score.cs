using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.gameScore.ToString();
    }
}
