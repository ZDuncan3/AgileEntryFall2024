using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(NavMeshAgent))]
public class RangedSentry : MonoBehaviour
{
	[HideInInspector] public NPC npcInfo;

	[HideInInspector] public NavMeshAgent agent;
	/*[HideInInspector] */public GameObject target;
	public float distanceCheck = 3f;

	private Vector3 direction;
	private float baseTimeBetweenAttacks;

	private void Awake()
	{
		npcInfo = GetComponent<NPC>();
		agent = GetComponent<NavMeshAgent>();

		baseTimeBetweenAttacks = npcInfo.timeBetweenAttacks;
	}

	private void OnTriggerStay(Collider other)
	{
		if (target == null)
		{
			Player newTargetPlayer = other.GetComponent<Player>();
			
			if (newTargetPlayer != null)
			{
				target = newTargetPlayer.gameObject;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		target = null;
	}

	private void Start()
	{
		StartCoroutine(UpdateDestination());
	}

	private void Update()
	{
		if (target != null && Vector3.Distance(target.transform.position, transform.position) < 2f)
		{
			direction = (target.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			lookRotation.x = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

			if (npcInfo.animator != null)
			{
				if (npcInfo.hasFinishedAttacking)
				{
					npcInfo.Attack();
				}
			}
		}
		else if (target != null && Vector3.Distance(target.transform.position, transform.position) < distanceCheck)
		{
			direction = (target.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(-direction);
			lookRotation.x = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		}
		else if (target != null)
		{
			direction = (target.transform.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			lookRotation.x = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

			if (npcInfo.animator != null)
			{
				if (npcInfo.hasFinishedAttacking)
				{
					npcInfo.Attack();
				}
			}
		}
	}

	private IEnumerator UpdateDestination()
	{
		while (true)
		{
			if (target != null && Vector3.Distance(target.transform.position, transform.position) <= 2f)
			{
				npcInfo.timeBetweenAttacks -= 0.025f;
				agent.destination = transform.position;
				agent.isStopped = true;
			}
			else if (target != null && Vector3.Distance(target.transform.position, transform.position) <= distanceCheck)
			{
				npcInfo.timeBetweenAttacks = baseTimeBetweenAttacks;
				agent.destination = (transform.position - (direction * 10f));
				agent.isStopped = false;
			}
			else if (target != null && Vector3.Distance(target.transform.position, transform.position) > distanceCheck + (distanceCheck * 0.5f))
			{
				npcInfo.timeBetweenAttacks = baseTimeBetweenAttacks;
				agent.destination = target.transform.position;
				agent.isStopped = false;
			}
			else
			{
				npcInfo.timeBetweenAttacks = baseTimeBetweenAttacks;
				agent.destination = transform.position;
				agent.isStopped = true;
			}
			yield return new WaitForSeconds(0.15f);
		}
	}
}