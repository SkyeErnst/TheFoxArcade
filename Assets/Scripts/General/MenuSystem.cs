using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        /// The main canvas for the breakout game.
        /// Contains information such as score and score multiplier
        /// </summary>
        BreakoutGame = 0,
        /// <summary>
        /// This canvas displays the win/loss text and offers to let the
        /// player try again or exit the cabinet
        /// </summary>
        BreakoutWinLose = 1,
        /// <summary>
        /// The menu that comes up when the player his the esc key
        /// </summary>
        EscMenu = 2
    }
    public enum ActiveGame
    {
        /// <summary>
        /// Active when there is no arcade game being played
        /// </summary>
        NoGame = 0,
        Breakout = 1
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Current state of being -Globaly- paused or unpaused
    /// </summary>
    public static PauseState GlobalPauseState { get; set; }
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
    #endregion

    #region Private Fields
    /// <summary>
    /// The canvas tag as stored in the tag manager
    /// </summary>
    private const string CANVAS_TAG = "Canvas";

    /// <summary>
    /// The text to be displayed when a breakout level is WON
    /// </summary>
    private const string BREKOUT_WIN_TEXT = "Cleared! \n Nice job!";

    /// <summary>
    /// The text to be displayed when a breakout level is LOST
    /// </summary>
    private const string BREKOUT_LOSE_TEXT = "Nice try...";

    private ActiveGame activeGame;
    #endregion

    private void Start()
    {
        activeGame = ActiveGame.Breakout;
        GlobalPauseState = PauseState.Unpaused;
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
        MakeActiveCanvas(Canvases.BreakoutGame);
    }
    /// <summary>
    /// Makes the passed in canvas active and setts all others to inactive
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
    /// <param name="winLose">Weather the game was won or lost. True = won, false = lost</param>
    public void OnWinLose(ActiveGame activeGame, bool winLose)
    {
        if(true == winLose)
        {
            WinLoseResultDisplayText.text = BREKOUT_WIN_TEXT;
            Pause();
            MakeActiveCanvas(Canvases.BreakoutWinLose);
            CursorManager.ChangeCursorState(CursorLockMode.None);
        }
        else if (false == winLose)
        {
            WinLoseResultDisplayText.text = BREKOUT_LOSE_TEXT;
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
        if(ActiveGame.Breakout == activeGame)
        {
            MakeActiveCanvas(Canvases.BreakoutGame);
        }
        Time.timeScale = 1.0f;
        GlobalPauseState = PauseState.Unpaused;
        //MakeActiveCanvas(Canvases.BreakoutGame);
        CursorManager.ChangeCursorState(CursorLockMode.Locked);
    }
    /// <summary>
    /// Pauses the game
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0.0f;
        GlobalPauseState = PauseState.Paused;
        //CursorManager.ChangeCursorState(CursorLockMode.Confined);
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
        if(true == Input.GetKeyDown(KeyCode.Escape) && GlobalPauseState == PauseState.Unpaused)
        {
            Pause(Canvases.EscMenu);
        }
        else if(true == Input.GetKeyDown(KeyCode.Escape) && GlobalPauseState == PauseState.Paused)
        {
            UnPause();
        }
    }

    /// <summary>
    /// Imidietly exits the game with no promt
    /// </summary>
    public void QuitToOS()
    {
        Application.Quit();
    }
}
