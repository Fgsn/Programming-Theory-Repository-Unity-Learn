using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public  abstract class Weapon : MonoBehaviour // ABSTRACTION
{
    [SerializeField] public int damage ; 
    [SerializeField] protected float attackCooldown; // ENCAPSULATION
    [SerializeField] protected float attackSpeed; // ENCAPSULATION
    [SerializeField] protected float attackRange; // ENCAPSULATION
    [SerializeField] public bool isReady { get; protected set; } = true; // ENCAPSULATION
    protected BoxCollider2D coll { get; set; } // ENCAPSULATION
    protected CircleCollider2D field { get; set; } // ENCAPSULATION

    protected List<Collider2D> enemysOnRange = new List<Collider2D>(); // ENCAPSULATION

    private void Awake() // ABSTRACTION
    {
        coll = GetComponent<BoxCollider2D>();
        field = GetComponent<CircleCollider2D>();
        field.radius = attackRange;
        coll.enabled = false;
    }

    private void Update() // ABSTRACTION
    {
        if (isReady)
        {
            isReady = false;
            Attack();
            StartCoroutine(Cooldown(attackCooldown));
        }
    }
    public virtual void Attack() // ABSTRACTION
    {
        GameObject target = ChooseTarget();

        if (!target)
            return;
        StartCoroutine(AttackMove(target.transform.position) );
    }


    protected virtual IEnumerator AttackMove(Vector2 pos) // ABSTRACTION
    {
        yield return null;
    }
    protected IEnumerator Cooldown(float cooldown) // ABSTRACTION
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision) // ABSTRACTION
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();

        field.OverlapCollider(filter, enemysOnRange);
    }
    protected void OnTriggerExit2D(Collider2D collision) // ABSTRACTION
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();

        field.OverlapCollider(filter, enemysOnRange);

    }
    protected GameObject ChooseTarget() // ABSTRACTION ENCAPSULATION
    {
        float minDistance = float.MaxValue;
        GameObject target = null;
        foreach(var enemy in enemysOnRange)
        {
            if (!enemy.CompareTag("Enemy"))
                continue;
            if (Vector2.Distance(transform.position, enemy.transform.position) < minDistance)
            {
                target = enemy.gameObject;
                minDistance = Vector2.Distance(transform.position, enemy.transform.position);
            }
        }
        return target;
    }

}