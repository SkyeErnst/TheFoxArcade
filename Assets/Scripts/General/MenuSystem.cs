using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuSystem : MonoBehaviour
{
    #region enums
    /// <summary>
    /// The pause state of the whole application
    /// </summary>
    public enum PauseState
    {
        Paused = 0,
        Unpaused = 1
    }
    public enum Canvases
    {
        /// <summary>
        /// The main canvas for the Blockbreak game.
        /// Contains information such as score and score multiplier
        /// </summary>
        BlockBreak = 0,
        /// <summary>
        /// This canvas displays the win/loss text and offers to let the
        /// player try again or exit the cabinet
        /// </summary>
        BreakoutWinLose = 1,
        /// <summary>
        /// The menu that comes up when the player his the esc key
        /// </summary>
        EscMenu = 2,
        /// <summary>
        /// The menu of the game before loading
        /// </summary>
        MainMenu = 3,
        /// <summary>
        /// The main canvas for the snek game
        /// </summary>
        SnekGame = 4

    }
    public enum Games
    {
        
        BlockBreak = 1,
        Snek = 2,
        MainMenu = 100,
        // <summary>
        /// Active when there is no arcade game being played
        /// </summary>
        NoGame = 200
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Current state of being -Globaly- paused or unpaused
    /// </summary>
    public static PauseState GlobalPauseState { get; set; }
    public static Games ActiveGame { get; set; }
    #endregion

    #region Public Fields
    /// <summary>
    /// The text object to display the win/loss result to
    /// </summary>
    public Text WinLoseResultDisplayText;
    /// <summary>
    /// Stores all the availible canvases in a way that is searchable
    /// </summary>
    public Dictionary<Canvases, Canvas> CanvasDict;

    /// <summary>
    /// The Game Object that has the dropdown component attached
    /// </summary>
    public GameObject GamesDropdownGO;

    /// <summary>
    /// Reference to the game manager class. Used in the Dropdown delegate to 
    /// load new games.
    /// </summary>
    public GameManager GameManagerRef;

    /// <summary>
    /// Reference to reloading manager on the _Keeper object.
    /// </summary>
    public ReloadingManager ReloadMan;

    /// <summary>
    /// Reference to the game manager on the _Keeper object
    /// </summary>
    public GameManager gameMan;

    /// <summary>
    /// Reference to the food manager class on the _Keeper object
    /// </summary>
    public FoodManager FoodMan;

    /// <summary>
    /// Referece to keep moving forward class
    /// </summary>
    public KeepMovingForward keepMovingFwd;
    #endregion

    #region Private Fields
    /// <summary>
    /// The canvas tag as stored in the tag manager
    /// </summary>
    private const string CANVAS_TAG = "Canvas";

    /// <summary>
    /// The text to be displayed when a breakout level is WON
    /// </summary>
    private const string BLOCKBREAK_WIN_TEXT = "Cleared! \n Nice job!";

    /// <summary>
    /// The text to be displayed when a breakout level is LOST
    /// </summary>
    private const string BLOCKBREAK_LOSE_TEXT = "Nice try...";

    /// <summary>
    /// Whether or not the dropdown on the main menu is being dislayed
    /// </summary>
    private bool dropdownIsDisplayed = false;

    /// <summary>
    /// Reference to the dropdown component 
    /// </summary>
    private Dropdown gamesDropdown;

    #endregion

    private void Start()
    {
        gamesDropdown = GamesDropdownGO.GetComponent<Dropdown>();

        gamesDropdown.onValueChanged.AddListener(delegate 
        {
            DropdownValueChangedListener(gamesDropdown);
        }
        );

        ActiveGame = Games.MainMenu;
        GlobalPauseState = PauseState.Unpaused;
        GamesDropdownGO.SetActive(dropdownIsDisplayed);
        Init();
    }
    /// <summary>
    /// Used to move activation of menu to after initialized of menu components and types
    /// is complete
    /// </summary>
    private void Init()
    {
        GameObject[] goArray = GameObject.FindGameObjectsWithTag(CANVAS_TAG);
        CanvasDict = new Dictionary<Canvases, Canvas>();

        foreach (GameObject canvasGO in goArray)
        {
            Canvases typeToAdd = canvasGO.GetComponent<CanvasAttachment>().CanvasType;
            Canvas canvasToAdd = canvasGO.GetComponent<Canvas>();

            CanvasDict.Add(typeToAdd, canvasToAdd);
        }
        MakeActiveCanvas(Canvases.MainMenu);
    }

    /// <summary>
    /// Delegate listener for when the dropdown value changes on the main menu
    /// </summary>
    /// <param name="target"></param>
    private void DropdownValueChangedListener(Dropdown target)
    {
        GameManagerRef.MakeActiveGame((Games)target.value);
        if(Games.Snek == (Games)target.value)
        {
            ReloadMan.ResetGame(Games.Snek);
            FoodMan.StartSpawningFood();
            keepMovingFwd.StartMovement();
        }
    }
    /// <summary>
    /// Makes the passed in canvas active and sets all others to inactive
    /// </summary>
    /// <param name="canvasToDisplay"></param>
    public void MakeActiveCanvas(Canvases canvasToDisplay)
    {
        foreach (Canvas canvas in CanvasDict.Values)
        {
            if(canvasToDisplay == canvas.gameObject.GetComponent<CanvasAttachment>().CanvasType)
            {
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }
        }

        if(Canvases.MainMenu == canvasToDisplay)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// Makes the passed in canvases active and setts all others to inactive
    /// </summary>
    /// <param name="canvas"></param>
    public void MakeActiveCanvas(Canvases[] canvasType)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Call to display win or loss text
    /// </summary>
    /// <param name="activeGame">The arcade game currently being played</param>
    /// <param name="winLose">Wether the game was won or lost. True = won, false = lost</param>
    public void OnWinLose(Games activeGame, bool winLose)
    {
        if(true == winLose)
        {
            WinLoseResultDisplayText.text = BLOCKBREAK_WIN_TEXT;
            Pause();
            MakeActiveCanvas(Canvases.BreakoutWinLose);
            CursorManager.ChangeCursorState(CursorLockMode.None);
        }
        else if (false == winLose)
        {
            WinLoseResultDisplayText.text = BLOCKBREAK_LOSE_TEXT;
            Pause();
            MakeActiveCanvas(Canvases.BreakoutWinLose);
            CursorManager.ChangeCursorState(CursorLockMode.None);
        }
        else
        {
            throw new System.Exception("Game was neither won or lost. This should be impossible to reach");
        }
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    public void UnPause()
    {
        switch (ActiveGame)
        {
            case Games.BlockBreak:
                MakeActiveCanvas(Canvases.BlockBreak);
                CursorManager.ChangeCursorState(CursorLockMode.Locked);
                break;
            case Games.Snek:
                MakeActiveCanvas(Canvases.SnekGame);
                CursorManager.ChangeCursorState(CursorLockMode.Locked);
                break;
            case Games.MainMenu:
                MakeActiveCanvas(Canvases.MainMenu);
                CursorManager.ChangeCursorState(CursorLockMode.None);
                break;
            case Games.NoGame:
                CursorManager.ChangeCursorState(CursorLockMode.Locked);
                break;
            default:
                Debug.Log("Somethng has gone wrong here");
                break;
        }
        Time.timeScale = 1.0f;
        GlobalPauseState = PauseState.Unpaused;
        
    }
    /// <summary>
    /// Pauses the game
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
        GlobalPauseState = PauseState.Paused;
    }
    /// <summary>
    /// Pauses the game, while switiching the displayed canvas
    /// </summary>
    /// <param name="canvasToShow"></param>
    public void Pause(Canvases canvasToShow)
    {
        Time.timeScale = 0.0f;
        GlobalPauseState = PauseState.Paused;
        //CursorManager.ChangeCursorState(CursorLockMode.Confined);
        MakeActiveCanvas(canvasToShow);
    }

    /// <summary>
    /// Unity method
    /// </summary>
    private void Update()
    {
        if(true == Input.GetKeyDown(KeyCode.Escape) && GlobalPauseState == PauseState.Unpaused && Games.MainMenu != ActiveGame)
        {
            Pause(Canvases.EscMenu);
        }
        else if(true == Input.GetKeyDown(KeyCode.Escape) && GlobalPauseState == PauseState.Paused && Games.MainMenu != ActiveGame)
        {
            UnPause();
        }
    }

    /// <summary>
    /// Switches the dropdown menu to be displayed or not
    /// </summary>
    public void SwitchDropdownState()
    {
        dropdownIsDisplayed = !dropdownIsDisplayed;

        GamesDropdownGO.SetActive(dropdownIsDisplayed);
    }

    /// <summary>
    /// Sets active game to main menu and makes the main menu canvas active while disabiling all others
    /// </summary>
    public void QuitToMainMenu()
    {
        ActiveGame = Games.MainMenu;
        MakeActiveCanvas(Canvases.MainMenu);
        ReloadMan.ResetGame(ActiveGame);
        gameMan.MakeActiveGame(Games.MainMenu);
    }

    /// <summary>
    /// Imidietly exits the game with no promt
    /// </summary>
    public void QuitToOS()
    {
        Application.Quit();
    }
}