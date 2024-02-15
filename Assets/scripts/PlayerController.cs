using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private int maxLive = 20;
    [SerializeField] private int live;
    public static Action OnDied;
    public static Action<int, int> OnLiveChange;
    private bool alive = true;

    private void Start()
    {
        live = maxLive;
    }
    void Update()
    {
        if(alive)
            Move();
    }

    public void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2 (horizontalInput , verticalInput);

        transform.Translate(movement * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (live > collision.gameObject.GetComponent<EnemyController>().damage)
            {
                live -= collision.gameObject.GetComponent<EnemyController>().damage;
                OnLiveChange?.Invoke(maxLive, live);
            }
            else
            {
                live = 0;
                Debug.Log("GameOver");
                alive = false;
                OnDied?.Invoke();
            }
        }
    }

}
