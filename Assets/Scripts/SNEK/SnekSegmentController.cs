using System.Linq;
using System.Collections.Generic;
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
    #endregion

    private void Awake()
    {
        segmentList = new List<Segment>();
        segmentList.Add(SnekHead.GetComponent<Segment>());
    }

    /// <summary>
    /// Returns reference to segment list
    /// </summary>
    /// <returns></returns>
    public static List<Segment> GetSegmentList() // Look into pass by refference
    {
        return segmentList;
    }

    public void AddSegment()
    {
        // Find location of last segment and instantiate segment prefab at an offset from the last segment. Rename segments to new job (body / tail)
        List<Segment> segLis = GetSegmentList();
        Transform last = segLis.Last().gameObject.transform;
        Vector2 wantedSpawnPos = last.position;
        wantedSpawnPos.y -= segmentOffset;
        GameObject go = Instantiate(BodySegmentPrefab, last.position, last.rotation);
        go.transform.position = wantedSpawnPos; //new 
        segmentList.Add(go.GetComponent<Segment>());
        Segment segComp = go.GetComponent<Segment>();
        segComp.SegmentType = Segment.SegmentTypes.Body;
    }
}