using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{

	//code for the GUI Skin
	private GUISkin skin;

	void Start()
	{
		skin = Resources.Load ("GUISkin") as GUISkin;
	}
	void OnGUI()
	{
		const int buttonWidth = 300;
		const int buttonHeight = 80;

		//using GUISkin skin
		GUI.skin = skin;
		
		// Draw a button to start the game
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3),
			buttonWidth,
			buttonHeight
			),
			"Blake's Birthday Game \n Start!"
			)
			)
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("Stage1");
		}
	}
}
