using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	private float currentLifetime = 0f;
    public float maxLifetime = 3f;

	public float projectileSpeed = 300f;

	[HideInInspector] public int projectileDamage = 5; // should be set upon being spawned by the thing that spawned it
	[HideInInspector] public NPC npcOwner;
	[HideInInspector] public Player playerOwner;

	private Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_rb.velocity = Vector3.zero;
		_rb.AddForce(transform.forward * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
	}

	private void FixedUpdate()
	{
		_rb.AddForce(transform.forward * projectileSpeed * 0.025f * Time.deltaTime, ForceMode.Impulse);
	}

	private void OnTriggerEnter(Collider other)
	{
		Player player = other.GetComponent<Player>();
		NPC npc = other.GetComponent<NPC>();
		Projectile proj = other.GetComponent<Projectile>();

		if (player != null && playerOwner == player)
		{
			return;
		}
		else if (npc != null)
		{
			return;
		}
		else if (proj != null)
		{
			return;
		}
		else if (player != null && playerOwner != player)
		{
			player.TakeDamage(projectileDamage);
			Debug.Log("Destroyed projectile due to collision with Player");
			Destroy(gameObject);
		}
		/*else if (npc != null && npcOwner != npc)
		{
			npc.TakeDamage(projectileDamage);
			Debug.Log($"Destroyed projectile due to collision with NPC: {npc.name}");
			Destroy(gameObject);
		}*/
		else
		{
			Debug.Log($"Destroyed projectile due to collision with object: {other.name}");
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if (currentLifetime >= maxLifetime)
		{
			Destroy(this.gameObject);
			Debug.Log("Destroyed projectile due to time violation");
			return;
		}

		currentLifetime += Time.deltaTime;
	}
}
