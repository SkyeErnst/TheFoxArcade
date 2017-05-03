using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomEdge : MonoBehaviour
{
    #region private fields
    /// <summary>
    /// Reference to the menu system object
    /// </summary>
    MenuSystem menSys;

    /// <summary>
    /// Name of the object that stores general objects like the menu system
    /// </summary>
    private const string KEEPER_NAME = "_Keepers";

    #endregion

    private void Awake()
    {
        menSys = GameObject.Find(KEEPER_NAME).GetComponent<MenuSystem>();
    }

    private void OnCollisionEnter2D()
    {
        menSys.OnWinLose(MenuSystem.ActiveGame.BlockBreak, false);
    }
}