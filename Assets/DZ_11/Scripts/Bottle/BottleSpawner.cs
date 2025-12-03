using System.Collections.Generic;
using UnityEngine;

namespace DZ_11
{
    public class BottleSpawner : MonoBehaviour
    {
        [SerializeField] private Bottle _bottlePrefab;

        [SerializeField] private float _cooldown;
        [SerializeField] private List<BottleSpawnPoint> _spawnPoints;

        private float _time;

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= _cooldown)
            {
                List<BottleSpawnPoint> emptyPoints = GetEmptyPoints();

                if (emptyPoints.Count == 0)
                {
                    _time = 0;
                    return;
                }

                BottleSpawnPoint point = GenerateEmptyPoint(emptyPoints);

                Spawn(point);
            }

        }

        private BottleSpawnPoint GenerateEmptyPoint(List<BottleSpawnPoint> emptyPoints) => emptyPoints[Random.Range(0, emptyPoints.Count)];

        private List<BottleSpawnPoint> GetEmptyPoints()
        {
            List<BottleSpawnPoint> emptyPoints = new List<BottleSpawnPoint>();

            foreach (BottleSpawnPoint point in _spawnPoints)
                if (point.IsEmpty == true)
                    emptyPoints.Add(point);

            return emptyPoints;
        }

        private void Spawn(BottleSpawnPoint point)
        {
            Bottle bottle = Instantiate(_bottlePrefab, point.transform.position, Quaternion.identity);
            point.Occupy(bottle);

            _time = 0;
        }
    }
}