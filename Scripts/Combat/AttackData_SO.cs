using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public float attackRange; //near end attack
    public float skillRange; //rear end attack
    public float coolDown;

    public int minDamage;
    public int maxDamage;

    public float criticalMultiplier;
    public float criticalChance;
}
