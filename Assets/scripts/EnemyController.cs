using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private int live = 10;
    [SerializeField] public int damage { get; private set; } = 5;

    private PlayerController playerController;

    public void Init(PlayerController player)
    {
        playerController = player;
    }

    private void Update()
    {
        Chase();
    }
    public void Chase()
    {
        Vector2 moveDirection = (playerController.transform.position - transform.position).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime); 
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("GG");
            live -= collision.gameObject.GetComponent<Weapon>().damage;
            if (live <= 0)
                Destroy(gameObject);
        }

    }

}
