using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int hp = 2;

	public bool isEnemy = true;

	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform powerUpPrefab;
	
	void OnTriggerEnter2D (Collider2D collider) {

		ShotScript shot = collider.gameObject.GetComponent<ShotScript> ();
		PowerUpScript powerUp = collider.gameObject.GetComponent<PowerUpScript> ();

		if (shot != null)
		{
			if (shot.isEnemyShot != isEnemy)
			{
				hp -= shot.damage;

				Destroy(shot.gameObject);

				if(hp <= 0)
				{
					SpecialEffectsHelper.Instance.Explosion(transform.position);
					SoundEffectsHelper.Instance.MakeExplosionSound();
					Vector3 positionOfEnemy = transform.position;
					Destroy(gameObject);
					//MY CODE this is to check and see if an enemy was killed and if
					//a powerUp should be given
					if (isEnemy)
					{
						int powerUpDetermine = Random.Range(0, 10);
						if (powerUpDetermine == 3 && powerUpPrefab)
						{
							makePowerUp(positionOfEnemy);
						}
					}
				}
			}
			//MY CODE checking for powerUp
		} else if (powerUp != null) {
			if (!isEnemy) {
				hp += powerUp.healthIncrease;
				Destroy(powerUp.gameObject);
				SpecialEffectsHelper.Instance.PowerUpSizzle (transform.position);
			}
		}
	}

	//This method is used to make a powerUp if it needs to
	void makePowerUp (Vector3 positionOfEnemy) {
		// Create a new powerUp
		var powerUpTransform = Instantiate(powerUpPrefab) as Transform;
		// Assign position
		powerUpTransform.position = positionOfEnemy;
	}
}
