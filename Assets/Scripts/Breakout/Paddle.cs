using UnityEngine;

public class Paddle : MonoBehaviour {

    #region Enums
    public enum e_ControlMethod
    {
        Keyboard = 0,
        Mouse = 1
    }
    #endregion

    #region public properties
    public e_ControlMethod ControlMethod
    {
        get
        {
            return currentControlMethod;
        }
    }
    #endregion


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
    private readonly float movementDampening = .15f;

    /// <summary>
    /// The minimum buffer distance allowed between the padel and 
    /// the map edges.
    /// </summary>
    private readonly float distanceBuffer = .05f;

    /// <summary>
    /// raycasthit2d type to store raycast hit information
    /// </summary>
    private RaycastHit2D hit2D;

    private e_ControlMethod currentControlMethod;
    #endregion
	
    void Awake()
    {
        SetControlMethod(e_ControlMethod.Mouse);
    }

	void Update ()
    {
        Move();
	}

    private void Move()
    {

        #region Keyboard Move
        if(e_ControlMethod.Keyboard == currentControlMethod)
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

        #endregion

        #region Mouse Move
        if(e_ControlMethod.Mouse == currentControlMethod)
        {
            Vector2 mouseDelta;
            mouseDelta.x = Input.GetAxis("Mouse X") * movementDampening;
            mouseDelta.y = 0.0f;
            transform.Translate(mouseDelta);
        }
        #endregion

    }

    public void SetControlMethod(e_ControlMethod cntrlMethod)
    {
        currentControlMethod = cntrlMethod;
    }
}