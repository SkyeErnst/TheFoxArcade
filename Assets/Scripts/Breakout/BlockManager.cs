using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Private Fields
    private List<Block> blockList;
    #endregion

    public void Awake()
    {
        blockList = new List<Block>();
    }
}
