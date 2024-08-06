using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Damageable
{
	public bool isAlly = false;
	public bool isNeutral = true;
    public bool isEnemy = false;

	[HideInInspector] public Animator animator;

	public void Attack()
	{

	}
}
