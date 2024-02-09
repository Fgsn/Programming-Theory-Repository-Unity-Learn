using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public SpawnManager spawnManager;
    private void Start()
    {
        spawnManager.Init(player);
    }
}
