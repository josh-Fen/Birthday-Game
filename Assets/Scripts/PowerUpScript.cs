using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

	public int healthIncrease = 5;
	
	
	// Use this for initialization
	void Start () {
		
		Destroy (gameObject, 20);
	}
}
