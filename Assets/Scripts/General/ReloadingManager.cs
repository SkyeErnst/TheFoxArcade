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
    /// The transform attatched to the paddle game object.
    /// </summary>
    private Transform paddleTransform;
    /// <summary>
    /// The transform attatched to the ball game object.
    /// </summary>
    private Transform ballTransform;

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

        paddleTransform = Paddle.transform;
        ballTransform = Ball.transform;
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
        Paddle.transform.position = paddleTransform.position;
        Debug.Log("Curr pos: " + Paddle.transform.position + " wanted pos " + paddleTransform.position);
        Ball.transform.position = ballTransform.position;
        Ball.transform.rotation = ballTransform.rotation;

        Rigidbody2D rb2D = Ball.GetComponent<Rigidbody2D>();
        rb2D.velocity = Vector2.zero;

        MenuSystem menSys = GameObject.Find("_Keepers").GetComponent<MenuSystem>();
        menSys.UnPause();
    }
}