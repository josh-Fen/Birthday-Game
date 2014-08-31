using UnityEngine;
using System.Collections;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
	//--------------------------------
	// 1 - Designer variables
	//--------------------------------
	
	/// <summary>
	/// Projectile prefab for shooting
	/// </summary>
	public Transform shotPrefab;
	
	/// <summary>
	/// Cooldown in seconds between two shots
	/// </summary>
	public float shootingRate = 0.25f;

	//MY VARIABLE --DELETE--
	public Vector2 projectileSpeed = new Vector2(5, 5);
	
	//--------------------------------
	// 2 - Cooldown
	//--------------------------------
	
	private float shootCooldown;
	
	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}
	
	//--------------------------------
	// 3 - Shooting from another script
	//--------------------------------
	
	/// <summary>
	/// Create a new projectile if possible
	/// </summary>
	/// MY CODE the reason I made it return a bool is to make sure the playerShotSound is played only
	/// when we actually shoot
	public bool Attack(bool isEnemy)
	{
		//MY CODE
		bool valueOfCanAttack = false;

		if (CanAttack)
		{
			//MY CODE
			valueOfCanAttack = true;
			shootCooldown = shootingRate;
			
			// Create a new shot
			var shotTransform = Instantiate(shotPrefab) as Transform;
			
			// Assign position
			shotTransform.position = transform.position;
			
			// The is enemy property
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();

			//THIS IS MY CODE --DELETE--
			MoveScript speedy = shotTransform.gameObject.GetComponent<MoveScript>();
			speedy.speed = projectileSpeed;

			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}
			
			// Make the weapon shot always towards it
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				move.direction = this.transform.right; // towards in 2D space is the right of the sprite
			}
		}
		return valueOfCanAttack;
	}
	
	/// <summary>
	/// Is the weapon ready to create a new projectile?
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
