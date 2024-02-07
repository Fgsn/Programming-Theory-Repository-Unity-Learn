using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemyController> prefabs;
    [SerializeField] private float startDelay = 1.5f;
    [SerializeField] private float repeat = 2f;
    [SerializeField] private float xRange = 20;
    [SerializeField] private float yRange = 15;
    [SerializeField] private int pacKEnemyRange = 4;
    private void Start()
    {
        InvokeRepeating("SpawnEnemyWave", startDelay, repeat);
    }

    private void SpawnEnemyWave()
    {
        float x = Random.Range(-xRange, xRange);
        float y = Random.Range(-yRange, yRange);
        int packCount = Random.Range(0, pacKEnemyRange);
        int prefabIndex = Random.Range(0, prefabs.Count);
        for( int i = 0; i < packCount;i++)
        {
            Instantiate(prefabs[prefabIndex], new Vector2(x,y), prefabs[prefabIndex].transform.rotation);
        }
    }

}
