using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInfos : MonoBehaviour {

	public Text goldText;
	private float nivelExp;
	private float currentExp;

	[SerializeField]
	public Stat expBar;

	// Use this for initialization
	void Start () {
		goldText.text = PlayerPrefs.GetInt ("Gold").ToString ();
	}

	void Awake(){
		nivelExp = Mathf.Pow (PlayerPrefs.GetInt ("Nivel"), 2f);
		currentExp = PlayerPrefs.GetInt ("Exp");
		expBar.MaxValue = nivelExp+1200;
		expBar.CurrentValue = currentExp;
	}

	public void setExp(){
		expBar.CurrentValue = 00;
	}

}
