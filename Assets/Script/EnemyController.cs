using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	private Stat health;

	public HeroController hero;
	private Animator anim;
	private int attackHash = Animator.StringToHash("Attack");
	private int hitHash = Animator.StringToHash("Hit");
	private int dieHash = Animator.StringToHash("Die");
	private float attackRate = 2f;
	private float nextAttack = 0.0f;

	private bool alive = true;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Awake(){
		health.Initialize ();	
		PopupTextController.Initialize ();
	}

	void FixedUpdate () {
		if (alive) {
			if (Time.time > nextAttack) {
				attack ();
			}
		}
	}

	void Update(){
		if (alive) {
			if (health.CurrentValue < 1)
				die ();
		}
	}

	void attack(){
			anim.Play (attackHash);
			nextAttack = Time.time + attackRate;
	}

	public void hit(int damage, bool critical){
		if (alive) {
			PopupTextController.createPopupText (damage.ToString(), new Vector2(transform.position.x+0.5f, transform.position.y+2), critical);
			anim.Play (hitHash);
			health.CurrentValue -= damage;
		}
	}

	//this method is called inside animation
	public void hitHero(){
		hero.hit ();
	}

	private void die(){
		alive = false;
		anim.Play (dieHash);
	}

}
