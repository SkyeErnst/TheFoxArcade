using UnityEngine;

public class Ball : MonoBehaviour {


    #region Public Fields
    public GameObject PaddleRef;
    #endregion

    #region Private Fields
    private readonly string paddleName = "Paddle";

    private readonly string wallName = "Wall";
    
    /// <summary>
    /// The maximum velocity that the ball is allowed to travel
    /// </summary>
    private readonly float maxVelocity = 150.0f;

    /// <summary>
    /// The minimum velocity that the ball is allowed to travel
    /// </summary>
    private readonly float minVelocity = .5f;

    /// <summary>
    /// How much velocity to add to the rigidbody after a bounce
    /// </summary>
    private readonly float velocityBounceMultiplier = 8.0f;

    /// <summary>
    /// How much to multiply the x value of the 
    /// distance between the ball impact point and the center
    /// of the paddle
    /// </summary>
    private readonly float bounceOffsetMultiplier = 100.0f;

    /// <summary>
    /// The maximum allowed velocity that a rigidbody is allowed
    /// to be over before the velocity is clamped back to the maximum.
    /// </summary>
    private readonly float maxVelocityOffset = 25.0f;

    /// <summary>
    /// The current position of the paddle
    /// </summary>
    private Vector2 paddlePos;

    /// <summary>
    /// Reference to the rigidbody 2d attatched to the same object
    /// this class is attached to
    /// </summary>
    private Rigidbody2D rB2D;

    /// <summary>
    /// Reference to Particle Manager object
    /// </summary>
    private ParticleManager partMan; 

    #endregion

    void Awake()
    {
        rB2D = GetComponent<Rigidbody2D>();
        partMan = GameObject.Find("_Keepers").GetComponent<ParticleManager>();
    }

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        if(paddleName == otherObject.gameObject.tag)
        {
            paddlePos = otherObject.gameObject.transform.position;
            ContactPoint2D contact = otherObject.contacts[0];
            partMan.SpawnSystem(otherObject.contacts[0].otherCollider.gameObject.transform.position, Vector3.up, ParticleManager.ParticleType.PaddleHit);
            rB2D.AddForce(CalcPosDiff(contact.point, paddlePos));
        }
        if(wallName == otherObject.gameObject.tag)
        {
            partMan.SpawnSystem(otherObject.contacts[0].otherCollider.gameObject.transform.position, Vector3.left, ParticleManager.ParticleType.WallHit);
        }
    }

    void OnCollisionExit2D(Collision2D otherObject)
    {
        if (paddleName == otherObject.collider.gameObject.tag )
        {
            rB2D.AddForce(rB2D.velocity * velocityBounceMultiplier);
        }
    }

    void FixedUpdate()
    {
        UpdateVelocity();
    }

    /// <summary>
    /// Updates the velocity of the ball to ensure it stays within acceptable bounds
    /// </summary>
    private void UpdateVelocity()
    {
        if (rB2D.velocity.sqrMagnitude > maxVelocity)
        {
            float absSquarMagVelocity = Mathf.Abs(rB2D.velocity.sqrMagnitude);

            if (absSquarMagVelocity > (maxVelocity + maxVelocityOffset))
            {
                rB2D.velocity *= .99f;
            }
            else
            {
                rB2D.AddForce(-rB2D.velocity * minVelocity);
            }
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