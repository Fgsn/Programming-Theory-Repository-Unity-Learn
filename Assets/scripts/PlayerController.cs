using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private int live = 20;
    [SerializeField] private Weapon[] weapons;

    private void Start()
    {
    }
    void Update()
    {
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
            live -= collision.gameObject.GetComponent<EnemyController>().damage;
            if (live <= 0)
                Debug.Log("GameOver");
        }
    }

}
