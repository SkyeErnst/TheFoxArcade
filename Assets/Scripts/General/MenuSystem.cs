using UnityEngine;

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
    #endregion

    #region Public Properties
    /// <summary>
    /// Current state of being -Globaly- paused or unpaused
    /// </summary>
    public GlobalPauseState PauseState { get; set; }
    #endregion 

    #region Private Fields

    #endregion


}
