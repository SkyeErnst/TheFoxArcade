using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KeepMovingForward : MonoBehaviour
{

    #region Public Fields
    public GameObject SnekHead;
    #endregion
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

    private Segment snekHeadSegment;
    #endregion

    private void Awake()
    {
        coru = MoveForward();
        snekHeadSegment = GetComponent<Segment>();
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
                    snekHeadSegment.WantedMovementDirection = Vector2.up;
                    MoveSnekStack(Vector2.up);
                    break;
                case SnekControl.DesiredDirection.Down:
                    snekHeadSegment.WantedMovementDirection = Vector2.down;
                    MoveSnekStack(Vector2.down);
                    break;
                case SnekControl.DesiredDirection.Left:
                    snekHeadSegment.WantedMovementDirection = Vector2.left;
                    MoveSnekStack(Vector2.left);
                    break;
                case SnekControl.DesiredDirection.Right:
                    snekHeadSegment.WantedMovementDirection = Vector2.right;
                    MoveSnekStack(Vector2.right);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(waitTimeBeforeMovement);
        }
    }

    /// <summary>
    /// Wrapper method to prevent code duplication in MoveForward method
    /// </summary>
    /// <param name="wantedDir"></param>
    private void MoveSnek(Vector2 wantedDir)
    {
        Segment segHead = SnekHead.GetComponent<Segment>();
        segHead.WantedMovementDirection = wantedDir; 
    }

    /// <summary>
    /// Moves head forward and moves tail segment to last head position
    /// </summary>
    private void MoveSnekStack(Vector2 movementVector)
    {
        List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());
        Vector2 originalHeadPos;

        originalHeadPos = SnekHead.transform.position;

        SnekHead.transform.Translate(movementVector / movementDampen);

        if(0 < segLis.Count)
        {
            
            segLis.Last().gameObject.transform.position = originalHeadPos;

            segLis.Insert(0, segLis.Last());
            segLis.RemoveAt(segLis.Count - 1);
        }
    }

    /// <summary>
    /// Restarts the movement coroutine.
    /// </summary>
    public void RestartMovement()
    {
        StartCoroutine(coru);
    }

    /// <summary>
    /// Stops the movement coroutine.
    /// </summary>
    public void StopMovement()
    {
        StopCoroutine(coru);
    }
}