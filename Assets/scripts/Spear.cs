using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    public override void Attack()
    {
        GameObject target = ChooseTarget();

        if (!target)
            return;

        Vector3 vectorToTarget = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
        StartCoroutine(AttackMove(target.transform.position));
        StartCoroutine(Cooldown(attackCooldown));

    }

    protected override IEnumerator AttackMove(Vector2 pos)
    {
        Vector2 currentPos = transform.position;
        Vector2 toPos = pos;
        float t = 0;
        coll.enabled = true;
        while (t <= 1)
        {
            t += Time.deltaTime * attackSpeed;
            transform.position = Vector2.Lerp(currentPos, toPos, t);
            yield return null;
        }
        coll.enabled = false;
        transform.localPosition = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }

}
