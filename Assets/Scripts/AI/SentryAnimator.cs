using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sentry)), RequireComponent(typeof(Animator))]
public class SentryAnimator : MonoBehaviour
{
    private Sentry _sentryAI;

	private void Awake()
	{
		_sentryAI = GetComponent<Sentry>();
	}

	private void Update()
	{
        if (_sentryAI != null && _sentryAI.npcInfo.animator != null)
		{
			if (_sentryAI.target != null && _sentryAI.agent.remainingDistance > _sentryAI.agent.stoppingDistance)
			{
				_sentryAI.npcInfo.animator.SetBool("isWalking", true);
			}
			else
			{
				_sentryAI.npcInfo.animator.SetBool("isWalking", false);
			}
		}
	}
}