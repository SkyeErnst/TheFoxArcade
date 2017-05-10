using UnityEngine;

public class FoodManager : MonoBehaviour
{
    #region Public Properties
    public uint FoodEaten
    {
        get
        {
            return foodEaten;
        }
        set
        {
            foodEaten = value;
            snekSegControl.AddSegment();
        }
    }

    #endregion

    #region Private Fields
    /// <summary>
    /// The ammount of food eaten so far. Stored here due to inability
    /// to use auto-properties
    /// </summary>
    private uint foodEaten = 0;

    private SnekSegmentController snekSegControl;
    #endregion

    private void Awake()
    {
        snekSegControl = gameObject.GetComponent<SnekSegmentController>();
    }
}
