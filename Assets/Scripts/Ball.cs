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

    void OnCollisionEnter2D(Collision2D otherObject)
    {
        Debug.Log("OncollisionEnter2D");
        if(paddleName == otherObject.gameObject.tag)
        {
            Debug.Log("Enter IF");
            padd = otherObject.collider.gameObject.GetComponent<Paddle>();
            paddlePos = otherObject.gameObject.transform.position;
            ContactPoint2D contact = otherObject.contacts[0];
            rB2D.AddForce(CalcPosDiff(contact.point, paddlePos));
        }
    }

    private Vector2 CalcPosDiff(Vector2 impactPoint, Vector2 centerPoint)
    {
        //Clamp return value to max width of paddle and return the difference of the two vecters.
        Vector2 totalDist = impactPoint - centerPoint;
        totalDist.x *= 100;
        Debug.Log("Dist between ball impact (" + impactPoint + ") and paddlePos (" + centerPoint);
        totalDist.y = 0.0f;
        //totalDist.x = Mathf.Clamp(totalDist.x, 0.0f, 0.5f);

        return totalDist;
    }
}