using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextController : MonoBehaviour {
	private static PopupText popupText;
	private static GameObject canvas;


	public static void Initialize(){
		canvas = GameObject.Find ("Canvas");
		popupText =	Resources.Load<PopupText> ("Prefabs/popUpParent");
	}

	public static void createPopupText(string text, Vector2 position, bool critical){
		PopupText instance = Instantiate (popupText);
		Vector2 screenPosition = new Vector2(position.x + Random.Range(-.1f,.5f), position.y);
		instance.transform.SetParent (canvas.transform, false);
		instance.transform.position = screenPosition;
		instance.setText (text,critical);
	
	}
}
