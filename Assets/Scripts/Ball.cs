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
    private readonly float maxVelocity = 35.0f;

    /// <summary>
    /// The minimum velocity that the ball is allowed to travel
    /// </summary>
    private readonly float minVelocity = 5.0f;

    /// <summary>
    /// How much velocity to add to the rigidbody after a bounce
    /// </summary>
    private readonly float velocityBounceMultiplier = 30.0f;

    /// <summary>
    /// How much to multiply the x value of the 
    /// distance between the ball impact point and the center
    /// of the paddle
    /// </summary>
    private readonly float bounceOffsetMultiplier = 100.0f;

    /// <summary>
    /// The current position of the paddle
    /// </summary>
    private Vector2 paddlePos;

    /// <summary>
    /// Reference to the rigidbody 2d attatched to the same object
    /// this class is attached to
    /// </summary>
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
            UpdateVelocity();
        }
    }

    void OnCollisionExit2D(Collision2D otherObject)
    {
        rB2D.AddForce(rB2D.velocity * velocityBounceMultiplier);
    }

    void FixedUpdate()
    {

        Debug.Log("Velocity: " + rB2D.velocity);

        UpdateVelocity();
    }

    /// <summary>
    /// Updates the velocity of the ball to ensure it stays within acceptable bounds
    /// </summary>
    private void UpdateVelocity()
    {
        if(rB2D.velocity.sqrMagnitude > maxVelocity)
        {
            if (rB2D.velocity.sqrMagnitude > maxVelocity + 40.0f)
            {
                Debug.Log("TOO DAMN FAST");
                rB2D.velocity = Vector2.ClampMagnitude(rB2D.velocity, maxVelocity);
            }
            else
            {
                Debug.Log("Decreasing Speed");
                rB2D.AddForce(-rB2D.velocity * maxVelocity, ForceMode2D.Force);
            }
        }
        if(rB2D.velocity.sqrMagnitude < minVelocity)
        {
            Debug.Log("Increasing Speed");
            rB2D.AddForce(rB2D.velocity * minVelocity, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Calculates the difference between impactPoint and centerPoint while locking the y value when returned
    /// </summary>
    /// <param name="impactPoint"></param>
    /// <param name="centerPoint"></param>
    /// <returns></returns>
    private Vector2 CalcPosDiff(Vector2 impactPoint, Vector2 centerPoint)
    {
        Vector2 totalDist = impactPoint - centerPoint;
        totalDist.x *= bounceOffsetMultiplier;
        totalDist.y = 0.0f;
        return totalDist;
    }
}