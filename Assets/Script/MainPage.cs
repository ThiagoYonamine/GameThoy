using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPage : MonoBehaviour {

	public void selectScene(string name){
		SceneManager.LoadScene (name, LoadSceneMode.Single);
	}
}
