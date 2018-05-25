using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {

	public Text result;
	public Text gold_txt;
	public Text exp_txt;

	private float  gold;
	private float  exp;
	private float  total_gold;
	private float  total_exp;

	private Animator anim;
	private int dropHash = Animator.StringToHash("Drop");

	void Start () {
		anim = GetComponent<Animator> ();
		float levelMultiplier = Mathf.Pow (LevelController.currentLevel, 3);
		total_gold = levelMultiplier * 123;
		total_exp = levelMultiplier * 95;

	}
	// Update is called once per frame
	void Update () {
		HandleValues ();	
	}

	private void HandleValues(){
		if (gold != total_gold) {
			gold = Mathf.Lerp(gold, total_gold, Time.deltaTime*3);
			gold_txt.text = Mathf.RoundToInt(gold).ToString();
		}

		if (exp != total_exp) {
			exp = Mathf.Lerp(exp, total_exp, Time.deltaTime*3);
			exp_txt.text = Mathf.RoundToInt(exp).ToString();
		}
	}


	public void drop(string s){
		if (s == "Defeat") {
			result.color = Color.red;
			total_gold *= 0.01f;
			total_exp *= 0.01f;
		} 
		else {
			PlayerPrefs.SetInt ("BattleLevel", LevelController.currentLevel+1);
		}
		result.text = s;
		anim.Play (dropHash);
	}

	//Called in menu Animation. This is needded because i want a lerp animation. tdo, is there better way?
	public void setGold(){
		gold = 0;
		int playerGold = PlayerPrefs.GetInt("Gold");
		playerGold += (int) total_gold;
		PlayerPrefs.SetInt ("Gold", playerGold);
	}

	//Called in menu Animation. This is needded because i want a lerp animation. tdo, is there better way?
	public void setExp(){
		exp = 0;
		int playerExp = PlayerPrefs.GetInt("Exp");
		playerExp += (int) total_exp;
		PlayerPrefs.SetInt ("Exp", playerExp);
	}

	public void backToArena(){
		SceneManager.LoadScene ("Arena", LoadSceneMode.Single);
	}
}
