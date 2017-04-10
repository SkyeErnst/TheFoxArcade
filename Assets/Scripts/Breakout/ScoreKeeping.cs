using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour
{
    #region Public Properties
    public uint Score
    {
        get
        {
            return score;
        }
        set
        {
            score++;
            timeSinceLastScore = 0.0f;
            scoreText.text = score.ToString();
        }
    }
    #endregion

    #region Public Fields
    public Text scoreText;
    #endregion

    #region Private Fields
    private  uint score;
    private float timeSinceLastScore;
    #endregion

    public void Update()
    {
        timeSinceLastScore += Time.deltaTime;
    }
}