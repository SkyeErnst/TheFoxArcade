using System;
using UnityEngine;

/// <summary>
/// Class for the purpose of storing and managing the settings of the application
/// </summary>
public class SettingsMenu :  MonoBehaviour {

    #region enums
    public enum TargetFramerates
    {
        /// <summary>
        /// 24fps
        /// </summary>
        Cinimatic = 24,
        /// <summary>
        /// 30fps
        /// </summary>
        Minimum = 30,
        /// <summary>
        /// 60fps
        /// </summary>
        Standard = 60,
        /// <summary>
        /// 120fps
        /// </summary>
        Target = 120,
        /// <summary>
        /// 144fps
        /// </summary>
        Fast = 144,
        /// <summary>
        /// 240fps
        /// </summary>
        Sanic = 240,
        Uncapped = int.MaxValue
    }
    #endregion

    #region Public Properties
    public int TargetFrameRate { get; set; }
    #endregion 

    private void Awake()
    {
        TargetFrameRate = (int)TargetFramerates.Fast;

        SetTargetFramerate(TargetFramerates.Fast);
    }

    public void SetTargetFramerate(TargetFramerates target)
    {
        Application.targetFrameRate = (int)target;
    }
}