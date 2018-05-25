using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour {

	public static int currentLevel = 1;
	public Sprite close;
	public int doorNumber;

	private bool closed(){
		return PlayerPrefs.GetInt("BattleLevel") < doorNumber;
	}

	void Start(){
			if(closed())
			this.GetComponent<Image> ().sprite = close;
	}


	public void selectButton(){
		if(!closed()){
			currentLevel = doorNumber;
			SceneManager.LoadScene ("Battle", LoadSceneMode.Single);
		}
	}
		
}
