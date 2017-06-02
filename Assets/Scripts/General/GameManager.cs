using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class used to start games
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Public Fields

    public GameObject BlockBreakParent;
    public GameObject SnekParent;

    #endregion

    #region Private Fields

    private List<GameObject> gameParents;

    /// <summary>
    /// True when the inital setup should be run.
    /// Used exclusivly for LateUpdate in this class.
    /// </summary>
    private bool runSetup = true;
    #endregion

    private void Awake()
    {
        gameParents = new List<GameObject>();

        gameParents.Add(BlockBreakParent);
        gameParents.Add(SnekParent);

    }

    private void LateUpdate()
    {
        if(true == runSetup)
        {
            DisableAllGames();
            runSetup = false;
        }
    }

    /// <summary>
    /// Activates the given game. Also deactivates all other games
    /// </summary>
    /// <param name="gameToMakeActive">Game to be made active</param>
    public void MakeActiveGame(MenuSystem.Games gameToMakeActive)
    {
        // Add all parent objects to list. Nested foreach loop to loop over parents and their children to activate / deactiveate as needed
        switch (gameToMakeActive)
        {
            case MenuSystem.Games.NoGame:
                break;
            case MenuSystem.Games.BlockBreak:
                PreformSort(MenuSystem.Games.BlockBreak);
                break;
            case MenuSystem.Games.Snek:
                PreformSort(MenuSystem.Games.Snek);
                break;
            case MenuSystem.Games.MainMenu:
                break;
            default:
                break;
        }
    }

    private void PreformSort(MenuSystem.Games wantedGame)
    {
        foreach (var go in gameParents)
        {
            if(wantedGame.ToString() == go.name)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }

    public void DisableAllGames()
    {
        foreach (var go in gameParents)
        {
            go.SetActive(false);
        }
    }

    #region Public Methods

    public void MakeBlockBreakActive()
    {
        MakeActiveGame(MenuSystem.Games.BlockBreak);
    }
    public void MakeSnekActive()
    {
        MakeActiveGame(MenuSystem.Games.Snek);
    }
    public void MakeNoGameActive()
    {
        MakeActiveGame(MenuSystem.Games.NoGame);
    }

    #endregion
}