using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Enums
    protected enum BlockTypes
    {
        Normal = 1,
        TakesTwoHits = 2,
        DoublePoints = 3,
        TriplePoints = 4,
        Indestructable = 5,
    }

    #endregion

    #region Private Fields
    /// <summary>
    /// List of all current blocks
    /// </summary>
    private List<Block> blockList;

    /// <summary>
    /// Temporary array for storing refference to all the blocks in the level
    /// </summary>
    private GameObject[] goArray;

    /// <summary>
    /// Array of the Block clas
    /// </summary>
    private Block[] blockArray;

    private System.Random sysRand;

    /// <summary>
    /// Tag of Block GameObjects
    /// </summary>
    private readonly string blockTag = "Block";
    #endregion

    public void Awake()
    {
        sysRand = new System.Random();
        blockList = new List<Block>();
        blockArray = new Block[51];
        goArray = new GameObject[51];
        goArray = GameObject.FindGameObjectsWithTag(blockTag);


        for (int i = 0; i < goArray.Length; i++)
        {
            Debug.Log("Iteration: " + i);
            blockArray[i] = goArray[i].gameObject.GetComponent<Block>();
            
        }
        blockList.AddRange(blockArray);
    }

    /// <summary>
    /// Returns an int with a liner falloff curve biased toward the minimum input
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private int BiasedRandomNumber(int min, int max)
    {
        throw new System.NotImplementedException();
    }
}
