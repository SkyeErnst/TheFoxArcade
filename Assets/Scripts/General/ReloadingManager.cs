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
    #endregion

    #region Private Fields
    /// <summary>
    /// Array that holds the block gameobjects
    /// </summary>
    private GameObject[] goArray;

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
        goArray = GameObject.FindGameObjectsWithTag(BLOCK_TAG);
        Paddle = GameObject.FindGameObjectWithTag(PADDLE_NAME);
        Ball = GameObject.Find(BALL_NAME);

    }

    /// <summary>
    /// Loops through return of GameOBject.FindObjecstWithTag for tag Block and unhides them.
    /// Also re assigns original transform to paddle and ball. Will also reset velocity of ball to zero.
    /// </summary>
    public void ResetGame()
    {
        foreach (GameObject go in goArray)
        {
            go.SetActive(true);
        }
        Rigidbody2D rb2D = Ball.GetComponent<Rigidbody2D>();
        rb2D.velocity = Vector2.zero;

        Paddle.transform.position = new Vector2(0.0f, -3.5f);
        Ball.transform.position = new Vector2(0, 1.75f);

        MenuSystem menSys = GameObject.Find("_Keepers").GetComponent<MenuSystem>();
        menSys.UnPause();
    }
}