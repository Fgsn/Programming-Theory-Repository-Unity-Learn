using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject objectPool;
    [SerializeField] public int enemyPoolCount = 10;
    public List<EnemyController> prefabs;
    [SerializeField] private float startDelay = 1.5f; // ENCAPSULATION
    [SerializeField] private float repeat = 2f; // ENCAPSULATION
    [SerializeField] private float xRange = 20; // ENCAPSULATION
    [SerializeField] private float yRange = 15; // ENCAPSULATION
    [SerializeField] private int pacKEnemyRange = 4; // ENCAPSULATION
    [SerializeField] private PlayerController playerController { get; set; } // ENCAPSULATION

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        PlayerController.OnDied += GameOver;
        EnemyController current = null;
        for (int i = 0; i < enemyPoolCount; i++)
        {
            for (int j = 0; j < prefabs.Count; j++)
            {
                current = Instantiate(prefabs[j], prefabs[j].transform.position, prefabs[j].transform.rotation, objectPool.transform);
                current.gameObject.SetActive(false);
                current.Init(playerController);
            }
        }

    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemyWave", startDelay, repeat);
    }

    private void OnDestroy()
    {
        PlayerController.OnDied -= GameOver;
    }
    private void SpawnEnemyWave()
    {
        if (GameManager.instance.gameIsOver)
            return;
        int packCount = Random.Range(0, pacKEnemyRange) + 1;
        for( int i = 0; i < packCount;i++)
        {
            foreach (Transform child in objectPool.transform)
            {
                if (child.gameObject.activeSelf)
                    continue;
                int randomer = enemyPoolCount * prefabs.Count + packCount;
                if (Random.Range(0, randomer) > packCount)
                {
                    randomer--;
                    continue;
                }
                float x = Random.Range(-xRange, xRange);
                float y = Random.Range(-yRange, yRange);

                child.position = new Vector3(x,y, child.position.z);
                child.gameObject.SetActive(true);
                break;
            }
            //Instantiate(prefabs[prefabIndex], new Vector2(x,y), prefabs[prefabIndex].transform.rotation);
        }
    }

    private void GameOver()
    {
        GameManager.instance.GameOver();
    }

}
