using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	public bool canTakeDamaged = true;
	public bool invulnerable = false;

	public bool canBeTargeted = true; // can be targeted by an AI

	[Range(0, 999999)] public int health;
	[Range(0, 999999)] public int maxHealth;

	public virtual void TakeDamage(int damageToDeal)
	{
		if (health - damageToDeal < 0)
		{
			health = 0;
		}
		else if (health - damageToDeal > maxHealth)
		{
			health = maxHealth;
		}
		else
		{
			health -= damageToDeal;
		}
	}

	protected virtual void FixedUpdate()
	{
		if (health > maxHealth)
		{
			health = maxHealth;
		}
		else if (health <= 0)
		{
			KillObject();
		}
	}

	protected virtual void KillObject()
	{
		Destroy(gameObject);
	}
}