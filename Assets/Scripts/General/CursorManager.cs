using UnityEngine;

public class CursorManager : MonoBehaviour
{

    private void Awake()
    {
        ChangeCursorState(CursorLockMode.Locked);
    }

    public static void ChangeCursorState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }
}