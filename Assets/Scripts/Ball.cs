using UnityEngine;

public class Ball : MonoBehaviour {


    #region Public Fields
    public GameObject PaddleRef;
    #endregion

    #region Private Fields
    private readonly string paddleName = "Paddle";
    private Paddle padd;
    private Vector2 paddlePos;
    private Rigidbody2D rB2D;
    #endregion

    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collider2D otherObject)
    {
        if(paddleName == otherObject.gameObject.tag)
        {
            padd = otherObject.GetComponent<Paddle>();
            paddlePos = otherObject.gameObject.transform.position;
            
        }
    }

    private float CalcPosDiff(Vector2 firstPos, Vector2 secondPos)
    {
        //Clamp return value to max width of paddle and return the difference of the two vecters.
    }
}
