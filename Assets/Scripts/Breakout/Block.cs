using UnityEngine;

public class Block : MonoBehaviour
{
    #region Private Fields
    private static ScoreKeeping scoreKeep;
    #endregion

    void Awake()
    {
        scoreKeep = GameObject.Find("Keepers").GetComponent<ScoreKeeping>();
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        scoreKeep.Score++;
        Destroy(gameObject);
    }
}