using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflySpewer : NPC
{
	public GameObject projectilePrefab;

	public Transform spawnPosition;

	public override void Attack()
	{
		if (attackingRoutine == null)
		{
			hasFinishedAttacking = false;
			animator.SetBool("isAttacking", true);
			attackingRoutine = StartCoroutine(AttackRoutine());
			// spawn damaging projectile
			if (projectilePrefab != null)
			{
				StartCoroutine(SpawnProjectile());
			}
		}
		else
		{
			Debug.Log($"Character \"{name}\" is already attacking!");
		}
	}

	private IEnumerator SpawnProjectile()
	{
		GameObject projObj = Instantiate(projectilePrefab, spawnPosition.position, spawnPosition.rotation, null);
		Projectile proj = projObj.GetComponent<Projectile>();

		proj.npcOwner = this;
		proj.projectileDamage = attackDamage;

		yield return new WaitForEndOfFrame();

		projObj.GetComponent<SphereCollider>().enabled = true;
	}
}
