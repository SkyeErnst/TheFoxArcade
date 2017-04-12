using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    #region Enums
    protected enum BlockTypes
    {
        Normal = 0,
        TakesTwoHits = 1,
        DoublePoints = 2,
        TriplePoints = 3,
        Indestructable = 4,
    }
    public enum PowerUps
    {
        
    }

    #endregion

    #region Protected fields
    protected Dictionary<int, Color> colorDict;
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
        colorDict = new Dictionary<int, Color>();
        blockArray = new Block[51];
        goArray = new GameObject[51];

        colorDict.Add(0, Color.white);
        colorDict.Add(1, Color.red);
        colorDict.Add(2, Color.green);
        colorDict.Add(3, Color.blue);
        colorDict.Add(4, Color.black);

        goArray = GameObject.FindGameObjectsWithTag(blockTag);


        for (int i = 0; i < goArray.Length; i++)
        {
            blockArray[i] = goArray[i].gameObject.GetComponent<Block>();
            int rand = BiasedRandomNumber(0, 5);
            Color newColor;
            if(true == colorDict.TryGetValue(rand, out newColor))
            {
                blockArray[i].ChangeBlockCollor(newColor);
            }
        }
        blockList.AddRange(blockArray);

        //for (int i = 0; i < goArray.Length; i++)
        //{
        //    blockArray[i].ChangeBlockCollor(Color.black);
        //}

        //for (int i = 0; i < 10000; i++)
        //{
        //    Debug.Log("Value: " + BiasedRandomNumber(0, 4));
        //}

    }

    /// <summary>
    /// Returns an int with a liner falloff curve biased toward the minimum input
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private int BiasedRandomNumber(int min, int max)
    {

        int biasedRan = sysRand.Next(min, max) - sysRand.Next(min, max);
        if(min > biasedRan)
        {
            biasedRan = min;
        }
        else if (biasedRan > max)
        {
            biasedRan = max;
        }
        return biasedRan;
    }
}
