using UnityEngine;

/// <summary>
/// Attach to game objects to have them persist through loading of scenes
/// </summary>
public class PersistantObject : MonoBehaviour
{
    private static PersistantObject persObject;

    private void Awake()
    {
        //DontDestroyOnLoad(this);

        if(null == persObject)
        {
            persObject = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}