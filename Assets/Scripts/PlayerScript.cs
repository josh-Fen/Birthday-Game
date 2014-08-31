﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Vector2 speed = new Vector2(50, 50);	
	private Vector3 movement;

	// Update is called once per frame
	void Update () {

		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		movement = new Vector3 (speed.x * inputX, speed.y * inputY, 0);

		bool shoot = Input.GetButtonDown ("Fire1");
		shoot |= Input.GetButtonDown ("Fire2");

		if (shoot)
		{
			WeaponScript weapon = GetComponent<WeaponScript>();
			if(weapon != null)
			{
				bool valueOfCantAttack = weapon.Attack(false);
				if (valueOfCantAttack == true)
					SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
		
		//movement *= Time.deltaTime;
		//transform.Translate (movement);
		//you can remove the fixed update portion if the above is active
		//HOWEVER the pixelnest site says transform.Translate could cause physics problems
			
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D collision){
				bool damagePlayer = false;

				//Collision with enemy
				EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript> ();

				if (enemy != null) {
						//Kill enemy
						HealthScript enemyHealth = enemy.GetComponent<HealthScript> ();
						if (enemyHealth != null) {
							SpecialEffectsHelper.Instance.Explosion(enemy.transform.position);
							SoundEffectsHelper.Instance.MakeExplosionSound();
							Destroy(collision.gameObject) ;
						}

						damagePlayer = true;	
				}

				//Damage player
				if (damagePlayer) {
						HealthScript playerHealth = this.GetComponent<HealthScript> ();
						if (playerHealth != null) {
							playerHealth.hp -= 1;
						}
				}
		}

	void OnDestroy()
	{
		transform.parent.gameObject.AddComponent<GameOverScript>();

	}
}
