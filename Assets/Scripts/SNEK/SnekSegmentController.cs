using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

/// <summary>
/// Class in Charge of adding and moving the segments of the snake
/// </summary>
public class SnekSegmentController : MonoBehaviour
{

    #region Public Properties
    
    #endregion

    #region Public Fields
    public GameObject SnekHead;
    public GameObject BodySegmentPrefab;

    /// <summary>
    /// parent object that all snek game ojbects are put under
    /// </summary>
    public Transform SnekParent;
    #endregion

    #region Private Fields
    private static List<Segment> segmentList;

    private float segmentOffset = 0.15f;

    private int segmentsSpawned = 0;
    #endregion

    private void Awake()
    {
        segmentList = new List<Segment>();
    }

    /// <summary>
    /// Returns reference to segment list
    /// </summary>
    /// <returns></returns>
    public static List<Segment> GetSegmentList() // Look into pass by reference
    {
        return segmentList;
    }

    public void ClearSegmentList()
    {
        segmentList.Clear();
    }

    /// <summary>
    /// Adds a new segment to the back of the snek
    /// </summary>
    public void AddSegment()
    {
        // Find location of last segment and instantiate segment prefab at an offset from the last segment.
        List<Segment> segLis = GetSegmentList();
        Transform last;

        if (0 == segLis.Count)
        {
            last = SnekHead.transform;
        }
        else
        {
            last = segLis.Last().gameObject.transform;
        }
        Vector2 wantedSpawnPos = last.position;

        wantedSpawnPos.y -= segmentOffset;

        GameObject go = Instantiate(BodySegmentPrefab, last.position, last.rotation);
        segmentList.Add(go.GetComponent<Segment>());
        

        go.transform.position = wantedSpawnPos;
        go.name = "BodySegment " + segmentsSpawned;
        segmentsSpawned++;
        go.tag = "Segment";

        Segment segComp = go.GetComponent<Segment>();
        segComp.SegmentType = Segment.SegmentTypes.Body;
        segComp.WantedMovementDirection = SnekHead.GetComponent<Segment>().WantedMovementDirection;

    }

    public void ResetSegments()
    {
        segmentList.Clear();
        segmentsSpawned = 0;
    }
}