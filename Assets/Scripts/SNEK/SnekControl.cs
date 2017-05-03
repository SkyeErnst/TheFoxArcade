using System;
using UnityEngine;

/// <summary>
/// Class for controling the snek
/// </summary>
public class SnekControl : MonoBehaviour
{
    #region enums
    public enum DesiredDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }
    #endregion

    #region Public Properties

    public static DesiredDirection CurrentDesiredDirection
    {
        get
        {
            return currDesire;
        }
    }

    #endregion

    private void Awake()
    {
        System.Random rand = new System.Random();

        int index = rand.Next(0, 3);
        currDesire = (DesiredDirection)index;
    }

    #region Private Fields
    private static DesiredDirection currDesire;
    #endregion
    private void Update()
    {
        if(Input.GetKey(KeyCode.W)) // Up
        {
            currDesire = DesiredDirection.Up;
        }
        if (Input.GetKey(KeyCode.S)) // Down
        {
            currDesire = DesiredDirection.Down;
        }
        if (Input.GetKey(KeyCode.A)) // Left
        {
            currDesire = DesiredDirection.Left;
        }
        if (Input.GetKey(KeyCode.D)) // Right
        {
            currDesire = DesiredDirection.Right;
        }
    }
}