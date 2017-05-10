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

    private float waitTimeBeforeMovement = 0.1f;

    /// <summary>
    /// Diviser in the movement calculation
    /// </summary>
    private float movementDampen = 10.0f;
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
        // This method needs to be changed. What should be done is this:
        // Each segement will be a prefab containing the segment and an empty game object
        // The empty game object is there as a marker to show where to move the segments to.
        // Each time the movement function ticks. Everything is shifted and (if needed) rotated forward by an arbitrary but uniform amount. (However far feels "far enough")

        List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());

        foreach (Segment seg in segLis)
        {
            //Get the gameobject of each segment and transform it in the correct direction
            GameObject go = seg.gameObject;
            go.transform.Translate(movementVector / movementDampen);
        }
    }
}