using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GT
{
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bombPrefab = null;
        [SerializeField] private BombConfiguration bombConfig = null;

        private List<float> posibleAngles = new List<float>();

        private List<Bomb> bombList = new List<Bomb>();

        public int BombsCreated { private set; get; } = 0;
        public int ActualBombs { get { return bombList.Count; } }
        public int DestroyedBombs { get { return BombsCreated - ActualBombs; } }
        public bool IsReady { private set; get; } = false;

        public System.Action<int> OnBombDestroy;
        public System.Action OnAllBombsDestroyed;

        public static BombSpawner Instance = null;

        private DataManager dataManager = null;

        private void Awake()
        {
            Instance = this;
            IsReady = false;
            BombsCreated = 0;
            dataManager = DataManager.Instance;
        }
        private void OnDestroy()
        {
            foreach (Bomb bomb in bombList) //Esto porque puede ser que no todas las bombas sean destruidas
            {
                bomb.OnGettingDestroyed -= AugmentDestroyedBombs;
            }
        }
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
            IsReady = true;
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
                GOB.SetBehavior(new BombJumper(GO, bombConfig));
                GO.name = "Jumper " + BombsCreated;
            }
            else
            {
                GOB.SetBehavior(new BombFollower(GO, playerTransform, bombConfig));
                GO.name = "Chaser " + BombsCreated;
            }
            GOB.OnGettingDestroyed += AugmentDestroyedBombs;
            bombList.Add(GOB);
            BombsCreated++;

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

        private void AugmentDestroyedBombs(Bomb thisBomb)
        {
            bombList.Remove(thisBomb);
            OnBombDestroy?.Invoke(bombList.Count);
            thisBomb.OnGettingDestroyed -= AugmentDestroyedBombs;
            if(bombList.Count==0)
            {
                OnAllBombsDestroyed?.Invoke();
            }
        }

    }
}