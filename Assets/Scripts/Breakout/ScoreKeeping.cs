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
    }

    public uint BlockScoreAddition
    {
        get
        {
            return blockScoreAddition;
        }
        set
        {
            blockScoreAddition = value;
        }
    }
    #endregion

    #region Public Fields
    public Text scoreText;
    public Text multiplierText;
    #endregion

    #region Private Fields
    /// <summary>
    /// private variable for the use of the uint Score property
    /// </summary>
    private  uint score = 0;
    /// <summary>
    /// Used to prevent directly multiplying the score by the multiplier
    /// </summary>
    private uint scorePerHit = 1;
    /// <summary>
    /// proxy variable for get of property of same name
    /// </summary>
    private uint blockScoreAddition = 0;
    /// <summary>
    /// The default value for scorePerHit
    /// </summary>
    private uint scorePerHitDefaultValue = 1;

    /// <summary>
    /// The current multiplier to be added to score
    /// </summary>
    private float scoreMultiplier = 1.0f;
    /// <summary>
    /// The minimum multiplier to be applied to the score
    /// </summary>
    private float minScoreMultiplier = 1.0f;

    /// <summary>
    /// The number of digits of precision to display for the 
    /// score multiplier
    /// </summary>
    private readonly string precision = "N2";

    #endregion

    public void Update()
    {
        
        if(scoreMultiplier > minScoreMultiplier)
        {
            scoreMultiplier -= Time.deltaTime * 1.5f;
        }
        else if(minScoreMultiplier > scoreMultiplier)
        {
            scoreMultiplier = minScoreMultiplier;
        }
        multiplierText.text = scoreMultiplier.ToString(precision);
    }

    public void UpdateScore()
    {
        scorePerHit *= (uint)scoreMultiplier + BlockScoreAddition;
        score += scorePerHit;
        scoreMultiplier += 1.5f;

        scoreText.text = score.ToString();

        scorePerHit = scorePerHitDefaultValue;
        
    }

    public void ResetScore()
    {
        score = 0;
    }
}