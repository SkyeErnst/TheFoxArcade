using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class in Charge of adding and moving the segments of the snake
/// </summary>
public class SnekSegmentController : MonoBehaviour
{
    #region Public Fields
    public GameObject SnekHead;
    #endregion

    #region Private Fields
    private List<Segment> segmentList;
    #endregion

    private void Awake()
    {
        segmentList = new List<Segment>();
        segmentList.Add(SnekHead.GetComponent<Segment>());
    }
}
