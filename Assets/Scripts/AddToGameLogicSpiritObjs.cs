using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToGameLogicSpiritObjs : MonoBehaviour
{
    [SerializeField] private bool _disabledBySpiritDim = false;

	private void Awake()
	{
		if (_disabledBySpiritDim)
		{
			GameLogic.instance.objsDisabledInSpiritDim.Add(gameObject);
			if (GameLogic.instance.isInSpiritDimension)
				gameObject.SetActive(false);
		}
		else
		{
			GameLogic.instance.objsEnabledInSpiritDim.Add(gameObject);
			if (!GameLogic.instance.isInSpiritDimension)
				gameObject.SetActive(false);
		}
	}
}
