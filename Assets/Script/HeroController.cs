using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour {

	[SerializeField]
	public Stat health;

	[SerializeField]
	public Stat energy;

	public EnemyController enemy;
	public Menu menu;

	public Button attack1_btn;
	public Button attack2_btn;

	private Animator anim;
	private int attackHash = Animator.StringToHash("Attack");
	private int attack2Hash = Animator.StringToHash("Attack2");
	private int hitHash = Animator.StringToHash("Hit");
	private int dieHash  = Animator.StringToHash("Die");

	private float attackRate;
	private float nextAttack1 = 0.0f;
	private float nextAttack2 = 0.0f;

	private bool alive = true;
	private Attribute attribute;

	void Start () {
		anim = GetComponent<Animator> ();
		attack1_btn.onClick.AddListener (attack1);
		attack2_btn.onClick.AddListener (attack2);

	}

	void Awake(){
		PopupTextController.Initialize ();
		attribute = new Attribute ();
		attribute.setStr (PlayerPrefs.GetFloat("str"));
		attribute.setDef (PlayerPrefs.GetFloat("def"));
		attribute.setAgi (PlayerPrefs.GetFloat("agi"));
		attribute.setDex (PlayerPrefs.GetFloat("dex"));
		attribute.setLuck (PlayerPrefs.GetFloat("luck"));
		attribute.setEnergy (PlayerPrefs.GetFloat("energy"));
		health.MaxValue = attribute.getLife();
		health.CurrentValue = health.MaxValue;
		energy.MaxValue = attribute.getTotalEnergy ();
		energy.CurrentValue = energy.MaxValue;
		attackRate = attribute.getDelay();
	}

	void FixedUpdate(){
		if (alive) {
			energy.CurrentValue += attribute.getRecovery();
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
		if (Time.time > nextAttack1 && energy.CurrentValue >= 20) {
			attack1_btn.interactable = true;
		} else {
			attack1_btn.interactable = false;
		}
		if (Time.time > nextAttack2 && energy.CurrentValue >= 30) {
			attack2_btn.interactable = true;
		} else {
			attack2_btn.interactable = false;
		}
	}

	public void attack1(){
		anim.Play (attackHash);
		energy.CurrentValue -= 20;
		nextAttack1 = Time.time + attackRate;
		//attack1_btn.interactable = false;
	}

	public void attack2(){
		anim.Play (attack2Hash);
		energy.CurrentValue -= 30;
		nextAttack2 = Time.time + attackRate*1.5f;
		//attack2_btn.interactable = false;
	}

	//when player is attacked
	public void hit(int damage, bool critical, float dex){
		if (alive) {
			int totalDamage = attribute.getHit (damage, dex);
			PopupTextController.createPopupText (totalDamage.ToString(), new Vector2(transform.position.x,transform.position.y+3), critical);
			if (totalDamage > 0) {
				anim.Play (hitHash);
			}
			health.CurrentValue -= totalDamage;
		}
	}
		
	private void die(){
		alive = false;
		attack1_btn.interactable = false;
		attack2_btn.interactable = false;
		anim.Play (dieHash);
	}

	//when player attack the enemy
	public void hitEnemy(float power){
		//attack is passed in animation attack
		bool critical = attribute.getCritical ();
		enemy.hit (attribute.getDamage (critical, power), critical, attribute.getDex());
	}

	public void dropMenu(){
		menu.drop ("Defeat");
	}

		
}