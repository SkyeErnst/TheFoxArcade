using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    #region enums
    /// <summary>
    /// The pause state of the whole application
    /// </summary>
    public enum GlobalPauseState
    {
        Paused = 0,
        Unpaused = 1
    }
    /// <summary>
    /// The pause state of specificly Breakout game
    /// </summary>
    public enum BreakoutPause
    {
        Paused = 0,
        Unpaused = 1
    }
    public enum Canvases
    {
        BreakoutGame = 0,
        BreakoutLose = 1,
        EscMenu = 2
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Current state of being -Globaly- paused or unpaused
    /// </summary>
    public GlobalPauseState PauseState { get; set; }
    #endregion

    #region Public Fields
    /// <summary>
    /// List of all canvases
    /// </summary>
    public List<Canvas> CanvasList;

    public Text BreakoutResultDisplayText;

    public Dictionary<Canvases, string> CanvasDict;
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
    #endregion

    //try again / cleared text goes here

    private void Awake()
    {
        GameObject[] goArray = GameObject.FindGameObjectsWithTag(CANVAS_TAG);
        CanvasList = new List<Canvas>();
        CanvasDict = new Dictionary<Canvases, string>();

        foreach (GameObject canvasGO in goArray)
        {
            CanvasList.Add(canvasGO.GetComponent<Canvas>());
        }
        
    }
}
