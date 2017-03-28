using UnityEngine;

public class Paddle : MonoBehaviour {

    #region Public Fields
    /// <summary>
    /// The left most part of the paddel model
    /// </summary>
    public GameObject LeftBuffer;

    /// <summary>
    /// The right most part of the paddel model
    /// </summary>
    public GameObject RightBuffer;
    #endregion

    #region Private Fields
    /// <summary>
    /// How much to multiply movement by to slow down movement
    /// </summary>
    private readonly float movementDampening = .25f;

    /// <summary>
    /// The minimum buffer distance allowed between the padel and 
    /// the map edges.
    /// </summary>
    private readonly float distanceBuffer = .05f;

    /// <summary>
    /// raycasthit2d type to store raycast hit information
    /// </summary>
    private RaycastHit2D hit2D;
    #endregion
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        Vector2 direction;
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) //As long as there is no object in the way, move left
        {
            direction = Vector2.left;
            Debug.DrawRay(LeftBuffer.transform.position, Vector2.left, Color.red, 1.25f);
            if (false == Physics2D.Raycast(LeftBuffer.transform.position, direction, distanceBuffer))
            {
                transform.Translate(Vector2.left * movementDampening);
            }
        }
        if (Input.GetKey(KeyCode.D)) //As long as there is no object in the way, move right
        {
            direction = Vector2.right;
            Debug.DrawRay(RightBuffer.transform.position, Vector2.right, Color.red, 1.25f);
            if (false == Physics2D.Raycast(RightBuffer.transform.position, Vector2.right, distanceBuffer))
            {
                transform.Translate(Vector2.right * movementDampening);
            }
        }
    }
}