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
            scoreText.text = score.ToString();
        }
    }
    #endregion

    #region Public Fields
    public Text scoreText;
    #endregion

    #region Private Fields
    private  uint score;
    #endregion
}