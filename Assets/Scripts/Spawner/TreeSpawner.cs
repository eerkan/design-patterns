using System.Collections;
using Strategy;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Spawner
{
    public class TreeSpawner : MonoBehaviour
    {
        [SerializeField] private float OrbitRadius;
        [SerializeField] private float SpawnRadius;
        [SerializeField] private float OrbitPeriodSeconds;
        [SerializeField] private float SpawnPeriodMilliseconds;
        [SerializeField] private uint SpawnCountPerPeriod;
        private Vector3 _startPosition;
        
        public ITreeSpawnStrategy TreeSpawnStrategy;

        private void Start()
        {
            _startPosition = transform.position;
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            var wait = new WaitForSeconds(SpawnPeriodMilliseconds * 1e-3f);
            while (true)
            {
                yield return wait;
                for(var i = 0; i < SpawnCountPerPeriod; i++)
                    SpawnTree(SpawnRadius);
            }
        }

        private Vector3 GetSpawnTreePosition(float relativeSpawnRadius)
        {
            var treePosition = transform.position + relativeSpawnRadius * Random.insideUnitSphere;
            treePosition.y = 0f;
            return treePosition;
        }

        private void SpawnTree(float relativeSpawnRadius)
        {
            TreeSpawnStrategy.Spawn().transform.position = GetSpawnTreePosition(relativeSpawnRadius);
        }

        private void Update()
        {
            transform.position = _startPosition + CircularMovementXZ(OrbitRadius, 1f / OrbitPeriodSeconds, 0f, Time.time);
        }
        
        private Vector3 CircularMovementXZ(float radius, float frequency, float phase, float time)
        {
            var radians = 2f * Mathf.PI * frequency * time + phase;
            return radius * new Vector3(Mathf.Cos(radians), 0f, Mathf.Sin(radians));
        }
    }
}