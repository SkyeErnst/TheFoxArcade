using UnityEngine;
using System.Collections;

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

    #region Public Fields

    public Transform FoodPrefab;
    public Transform LeftBoarder;
    public Transform RightBoarder;
    public Transform UpperBoarder;
    public Transform LowerBoarder;

    #endregion

    #region Private Fields
    /// <summary>
    /// The ammount of food eaten so far. Stored here due to inability
    /// to use auto-properties
    /// </summary>
    private uint foodEaten = 0;

    private SnekSegmentController snekSegControl;

    private float timeBetweenFoodSpawn = 5.0f;

    private Vector2 foodSpawnPoint;

    private IEnumerator foodCoru;
    #endregion

    private void Awake()
    {
        snekSegControl = gameObject.GetComponent<SnekSegmentController>();
        foodCoru = SpawnFood();
    }

    // Still needs way to start and stop corotine

    private IEnumerator SpawnFood()
    {
        while(true)
        {
            foodSpawnPoint.x = UnityEngine.Random.Range(LeftBoarder.position.x, RightBoarder.position.x);
            foodSpawnPoint.y = UnityEngine.Random.Range(UpperBoarder.position.y, LowerBoarder.position.y);

            Instantiate(FoodPrefab, foodSpawnPoint, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenFoodSpawn);
        }
    }

    #region Public Methods

    public void StartSpawningFood()
    {
        StartCoroutine(foodCoru);
    }

    public void StopSpawningFood()
    {
        StopCoroutine(foodCoru);
    }

    /// <summary>
    /// Sets the number of food eaten back to zero.
    /// </summary>
    public void ResetFoodCounter()
    {
        foodEaten = 0;
    }

    #endregion
}
