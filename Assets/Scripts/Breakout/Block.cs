using UnityEngine;

public class Block : MonoBehaviour
{
    #region Private Fields
    private static ScoreKeeping scoreKeep;
    private static ParticleManager partMan;
    #endregion

    void Awake()
    {
        scoreKeep = GameObject.Find("_Keepers").GetComponent<ScoreKeeping>();
        partMan = GameObject.Find("_Keepers").GetComponent<ParticleManager>();
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        scoreKeep.Score++;
        partMan.SpawnSystem(gameObject.transform.position, Vector3.up, ParticleManager.ParticleType.BlockBreak);
        Destroy(gameObject);
    }
}