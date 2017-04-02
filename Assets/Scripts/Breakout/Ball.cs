﻿using UnityEngine;

public class Ball : MonoBehaviour {


    #region Public Fields
    public GameObject PaddleRef;
    #endregion

    #region Private Fields
    private readonly string paddleName = "Paddle";
    
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
    private readonly float velocityBounceMultiplier = 10.0f;

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

    void OnCollisionExit2D(Collision2D otherObject)
    {
        //&& maxVelocity > (rB2D.velocity * velocityBounceMultiplier).sqrMagnitude
        if (paddleName == otherObject.collider.gameObject.tag )
        {
            Debug.LogWarning("Passed if");
            rB2D.AddForce(rB2D.velocity * velocityBounceMultiplier);
        }
    }

    void FixedUpdate()
    {
        if (rB2D.velocity.sqrMagnitude > maxVelocity)
        {
            float absSquarMagVelocity = Mathf.Abs(rB2D.velocity.sqrMagnitude);

            if (absSquarMagVelocity > (maxVelocity + maxVelocityOffset))
            {
                Debug.Log("Velocity: " + rB2D.velocity);
                Debug.LogWarning("TOO DAMN FAST");
                //rB2D.velocity = Vector2.ClampMagnitude(rB2D.velocity, maxVelocity);

                rB2D.velocity *= .99f;


                //Vector2 clampedVeloc = Vector2.ClampMagnitude(rB2D.velocity, maxVelocity);
                //rB2D.velocity = Vector2.zero;

                //rB2D.velocity = clampedVeloc;
                
            }
            else
            {
            Debug.Log("Velocity: " + rB2D.velocity);
            Debug.Log("Decreasing Speed");
            rB2D.AddForce(-rB2D.velocity * minVelocity);
            }
        }
        //if (rB2D.velocity.sqrMagnitude < minVelocity)
        //{
        //    Debug.Log("Velocity: " + rB2D.velocity);
        //    Debug.Log("Increasing Speed");
        //    rB2D.AddForce(rB2D.velocity * minVelocity * 50);
        //}

        UpdateVelocity();
    }

    /// <summary>
    /// Updates the velocity of the ball to ensure it stays within acceptable bounds
    /// </summary>
    private void UpdateVelocity()
    {
        //if (rB2D.velocity.sqrMagnitude > maxVelocity)
        //{
        //    if (rB2D.velocity.sqrMagnitude > maxVelocity + 40.0f)
        //    {
        //        Debug.Log("TOO DAMN FAST");
        //        rB2D.velocity = Vector2.ClampMagnitude(rB2D.velocity, maxVelocity);
        //    }
        //    else
        //    {
        //        Debug.Log("Decreasing Speed");
        //        rB2D.AddForce(-rB2D.velocity * maxVelocity);
        //    }
        //}
        //if (rB2D.velocity.sqrMagnitude < minVelocity)
        //{
        //    Debug.Log("Increasing Speed");
        //    rB2D.AddForce(rB2D.velocity * minVelocity * 100);
        //}
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