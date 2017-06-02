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
    private Dictionary <MenuSystem.Games, List<GameObject>> gameDict;

    private List<GameObject> blockBreakChildren;
    private List<GameObject> snekChildren;

    private List<GameObject> gameParents;

    /// <summary>
    /// True when the inital setup should be run.
    /// Used exclusivly for LateUpdate in this class.
    /// </summary>
    private bool runSetup = true;
    #endregion

    private void Awake()
    {
        blockBreakChildren = new List<GameObject>();
        snekChildren = new List<GameObject>();
        gameParents = new List<GameObject>();
        gameDict = new Dictionary<MenuSystem.Games, List<GameObject>>();

        gameParents.Add(BlockBreakParent);
        gameParents.Add(SnekParent);

        foreach (Transform child in BlockBreakParent.transform)
        {
            blockBreakChildren.Add(child.gameObject);
        }
        foreach (Transform child in SnekParent.transform)
        {
            snekChildren.Add(child.gameObject);
        }

        gameDict.Add(MenuSystem.Games.BlockBreak, blockBreakChildren);
        gameDict.Add(MenuSystem.Games.Snek, snekChildren);

        Debug.LogWarning("Using invoke to disable gameobjects. If there are" +
            " random null reff errors, this may be why");
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
    /// <param name="gameToMakeActive"></param>
    //public void MakeActiveGame(MenuSystem.Games gameToMakeActive)
    //{
    //    // Add all parent objects to list. Nested foreach loop to loop over parents and their children to activate / deactiveate as needed
    //    switch (gameToMakeActive)
    //    {
    //        case MenuSystem.Games.NoGame:
    //            break;
    //        case MenuSystem.Games.BlockBreak:
    //            PreformSort(MenuSystem.Games.BlockBreak);
    //            break;
    //        case MenuSystem.Games.Snek:
    //            PreformSort(MenuSystem.Games.Snek);
    //            break;
    //        case MenuSystem.Games.MainMenu:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void MakeActiveGame(MenuSystem.Games gameToMakeActive)
    {
        // Add all parent objects to list. Nested foreach loop to loop over parents and their children to activate / deactiveate as needed
        switch (gameToMakeActive)
        {
            case MenuSystem.Games.NoGame:
                break;
            case MenuSystem.Games.BlockBreak:
                //PreformSort(MenuSystem.Games.BlockBreak);
                break;
            case MenuSystem.Games.Snek:
                //PreformSort(MenuSystem.Games.Snek);
                break;
            case MenuSystem.Games.MainMenu:
                break;
            default:
                break;
        }
    }

    private void PreformSort(MenuSystem.Games wantedGame)
    {
        Debug.Log("Sorting!");
        foreach (var go in gameParents)
        {
            if(wantedGame.ToString() == go.name)
            {
                Debug.Log("Enabiling " + go.name);
                go.SetActive(true);
            }
            else
            {
                Debug.Log("Wanted game: " + wantedGame.ToString());
                go.SetActive(false);
            }
        }
    }

    //private void PreformSort(MenuSystem.Games wantedGame)
    //{
    //    List<GameObject> enable = new List<GameObject>();
    //    List<GameObject> disable = new List<GameObject>();

    //    foreach (KeyValuePair<MenuSystem.Games, List<GameObject>> item in gameDict)
    //    {
    //        if(wantedGame == item.Key)
    //        {
    //            gameDict.TryGetValue(item.Key, out enable);
    //        }
    //        else
    //        {
    //            foreach (GameObject go in item.Value)
    //            {
    //                disable.Add(go);
    //            }
    //        }
    //    }

    //    foreach (GameObject enableThis in enable)
    //    {
    //        enableThis.SetActive(true);
    //        Debug.Log("Activated " + enableThis.name);
    //        Debug.Log("Hir " + enableThis.activeInHierarchy);
    //        Debug.Log("self " + enableThis.activeSelf);
    //    }
    //    foreach (GameObject disableThis in disable)
    //    {
    //        disableThis.SetActive(false);
    //        Debug.Log("Disabled " + disableThis.name);
    //    }

    //}

    //public void DisableAllGames()
    //{
    //    foreach (KeyValuePair<MenuSystem.Games, List<GameObject>> item in gameDict)
    //    {
    //        foreach (GameObject go in item.Value)
    //        {
    //            go.SetActive(false);
    //        }
    //    }
    //}

    public void DisableAllGames()
    {
        Debug.Log("Disabling all games");
        foreach (var go in gameParents)
        {
            Debug.Log("Disabling " + go.name);
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