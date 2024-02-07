using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public  abstract class Weapon : MonoBehaviour
{
    [SerializeField] public int damage ;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] public bool isReady { get; protected set; } = true;
    protected BoxCollider2D coll { get; set; }
    protected CircleCollider2D field { get; set; }

    protected List<Collider2D> enemysOnRange = new List<Collider2D>();

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        field = GetComponent<CircleCollider2D>();
        field.radius = attackRange;
        coll.enabled = false;
    }

    private void Update()
    {
        if (isReady)
        {
            isReady = false;
            Attack();
            StartCoroutine(Cooldown(attackCooldown));
        }
    }
    public virtual void Attack()
    {
        GameObject target = ChooseTarget();

        if (!target)
            return;
        StartCoroutine(AttackMove(target.transform.position) );
    }


    protected virtual IEnumerator AttackMove(Vector2 pos)
    {
        yield return null;
    }
    protected IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();

        field.OverlapCollider(filter, enemysOnRange);
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        ContactFilter2D filter = new ContactFilter2D().NoFilter();

        field.OverlapCollider(filter, enemysOnRange);

    }
    protected GameObject ChooseTarget()
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