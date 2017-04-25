using System;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    #region Public Fields
    public GameObject BlockBreakParticlePrefab;
    public GameObject PaddleHitParticlePrefab;
    public GameObject WallHitParticlePrefab;
    public GameObject BallLostParticlePrefab;
    #endregion

    /// <summary>
    /// An enum containing the different types of particle effects availible
    /// </summary>
    public enum ParticleType
    {
        BlockBreak = 0,
        PaddleHit,
        WallHit,
        BallLost
    }

    private enum ScreenSide
    {
        Left = 0,
        Right
    }

    // To rotate the wall particle system correctly, try finding if the left or right wall is hit,
    // and then rotate 90 degrees in the needed direction

    /// <summary>
    /// Instantiates a new empty GameObject at spawnPoint that contains the given particle system type
    /// Rotation of the new object is handeled internally.
    /// </summary>
    /// <param name="spawnPoint"></param>
    /// <param name="normal"></param>
    /// <param name="particleType"></param>
    public void SpawnSystem(Vector3 spawnPoint, Vector3 normal, ParticleType particleType)
    {
        GameObject newGO;
        Quaternion newRot; 
        switch (particleType)
        {
            case ParticleType.BlockBreak:
                newGO = Instantiate(BlockBreakParticlePrefab, spawnPoint, Quaternion.identity);
                newRot = Quaternion.LookRotation(normal);
                newGO.transform.rotation = newRot;
                newGO.GetComponent<TimedDestroy>().DestroyAfterTime(5.0f);
                break;
            case ParticleType.BallLost:
                break;
            case ParticleType.PaddleHit:
                newGO = Instantiate(PaddleHitParticlePrefab, spawnPoint, Quaternion.identity);
                newRot = Quaternion.LookRotation(normal);
                newGO.transform.rotation = newRot;
                newGO.GetComponent<TimedDestroy>().DestroyAfterTime(5.0f);
                break;
            case ParticleType.WallHit:
                newGO = Instantiate(WallHitParticlePrefab, spawnPoint, Quaternion.identity);
                if(ScreenSide.Left ==(FindLeftOrRight(spawnPoint)))
                {
                    newRot = Quaternion.Euler(0, 0, 270);
                }
                else
                {
                    newRot = Quaternion.Euler(0, 0, 90);
                }
                newGO.transform.rotation = newRot;
                newGO.GetComponent<TimedDestroy>().DestroyAfterTime(5.0f);
                break;
        }
    }

    private ScreenSide FindLeftOrRight(GameObject obj)
    {
        if(obj.transform.position.x > 0.0f)
        {
            return ScreenSide.Right;
        }
        if (obj.transform.position.x < 0.0f)
        {
            return ScreenSide.Left;
        }
        else
        {
            throw new Exception("Contact is neither left or right of center. wat?");
        }
    }
    private ScreenSide FindLeftOrRight(Vector3 obj)
    {
        if (obj.x > 0.0f)
        {
            return ScreenSide.Right;
        }
        if (obj.x < 0.0f)
        {
            return ScreenSide.Left;
        }
        else
        {
            throw new Exception("Contact is neither left or right of center. wat?");
        }
    }
}
