using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WaveData> waveInfoList;
    [SerializeField] private Transform[] waveSpawnPoint;

    private ObjectPoolingManager poolManager;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }


    private void Start()
    {
        poolManager = ServiceLocator.GetService<ObjectPoolingManager>();
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
                    PoolableObject newObject = poolManager.GetPoolable(waveObject.Prefab);
                    newObject.transform.position = spawnPosition;
                }
            }
        }
    }

}
