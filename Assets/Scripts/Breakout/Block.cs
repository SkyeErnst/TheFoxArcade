using UnityEngine;

public class Block : BlockManager
{

    #region Properties
    public BlockTypes CurrentBlockType
    {
        get
        {
            return currType;
        }
        set
        {
            currType = value;
        }
    }
    #endregion 

    #region Private Fields
    /// <summary>
    /// Reference to score keeping class
    /// </summary>
    private static ScoreKeeping scoreKeep;
    /// <summary>
    /// Reference to particle manager class
    /// </summary>
    private static ParticleManager partMan;
    /// <summary>
    /// Reference to Block Manager class
    /// </summary>
    private static BlockManager blockMan;
    /// <summary>
    /// The type of block that this block is.
    /// </summary>
    private BlockTypes currType;
    /// <summary>
    /// The current health of the block. 0 means the block should be destroyed
    /// </summary>
    private int health = 2;
    #endregion



    new void Awake()
    {
        scoreKeep = GameObject.Find("_Keepers").GetComponent<ScoreKeeping>();
        partMan = GameObject.Find("_Keepers").GetComponent<ParticleManager>();
        blockMan = GameObject.Find("_Keepers").GetComponent<BlockManager>();
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        switch(CurrentBlockType)
        {
            case BlockTypes.Normal:
                health -= 2;
                scoreKeep.BlockScoreAddition = 1;
                break;
            case BlockTypes.TakesTwoHits:
                health -= 1;
                scoreKeep.BlockScoreAddition = 1;
                ChangeBlockCollor(Color.yellow);
                break;
            case BlockTypes.DoublePoints:
                health -= 2;
                scoreKeep.BlockScoreAddition = 2;
                break;
            case BlockTypes.TriplePoints:
                health -= 2;
                scoreKeep.BlockScoreAddition = 3;
                break;
            case BlockTypes.Indestructable:
                break;
        }

        if(0 >= health)
        {
            gameObject.SetActive(false);
            scoreKeep.UpdateScore();
            partMan.SpawnSystem(gameObject.transform.position, Vector3.up, ParticleManager.ParticleType.BlockBreak);
            blockMan.BlocksDestroyed++;
        }
    }

    public void ChangeBlockCollor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }
}