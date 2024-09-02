using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
	public string itemName;
	public TMP_Text pickupText;
	private float colorA = 1f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			pickupText.gameObject.SetActive(true);
			pickupText.color = new Color(1f, 1f, 1f, 1f);
			colorA = 1f;
			pickupText.text = $"\"{itemName}\" picked up!";
			StartCoroutine(RemoveText());
			PlayerController.instance.keys.Add(itemName);
			GetComponent<MeshRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
		}
	}

	private IEnumerator RemoveText()
	{
		yield return new WaitForSeconds(2f);

		while (colorA > 0)
		{
			colorA -= 0.02f;
			pickupText.color = new Color(1f, 1f, 1f, colorA);

			yield return new WaitForEndOfFrame();
		}

		pickupText.text = "";
		pickupText.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
}
