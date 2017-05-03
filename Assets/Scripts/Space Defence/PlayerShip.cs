using UnityEngine;

public class PlayerShip : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (MenuSystem.PauseState.Unpaused == MenuSystem.GlobalPauseState)
        {
            Move();
        }
    }

    private void Move()
    {
        if(Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector2.left * 10);
        }
        if(Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * 10);
        }
    }
}