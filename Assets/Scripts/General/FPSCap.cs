using System;
using UnityEngine;

/// <summary>
/// Class for the perpose of keeping the framerate of the application in check
/// </summary>
public class FPSCap :  MonoBehaviour {

    #region enums
    public enum TargetFramerates
    {
        Cinimatic = 24,
        Minimum = 30,
        Standard = 60,
        Target = 120,
        Fast = 144,
        Sanic = 240,
        Uncapped = int.MaxValue
    }
    #endregion

    #region Public Properties
    public int TargetFrameRate { get; set; }
    #endregion 

    public void Awake()
    {
        TargetFrameRate = (int)TargetFramerates.Fast;

        Application.targetFrameRate = TargetFrameRate;
    }
}