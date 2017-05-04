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
        }
    }

    #endregion

    #region Private Fields
    private uint foodEaten = 0;
    #endregion
}
