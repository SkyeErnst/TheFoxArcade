using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private void MoveSnek(Vector2 wantedDir)
    {
        Segment segHead = SnekHead.GetComponent<Segment>();
        segHead.WantedMovementDirection = wantedDir; 
    }
    /// <summary>
    /// Loops over snek segemnts and moves them all.
    /// </summary>
    private void MoveSnekStack(Vector2 movementVector)
    {


        List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());
        segLis[0].gameObject.transform.Translate(movementVector / movementDampen);

        // This needs to handle when there is only one segment
        for (int i = 1; i < segLis.Count; i++)
        {
            Segment movingSeg = segLis[i];
            Segment lastMovedSeg = segLis[i-1];
            if(movingSeg.WantedMovementDirection == lastMovedSeg.WantedMovementDirection)
            {
                Debug.Log("Moving as normal");
                movingSeg.gameObject.transform.Translate(movementVector / movementDampen);
                movingSeg.WantedMovementDirection = movementVector;
            }
            if(movingSeg.WantedMovementDirection != lastMovedSeg.WantedMovementDirection)
            {
                //Debug.Log("Adjusting trajectory");
                //Debug.Log("Current Direction: " + movingSeg.WantedMovementDirection + "Direction to Change to: " + lastMovedSeg.WantedMovementDirection);
                Debug.Log("Snek head meme: " + SnekHead.GetComponent<Segment>().WantedMovementDirection);
                movingSeg.gameObject.transform.Translate(lastMovedSeg.WantedMovementDirection / movementDampen);
                movingSeg.WantedMovementDirection = lastMovedSeg.WantedMovementDirection;
            }
        }
        //foreach (Segment seg in segLis)
        //{
            
        //    //Get the gameobject of each segment and transform it in the correct direction
        //    GameObject go = seg.gameObject;
        //    go.transform.Translate(movementVector / movementDampen);
        //}
    }
}