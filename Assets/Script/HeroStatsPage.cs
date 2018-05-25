using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroStatsPage : MonoBehaviour {

	public Text str_txt;
	public Text agi_txt;
	public Text dex_txt;
	public Text def_txt;
	public Text luck_txt;
	public Text eng_txt;

	int str;
	int agi;
	int dex;
	int def;
	int luck;
	int eng;

	// Use this for initialization
	void Start () {
		str = (int) PlayerPrefs.GetFloat ("str");
		agi = (int) PlayerPrefs.GetFloat ("agi");
		dex = (int) PlayerPrefs.GetFloat ("dex");
		def = (int) PlayerPrefs.GetFloat ("def");
		luck = (int) PlayerPrefs.GetFloat ("luck");
		eng = (int) PlayerPrefs.GetFloat ("eng");

		refreshTexts ();
	}

	private void refreshTexts(){
		str_txt.text = str.ToString();
		agi_txt.text = agi.ToString();
		dex_txt.text = dex.ToString();
		def_txt.text = def.ToString();
		luck_txt.text = luck.ToString();
		eng_txt.text = eng.ToString();

	}

	private void refreshPrefs(){
		PlayerPrefs.SetFloat ("str", (float) str);
		PlayerPrefs.SetFloat ("def", (float) def);
		PlayerPrefs.SetFloat ("agi", (float) agi);
		PlayerPrefs.SetFloat ("dex", (float) dex);
		PlayerPrefs.SetFloat ("luck", (float) luck);
		PlayerPrefs.SetFloat ("eng", (float) eng);
	}

	public void incrementStat(string s){
		switch (s) {
		case "str":
			str++;
			break;
		case "agi":
			agi++;
			break;
		case "dex":
			dex++;
			break;
		case "def":
			def++;
			break;
		case "luck":
			luck++;
			break;
		case "eng":
			eng++;
			break;
		}
		refreshTexts ();
	}

	public void BackButton(){
		refreshPrefs ();
		SceneManager.LoadScene ("Map", LoadSceneMode.Single);
	}
		
}
