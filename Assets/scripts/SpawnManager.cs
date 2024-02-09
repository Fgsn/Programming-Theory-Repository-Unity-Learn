using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<EnemyController> prefabs;
    [SerializeField] private float startDelay = 1.5f; // ENCAPSULATION
    [SerializeField] private float repeat = 2f; // ENCAPSULATION
    [SerializeField] private float xRange = 20; // ENCAPSULATION
    [SerializeField] private float yRange = 15; // ENCAPSULATION
    [SerializeField] private int pacKEnemyRange = 4; // ENCAPSULATION
    [SerializeField] private PlayerController playerController { get; set; } // ENCAPSULATION

    public void Init(PlayerController player)
    {
        playerController = player;
    }
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
        EnemyController currentEnemy = null;
        for( int i = 0; i < packCount;i++)
        {
            currentEnemy = Instantiate(prefabs[prefabIndex], new Vector2(x,y), prefabs[prefabIndex].transform.rotation);
            currentEnemy.Init(playerController);
        }
    }

}
