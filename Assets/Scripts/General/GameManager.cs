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

    public MenuSystem MenSys;

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

    /// <summary>
    /// Unity Method. 
    /// This is used as an init so that all setup is done before disabilng game objects
    /// </summary>
    private void LateUpdate()
    {
        if(true == runSetup)
        {
            DisableAllGames();
            runSetup = false;
        }
    }

    /// <summary>
    /// Activates the given game. Also deactivates all other games and 
    /// calls Make Active Canvas to setup the correct canvas.
    /// </summary>
    /// <param name="gameToMakeActive">Game to be made active</param>
    public void MakeActiveGame(MenuSystem.Games gameToMakeActive)
    {
        // Add all parent objects to list. Nested foreach loop to loop over parents and their children to activate / deactiveate as needed
        switch (gameToMakeActive)
        {
            case MenuSystem.Games.NoGame:
                MenuSystem.ActiveGame = MenuSystem.Games.NoGame;
                break;
            case MenuSystem.Games.BlockBreak:
                MenuSystem.ActiveGame = MenuSystem.Games.BlockBreak;
                PreformSort(MenuSystem.Games.BlockBreak);
                break;
            case MenuSystem.Games.Snek:
                MenuSystem.ActiveGame = MenuSystem.Games.Snek;
                PreformSort(MenuSystem.Games.Snek);
                break;
            case MenuSystem.Games.MainMenu:
                MenuSystem.ActiveGame = MenuSystem.Games.MainMenu;
                PreformSort(MenuSystem.Games.MainMenu);
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
                switch (wantedGame)
                {
                    case MenuSystem.Games.BlockBreak:
                        MenSys.MakeActiveCanvas(MenuSystem.Canvases.BlockBreak);
                        break;
                    case MenuSystem.Games.Snek:
                        MenSys.MakeActiveCanvas(MenuSystem.Canvases.SnekGame);
                        break;
                    case MenuSystem.Games.MainMenu:
                        MenSys.MakeActiveCanvas(MenuSystem.Canvases.MainMenu);
                        break;
                    case MenuSystem.Games.NoGame:
                        break;
                    default:
                        break;
                }
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