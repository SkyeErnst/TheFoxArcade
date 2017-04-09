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
                break;
            case ParticleType.BallLost:
                break;
            case ParticleType.PaddleHit:
                break;
            case ParticleType.WallHit:
                newGO = Instantiate(WallHitParticlePrefab, spawnPoint, Quaternion.identity);
                newRot = Quaternion.LookRotation(normal);
                newGO.transform.rotation = newRot;
                break;

        }
    }
}
