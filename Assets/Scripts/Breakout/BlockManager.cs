using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Private Fields
    /// <summary>
    /// List of all current blocks
    /// </summary>
    private List<Block> blockList;

    private GameObject[] gameArray;
    private Block[] blockArray;

    /// <summary>
    /// Tag of Block GameObjects
    /// </summary>
    private readonly string blockTag = "Block";
    #endregion

    public void Awake()
    {
        blockList = new List<Block>();
        blockArray = new Block[51];
        gameArray = new GameObject[51];
        gameArray = GameObject.FindGameObjectsWithTag(blockTag);


        for (int i = 0; i < gameArray.Length; i++)
        {
            Debug.Log("Iteration: " + i);
            blockArray[i] = gameArray[i].gameObject.GetComponent<Block>();
            
        }
        blockList.AddRange(blockArray);
        
    }
}
