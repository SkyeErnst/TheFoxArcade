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
        List<Segment> segLis = SnekSegmentController.GetSegmentList();
        SortedDictionary<Segment, Vector2> segDict = SnekSegmentController.GetSegmentDict();

        Vector2 originalHeadPos;
        //segDict.TryGetValue(segLis[0], out originalHeadPos);

        originalHeadPos = SnekHead.transform.position;

        SnekHead.transform.Translate(movementVector / movementDampen);

        if(0 < segLis.Count)
        {
            segLis.Last().gameObject.transform.position = originalHeadPos;

            segLis.Insert(0, segLis.Last());
            segLis.RemoveAt(segLis.Count - 1);
        }
        
        // In a for loop, set the location of the moving segemnet then move that segment to correct location


        

        //List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());
        //segLis[0].gameObject.transform.Translate(movementVector / movementDampen);

        //// This needs to handle when there is only one segment
        //for (int i = 1; i < segLis.Count; i++)
        //{
        //    Segment movingSeg = segLis[i];
        //    Segment lastMovedSeg = segLis[i-1];
        //    Transform headTrans = SnekHead.transform;

        //    Debug.Log(movingSeg.gameObject.name);
        //    if (movingSeg.WantedMovementDirection == lastMovedSeg.WantedMovementDirection)
        //    {
        //        Debug.Log("Moving as normal");
        //        movingSeg.gameObject.transform.Translate(movementVector / movementDampen);
        //        movingSeg.WantedMovementDirection = movementVector;
        //    }
        //    if(movingSeg.WantedMovementDirection != lastMovedSeg.WantedMovementDirection)
        //    {
        //        //segLis.Last<>
                
        //        //Debug.Log("Adjusting trajectory");
        //        Debug.Log("Current Direction: " + movingSeg.WantedMovementDirection + "Direction to Change to: " + lastMovedSeg.WantedMovementDirection);

        //        movingSeg.gameObject.transform.Translate(movingSeg.WantedMovementDirection / movementDampen);
        //        movingSeg.WantedMovementDirection = lastMovedSeg.WantedMovementDirection;
        //    }
        //}

        ////foreach (Segment seg in segLis)
        ////{
            
        ////    //Get the gameobject of each segment and transform it in the correct direction
        ////    GameObject go = seg.gameObject;
        ////    go.transform.Translate(movementVector / movementDampen);
        ////}
    }
}