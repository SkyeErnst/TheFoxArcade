using System.Collections.Generic;
using UnityEngine;


public class ReloadingManager : MonoBehaviour
{
    #region Public Fields
    /// <summary>
    /// The Paddle that the player controls
    /// </summary>
    public GameObject Paddle;
    /// <summary>
    /// The object that hits and destroys blocks
    /// </summary>
    public GameObject Ball;
    /// <summary>
    /// The head of the snek. This is where most snek related classes
    /// are attatched.
    /// </summary>
    public GameObject SnekHead;
    #endregion

    #region Private Fields
    /// <summary>
    /// Array that holds the block gameobjects
    /// </summary>
    private GameObject[] blockgoArray;

    private const string BLOCK_TAG = "Block";
    private const string PADDLE_NAME = "Paddle";
    private const string BALL_NAME = "Ball";
    private const string KEEPER_NAME = "_Keepers";
    #endregion

    /// <summary>
    /// Unity callback method
    /// </summary>
    private void Awake()
    {
        blockgoArray = GameObject.FindGameObjectsWithTag(BLOCK_TAG);
        Paddle = GameObject.FindGameObjectWithTag(PADDLE_NAME);
        Ball = GameObject.Find(BALL_NAME);

    }

    /// <summary>
    /// Loops through return of GameOBject.FindObjecstWithTag for tag Block and unhides them.
    /// Also re assigns original transform to paddle and ball. Resets velocity of ball to zero.
    /// </summary>
    private void RestartBlockBreak()
    {
        foreach (GameObject go in blockgoArray)
        {
            go.SetActive(true);
        }
        Rigidbody2D rb2D = Ball.GetComponent<Rigidbody2D>();
        rb2D.velocity = Vector2.zero;
        ScoreKeeping scoreKeep = GameObject.Find("_Keepers").GetComponent<ScoreKeeping>();
        scoreKeep.ResetScore();

        Paddle.transform.position = new Vector2(0.0f, -3.5f);
        Ball.transform.position = new Vector2(0, 1.75f);

        MenuSystem menSys = GameObject.Find("_Keepers").GetComponent<MenuSystem>();
        menSys.MakeActiveCanvas(MenuSystem.Canvases.BreakoutGame);
        menSys.UnPause();
    }

    private void RestartSnek()
    {
        SnekSegmentController snekSegCtrl = GameObject.Find("SnekHead").GetComponent<SnekSegmentController>();
        GameObject[] foods = new GameObject[GameObject.FindGameObjectsWithTag("Food").Length];
        GameObject[] segments = new GameObject[GameObject.FindGameObjectsWithTag("Segment").Length];

        SnekHead.transform.position = Vector2.zero;
        SnekHead.GetComponent<FoodManager>().ResetFoodCounter();

        
        foods = GameObject.FindGameObjectsWithTag("Food");

        
        segments = GameObject.FindGameObjectsWithTag("Segment");

        foreach (var go in foods)
        {
            Destroy(go);
        }

        foreach (var go in segments)
        {
            Destroy(go);
        }


        if(0 != SnekSegmentController.GetSegmentList().Count)
        {
            List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());

            foreach (var seg in segLis)
            {
                Destroy(seg);
            }
        }
        snekSegCtrl.ClearSegmentList();
    }

    /// <summary>
    /// Resets the given game. Reseting NoGame will do nothing.
    /// </summary>
    /// <param name="gameToReset"></param>
    public void ResetGame(MenuSystem.Games gameToReset)
    {
        switch (gameToReset)
        {
            case MenuSystem.Games.NoGame:
                break;
            case MenuSystem.Games.BlockBreak:
                RestartBlockBreak();
                break;
            case MenuSystem.Games.Snek:
                RestartSnek();
                break;
            case MenuSystem.Games.MainMenu:
                break;
            default:
                break;
        }
    }

    public void ResetBlockBreak()
    {
        ResetGame(MenuSystem.Games.BlockBreak);
    }

    public void ResetSnek()
    {
        ResetGame(MenuSystem.Games.Snek);
    }

}