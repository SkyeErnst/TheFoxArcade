using UnityEngine;

public class Segment : MonoBehaviour
{
    #region Enums
    private enum SegmentType
    {
        Head = 0,
        Body = 1,
        Tail = 0
    }
    #endregion

    #region Private Fields
    private SegmentType thisSegmentType;
    #endregion

    private void Awake()
    {
        if("SnekHead" == gameObject.tag)
        {
            thisSegmentType = SegmentType.Head;
        }
        //make swith block here. switch on segment type
    }
}