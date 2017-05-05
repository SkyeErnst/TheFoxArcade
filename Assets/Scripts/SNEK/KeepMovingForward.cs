using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeepMovingForward : MonoBehaviour
{
    #region Private Fields
    /// <summary>
    /// stores reference to MoveForward corutine
    /// </summary>
    private IEnumerator coru;

    private float waitTimeBeforeMovement = 0.25f;

    /// <summary>
    /// Diviser in the movement calculation
    /// </summary>
    private float movementDampen = 3.0f;
    #endregion

    private void Awake()
    {
        coru = MoveForward();
    }

    private void Start()
    {
        StartCoroutine(coru);
    }

    /// <summary>
    /// Keeps the attachted object moving forward.
    /// Pretty much only meant for the Snek object
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveForward()
    {
        while (true)
        {
            switch (SnekControl.CurrentDesiredDirection)
            {
                case SnekControl.DesiredDirection.Up:
                    MoveSnekStack(Vector2.up);
                    break;
                case SnekControl.DesiredDirection.Down:
                    MoveSnekStack(Vector2.down);
                    break;
                case SnekControl.DesiredDirection.Left:
                    MoveSnekStack(Vector2.left);
                    break;
                case SnekControl.DesiredDirection.Right:
                    MoveSnekStack(Vector2.right);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(waitTimeBeforeMovement);
        }
    }

    /// <summary>
    /// Loops over snek segemnts and moves them all.
    /// </summary>
    private void MoveSnekStack(Vector2 movementVector)
    {
        List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());

        foreach (Segment seg in segLis)
        {
            //Get the gameobject of each segment and transform it in the correct direction
            GameObject go = seg.gameObject;
            go.transform.Translate(movementVector / movementDampen);
        }
    }
}