using UnityEngine;

public class Food : MonoBehaviour
{

    #region Private Fields
    /// <summary>
    /// reference to food manager class
    /// </summary>
    private static FoodManager foodMan;

    /// <summary>
    /// Collider attachted to this food object
    /// </summary>
    private Collider thisCol;

    /// <summary>
    /// Collider attachted to the snek head
    /// </summary>
    private static Collider snekCol;
    #endregion

    // Use this for initialization
    private void Awake ()
    {
        thisCol = gameObject.GetComponent<Collider>();
        foodMan =GameObject.Find("_Keepers").GetComponent<FoodManager>();
        snekCol = GameObject.Find("SnekHead").GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(true == thisCol.bounds.Intersects(snekCol.bounds))
        {
            Destroy(gameObject);
        }
	}
}
