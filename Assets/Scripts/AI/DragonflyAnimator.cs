using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DragonflyAnimator : MonoBehaviour
{
	public Sentry _sentryAI;
	public RangedSentry _rangedSentryAI;
	// add more AI types, then only attach the one you want to use

	private void Update()
	{
		if (_sentryAI != null)
		{
			if (_sentryAI.npcInfo.animator != null)
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
		else if (_rangedSentryAI != null)
		{
			if (_rangedSentryAI.npcInfo.animator != null)
			{
				if (_rangedSentryAI.target != null && _rangedSentryAI.agent.remainingDistance > _rangedSentryAI.agent.stoppingDistance)
				{
					_rangedSentryAI.npcInfo.animator.SetBool("isWalking", true);
				}
				else
				{
					_rangedSentryAI.npcInfo.animator.SetBool("isWalking", false);
				}
			}
		}
	}
}