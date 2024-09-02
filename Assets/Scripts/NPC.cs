using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Damageable
{
	public new string name;

	public bool isAlly = false;
	public bool isNeutral = true;
    public bool isEnemy = false;

	public bool hasFinishedAttacking = true;

	[HideInInspector] public Animator animator;

	public float attackRange = 1f; // short melee 1f, long melee 3f, short range 6f, long range 12f
	public float timeBetweenAttacks = 1f;
	public int attackDamage = 10;

	protected Coroutine attackingRoutine;

	private void Awake()
	{
		animator = GetComponent<Animator>();

		if (name == string.Empty)
		{
			name = gameObject.name;

			if (name == string.Empty)
			{
				name = "with GUID: " + GetComponent<GUIDObject>().ID;
			}
		}
	}

	public virtual void Attack()
	{
		if (attackingRoutine == null)
		{
			hasFinishedAttacking = false;
			animator.SetBool("isAttacking", true);
			attackingRoutine = StartCoroutine(AttackRoutine());
		}
		else
		{
			Debug.Log($"Character \"{name}\" is already attacking!");
		}
	}

	protected virtual IEnumerator AttackRoutine()
	{
		yield return new WaitForSeconds(0.05f);
		animator.SetBool("isAttacking", false);
		yield return new WaitForSeconds(timeBetweenAttacks);
		attackingRoutine = null;
		hasFinishedAttacking = true;
	}
}