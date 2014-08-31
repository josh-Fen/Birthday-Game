using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of particles from code with no effort
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
	/// <summary>
	/// Singleton
	/// </summary>
	public static SpecialEffectsHelper Instance;
	
	public ParticleSystem smokeEffect;
	public ParticleSystem fireEffect;
	public ParticleSystem powerUpEffect;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SpecialEffectsHelper!");
		}
		
		Instance = this;
	}
	
	/// <summary>
	/// Create an explosion at the given location
	/// </summary>
	/// <param name="position"></param>
	public void Explosion(Vector3 position)
	{
		// Smoke on the water
		instantiate(smokeEffect, position);
		
		// Tu tu tu, tu tu tudu
		
		// Fire in the sky
		instantiate(fireEffect, position);
	}

	// particle effect for power up
	public void PowerUpSizzle(Vector3 position)
	{
		//particle effect
		instantiate (powerUpEffect, position);
	}
	
	/// <summary>
	/// Instantiate a Particle system from prefab
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate(
			prefab,
			position,
			Quaternion.identity
			) as ParticleSystem;
		
		// Make sure it will be destroyed
		Destroy(
			newParticleSystem.gameObject,
			newParticleSystem.startLifetime
			);
		
		return newParticleSystem;
	}
}
