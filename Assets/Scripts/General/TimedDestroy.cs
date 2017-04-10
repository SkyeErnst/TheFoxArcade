using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    /// <summary>
    /// Destroys the attatched object after the input time in seconds
    /// </summary>
    /// <param name="time">How long in seconds to wait before destroying the object</param>
    public void DestroyAfterTime(float time)
    {
        Destroy(gameObject, time);
    }
}
