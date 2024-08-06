using UnityEngine.Rendering.PostProcessing;

public class Player : Damageable
{
	public PostProcessVolume damageVolume;

	public override void TakeDamage(int damageToDeal)
	{
		// post process vignette
		damageVolume.weight = 0;
		base.TakeDamage(damageToDeal);
	}
}