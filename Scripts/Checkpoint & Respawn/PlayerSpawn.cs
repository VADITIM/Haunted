using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
    private Vector3 spawnPoint;
    private bool spawnPointSet = false;

    void Start()
    {
        // Initialize the spawn poin to players postion
        if (!spawnPointSet)
        {
            spawnPoint = transform.position;
            spawnPointSet = true;
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
        spawnPointSet = true;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnAfterFrame());
    }

    private IEnumerator RespawnAfterFrame()
    {
        yield return new WaitForEndOfFrame();  // Wait until the end of the current frame to avoid conflicts

        transform.position = spawnPoint;
    }
}
