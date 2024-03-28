using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waveInfoList;
    [SerializeField] private Transform[] waveSpawnPoint;

    private ObjectPoolingManager poolingManager;

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    private void Start()
    {
        StartCoroutine(ESpawnWave());
    }

    private IEnumerator ESpawnWave()
    {
        foreach (WaveData waveinfo in waveInfoList)
        {
            yield return new WaitForSeconds(waveinfo.TimeBetweenWave);

            foreach (WaveObjectData waveObject in waveinfo.WaveInfoList)
            {
                for (int i = 0; i < waveObject.Amount; i++)
                {
                    yield return new WaitForSeconds(waveObject.TimeBetweenSpawn);

                    int randomIndex = Random.Range(0, waveSpawnPoint.Length);
                    Vector3 spawnPosition = waveSpawnPoint[randomIndex].position;
                    PoolableObject newObject = poolingManager.GetPoolable(waveObject.Prefab);
                    newObject.transform.position = spawnPosition;
                }
            }
        }
    }

}
