using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;

    public static int BombsSpawned { private set; get; } = 0;

    private List<float> posibleAngles = new List<float>();

    public void StartSpawningBombs(int bombCount, float distanceFromPlayer, Transform playerTransform, float spawnDelay)
    {
        GetAllPosiblePositions(bombCount);
        StartCoroutine(SpawnBombs(bombCount, distanceFromPlayer, playerTransform, spawnDelay));
    }

    IEnumerator SpawnBombs(int bombCount, float distanceFromPlayer, Transform playerTransform, float spawnDelay)
    {
        GetAllPosiblePositions(bombCount);

        for (int i = 0; i < bombCount; i++)
        {
            SpawnBomb(CalculateCirclePosition(GetAnAngle() * i, distanceFromPlayer, playerTransform));
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector3 CalculateCirclePosition(float angle, float radius, Transform playerTransform)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = playerTransform.position.x + radius * Mathf.Cos(radians);
        float z = playerTransform.position.z + radius * Mathf.Sin(radians);
        return new Vector3(x, playerTransform.position.y, z);
    }

    private void SpawnBomb(Vector3 position)
    {
        Instantiate(bombPrefab, position, Quaternion.identity, transform);
        BombsSpawned++;
        //Debug.Log("Spawned a bomb! (" + BombsSpawned + ").");
    }

    private void GetAllPosiblePositions(int bombCount)
    {
        float anglesBetweenBombs = 360f / bombCount;

        for (int i = 0; i < bombCount; i++)
        {
            posibleAngles.Add(anglesBetweenBombs * i);
        }
    }

    private float GetAnAngle()
    {
        float angle = 0.0f;
        if (posibleAngles.Count > 1)
        {
            int index = Random.Range(0, posibleAngles.Count);
            angle = posibleAngles[index];
            posibleAngles.RemoveAt(index);
        }
        else
        {
            angle = posibleAngles[0];
            posibleAngles.Remove(0);
        }
        return angle;
    }

}
