using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleHunt : MonoBehaviour
{
    private NavMeshAgent _agent;

	[SerializeField] private GameObject _target;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_target = FindObjectOfType<Player>().gameObject;
	}

	private void Start()
	{
		StartCoroutine(UpdateDestination());
	}

	private IEnumerator UpdateDestination()
	{
		while (true)
		{
			if (_target != null)
			{
				_agent.destination = _target.transform.position;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
}
