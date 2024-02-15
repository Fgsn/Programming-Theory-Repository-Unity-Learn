using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sword : Weapon // INHERITANCE
{
    [SerializeField] private readonly float attackAngle = 120; // ENCAPSULATION

    public override void Attack() // POLYMORPHISM
    {
        GameObject target = ChooseTarget();

        if (!target)
            return;

        transform.LookAt(target.transform);

        Vector3 vectorToTarget = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
        StartCoroutine(AttackMove(target.transform.position));
        StartCoroutine(Cooldown(attackCooldown));

    }

    protected override IEnumerator AttackMove(Vector2 pos) // POLYMORPHISM
    {
        Vector2 currentPos = pos;
        float z = 1;
        if (transform.rotation.eulerAngles.z < 180)
            z = -1;
        float x = 0;
        float y = 0;
        float r = Vector2.Distance(transform.parent.position, pos);
        transform.Rotate(0, 0, z * attackAngle/2);
        x = r * Mathf.Cos( (transform.rotation.eulerAngles.z + 90f) * Mathf.PI / 180) + transform.parent.position.x;
        y = r * Mathf.Sin( (transform.rotation.eulerAngles.z + 90f) * Mathf.PI / 180) + transform.parent.position.y;
        transform.position = new Vector2(x, y);
        float t = 0;
        coll.enabled = true;
        while (t <= 1)
        {
            t += Time.deltaTime * attackSpeed;
            transform.Rotate(0, 0, z * -attackAngle * Time.deltaTime * attackSpeed);
            x = r * Mathf.Cos((transform.rotation.eulerAngles.z + 90f) * Mathf.PI / 180) + transform.parent.position.x;
            y = r * Mathf.Sin((transform.rotation.eulerAngles.z + 90f) * Mathf.PI / 180) + transform.parent.position.y;
            currentPos = new Vector2(x, y);
            transform.position = currentPos;
            yield return null;
        }
        coll.enabled = false;
        transform.localPosition = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }
}
