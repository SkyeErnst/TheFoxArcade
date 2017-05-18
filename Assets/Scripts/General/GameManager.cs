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
    #endregion

    private void Awake()
    {
        blockBreakChildren = new List<GameObject>();
        snekChildren = new List<GameObject>();
        gameDict = new Dictionary<MenuSystem.Games, List<GameObject>>();

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
        Invoke("DisableAllGames", 2.0f);
    }

    /// <summary>
    /// Activates the given game. Also deactivates all other games
    /// </summary>
    /// <param name="gameToMakeActive"></param>
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
        List<GameObject> enable = new List<GameObject>();
        List<GameObject> disable = new List<GameObject>();

        foreach (KeyValuePair<MenuSystem.Games, List<GameObject>> item in gameDict)
        {
            if(wantedGame == item.Key)
            {
                gameDict.TryGetValue(item.Key, out enable);
            }
            else
            {
                foreach (GameObject go in item.Value)
                {
                    disable.Add(go);
                }
            }
        }

        foreach (GameObject enableThis in enable)
        {
            enableThis.SetActive(true);
        }
        foreach (GameObject disableThis in disable)
        {
            disableThis.SetActive(false);
        }

    }

    public void DisableAllGames()
    {
        foreach (KeyValuePair<MenuSystem.Games, List<GameObject>> item in gameDict)
        {
            foreach (GameObject go in item.Value)
            {
                go.SetActive(false);
            }
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