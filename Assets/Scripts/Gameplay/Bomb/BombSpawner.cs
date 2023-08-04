using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bombPrefab;

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
                Vector3 circlePos = CalculateCirclePosition(GetAnAngle(), distanceFromPlayer, playerTransform);
                SpawnBomb(circlePos, playerTransform);
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

        private void SpawnBomb(Vector3 position, Transform playerTransform)
        {
            GameObject GO;
            GO = Instantiate(bombPrefab, position, Quaternion.identity, transform);
            Bomb GOB = GO.GetComponent<Bomb>();
            int random = Random.Range(0, 9);

            if (random % 2 == 0)
            {
                GOB.SetBehavior(new BombJumper(GO));
                GO.name = "Jumper " + Bomb.BombCount;
            }
            else
            {
                GOB.SetBehavior(new BombFollower(GO, playerTransform));
                GO.name = "Chaser " + Bomb.BombCount;
            }
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
}