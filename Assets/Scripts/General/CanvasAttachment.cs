using UnityEngine;
/// <summary>
/// Class to be attatched to any canvas object to facilitate the finding 
/// and sorting canvas objects
/// </summary>
[RequireComponent(typeof(Canvas))]
public class CanvasAttachment : MonoBehaviour
{
    #region Public Properties
    public MenuSystem.Canvases CanvasType
    {
        get
        {
            return canvasType;
        }
    }
    #endregion

    #region Private Fields
    /// <summary>
    /// The type of canvas that is attached to the same object as this class
    /// </summary>
    private MenuSystem.Canvases canvasType;
    /// <summary>
    /// The name inside the unity inspector of the main breakout canvas, the one that
    /// displays the score and score multplier
    /// </summary>
    private const string BREAKOUT_CANVAS_MAIN = "BreakoutCanvas";
    /// <summary>
    /// The name inside the unity inspecter of the breakout canvas that deals with 
    /// displaying the text and buttons related to the winning and losing
    /// </summary>
    private const string BREAKOUT_CANVAS_WINLOSE = "BreakoutCanvasSub";
    /// <summary>
    /// The name inside the unity inpsecter of the general canvas that handels the escape
    /// menu functions, such as chaning settings and quiting the game.
    /// </summary>
    private const string GENERAL_CANVAS_ESCMENU = "GeneralCanvasESCMenu";
    #endregion
    private void Awake()
    {
        string goName = gameObject.name;
        switch (name)
        {
            case BREAKOUT_CANVAS_MAIN:
                canvasType = MenuSystem.Canvases.BreakoutGame;
                break;
            case BREAKOUT_CANVAS_WINLOSE:
                canvasType = MenuSystem.Canvases.BreakoutWinLose;
                break;
            case GENERAL_CANVAS_ESCMENU:
                canvasType = MenuSystem.Canvases.EscMenu;
                break;
            default:
                Debug.LogError("Something has gone wrong here");
                break;
        }
        Debug.Log("Type: " + CanvasType);
    }
}