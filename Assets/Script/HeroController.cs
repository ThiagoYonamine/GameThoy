using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroController : MonoBehaviour {

	[SerializeField]
	private Stat health;

	[SerializeField]
	private Stat energy;

	public EnemyController enemy;

	public Button attack1_btn;
	public Button attack2_btn;

	private Animator anim;
	private int attackHash = Animator.StringToHash("Attack");
	private int attack2Hash = Animator.StringToHash("Attack2");
	private int hitHash = Animator.StringToHash("Hit");
	private int dieHash  = Animator.StringToHash("Die");

	private float attackRate = 2f;
	private float nextAttack1 = 0.0f;
	private float nextAttack2 = 0.0f;

	//hero attributes
	private bool alive = true;
	private float str = 10; // strenght
	private float luck = 50; 

	private Attribute attribute;

	void Start () {
		anim = GetComponent<Animator> ();
		attack1_btn.onClick.AddListener (attack1);
		attack2_btn.onClick.AddListener (attack2);

	}

	void Awake(){
		health.Initialize ();	
		energy.Initialize ();
		PopupTextController.Initialize ();
		attribute = new Attribute ();
		attribute.setStr (10);
		attribute.setDef (15);
		attribute.setAgi (55);
		attribute.setDex (10);
		attribute.setLuck (10);
	}

	void FixedUpdate(){
		if (alive) {
			energy.CurrentValue += 0.1f;
		}
	}
		
	void Update(){
		if (alive) {
			setInteractables ();
			if (health.CurrentValue < 1)
				die ();
		}
	}

	private void setInteractables(){
		if (Time.time > nextAttack1 && energy.CurrentValue >= 20 ) {
			attack1_btn.interactable = true;
		}
		if (Time.time > nextAttack2 && energy.CurrentValue >= 30) {
			attack2_btn.interactable = true;
		}
	}

	public void attack1(){
		anim.Play (attackHash);
		energy.CurrentValue -= 20;
		nextAttack1 = Time.time + attackRate;
		attack1_btn.interactable = false;
	}

	public void attack2(){
		anim.Play (attack2Hash);
		energy.CurrentValue -= 30;
		nextAttack2 = Time.time + attackRate;
		attack2_btn.interactable = false;
	}

	public void hit(){
		if (alive) {
			int hitDamage = attribute.getHit (10);
			PopupTextController.createPopupText (hitDamage.ToString(), new Vector2(transform.position.x,transform.position.y+3), false);
			if (hitDamage > 0) {
				anim.Play (hitHash);
			}
			health.CurrentValue -= hitDamage;
		}
	}
		
	private void die(){
		alive = false;
		attack1_btn.interactable = false;
		attack2_btn.interactable = false;
		anim.Play (dieHash);
	}

	public void hitEnemy(float power){
		//attack is passed in animation attack
		bool critical = attribute.getCritical ();
		enemy.hit (attribute.getDamage(critical, power),critical);
	}


		
}