using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		PlayerPrefs.SetInt ("Reset", 0);
		if (PlayerPrefs.GetInt("Reset") == 1) {
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt ("Reset", 0);
			PlayerPrefs.SetInt ("Gold", 0);
			PlayerPrefs.SetFloat ("Exp", 0);
			PlayerPrefs.SetInt ("Nivel", 1);
			PlayerPrefs.SetInt ("BattleLevel", 1);
			PlayerPrefs.SetFloat ("str", 10);
			PlayerPrefs.SetFloat ("def", 10);
			PlayerPrefs.SetFloat ("agi", 10);
			PlayerPrefs.SetFloat ("dex", 10);
			PlayerPrefs.SetFloat ("luck", 10);
			PlayerPrefs.SetFloat ("eng", 10);
		}

		PlayerPrefs.Save ();
		Debug.Log (PlayerPrefs.GetInt ("Gold"));
	}


}
