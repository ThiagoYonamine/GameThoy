using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopupText : MonoBehaviour {

	public Animator animator;
	private Text damage;
	// Use this for initialization
	void OnEnable () {
		AnimatorClipInfo[] clipinfo = animator.GetCurrentAnimatorClipInfo (0);
		Destroy(gameObject, clipinfo[0].clip.length);
		damage = animator.GetComponent<Text> ();
	}
	

	public void setText(string text, bool critical){
		if (text == "0")
			text = "Miss";
		damage.text = text;
		if (critical) {
			damage.fontSize = 48;
			damage.font = Resources.Load<Font>("Fonts/JFRocSol");
		}
	}
}
