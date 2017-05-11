using UnityEngine;

public class Segment : MonoBehaviour
{
    #region Enums
    public enum SegmentTypes
    {
        Head = 0,
        Body = 1,
        Tail = 0
    }
    #endregion

    #region Public Properties
    public SegmentTypes SegmentType
    {
        get
        {
            return thisSegmentType;
        }
        set
        {
            thisSegmentType = value;
        }
    }
    public Vector2 WantedMovementDirection
    {
        get
        {
            return wantedMovementDirection;
        }

        set
        {
            wantedMovementDirection = value;
        }
    }
    #endregion

    #region Private Fields
    private SegmentTypes thisSegmentType;

    private Vector2 wantedMovementDirection;
    #endregion

    private void Awake()
    {
        if("SnekHead" == gameObject.tag)
        {
            thisSegmentType = SegmentTypes.Head;
        }
        string tag = gameObject.tag;
        switch (tag)
        {
            case "SnekHead":
                thisSegmentType = SegmentTypes.Head;
                break;
            case "SnekBody":
                thisSegmentType = SegmentTypes.Body;
                break;
            case "SnekTail":
                thisSegmentType = SegmentTypes.Tail;
                break;
        }


        //make swith block here. switch on segment type
    }
}