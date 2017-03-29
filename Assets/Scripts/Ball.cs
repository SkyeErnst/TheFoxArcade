using UnityEngine;

public class Ball : MonoBehaviour {


    #region Public Fields
    public GameObject PaddleRef;
    #endregion

    #region Private Fields
    private readonly string paddleName = "Paddle";
    
    /// <summary>
    /// The maximum velocity that the ball is allowed to travel
    /// </summary>
    private readonly float maxVelocity = 30.0f;

    /// <summary>
    /// The minimum velocity that the ball is allowed to travel
    /// </summary>
    private readonly float minVelocity = 5.0f;
    private Vector2 paddlePos;
    private Rigidbody2D rB2D;
    #endregion

    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        if(paddleName == otherObject.gameObject.tag)
        {
            paddlePos = otherObject.gameObject.transform.position;
            ContactPoint2D contact = otherObject.contacts[0];
            rB2D.AddForce(CalcPosDiff(contact.point, paddlePos));
        }
    }

    void FixedUpadte()
    {
        Debug.Log("FixedUpdate");

        UpdateVelocity();
    }

    private void UpdateVelocity()
    {

    }

    /// <summary>
    /// Calculates the difference between impactPoint and centerPoint while locking the y value when returned
    /// </summary>
    /// <param name="impactPoint"></param>
    /// <param name="centerPoint"></param>
    /// <returns></returns>
    private Vector2 CalcPosDiff(Vector2 impactPoint, Vector2 centerPoint)
    {
        //Clamp return value to max width of paddle and return the difference of the two vecters.
        Vector2 totalDist = impactPoint - centerPoint;
        totalDist.x *= 100;
        Debug.Log("Dist between ball impact (" + impactPoint + ") and paddlePos (" + centerPoint);
        totalDist.y = 0.0f;

        return totalDist;
    }
}