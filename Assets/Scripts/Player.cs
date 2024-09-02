using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : Damageable
{
	public PostProcessVolume damageVolume;

	public GameObject loseScreen;

	public override void TakeDamage(int damageToDeal)
	{
		// post process vignette
		//damageVolume.weight = 0;
		base.TakeDamage(damageToDeal);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (health <= 0)
		{
			LoseGame();
		}
	}

	private void LoseGame()
	{
		Time.timeScale = 0f;
		loseScreen.SetActive(true);

		SaveLoad.DeleteSave("zduncan3AgileEntry");
	}
}