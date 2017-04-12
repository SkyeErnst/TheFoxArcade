using UnityEngine;

public class Block : BlockManager
{
    #region Private Fields
    private static ScoreKeeping scoreKeep;
    private static ParticleManager partMan;
    #endregion

    new void Awake()
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

    public void ChangeBlockCollor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }
}