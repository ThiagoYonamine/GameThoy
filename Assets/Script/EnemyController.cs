using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField]
	public Stat health;

	public HeroController hero;
	public Menu menu;

	private Animator anim;
	private int attackHash = Animator.StringToHash("Attack");
	private int hitHash = Animator.StringToHash("Hit");
	private int dieHash = Animator.StringToHash("Die");
	private float attackRate;
	private float nextAttack = 6f;

	private bool alive = true;
	private Attribute attribute;

	void Start () {
		anim = GetComponent<Animator> ();
		string enemy = "Enemy/enemy" + LevelController.currentLevel;
		anim.runtimeAnimatorController = Resources.Load (enemy) as RuntimeAnimatorController;
	}

	void Awake(){
		PopupTextController.Initialize ();
		attribute = new Attribute ();
		setEnemyStatus ();
		health.MaxValue = attribute.getLife();
		health.CurrentValue = health.MaxValue;
		attackRate = attribute.getDelay();
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

	public void hit(int damage, bool critical, float dex){
		if (alive) {
			int totalDamage = attribute.getHit (damage, dex);
			PopupTextController.createPopupText (totalDamage.ToString(), new Vector2(transform.position.x+0.5f,transform.position.y+2), critical);
			if (totalDamage > 0) {
				anim.Play (hitHash);
			}
			health.CurrentValue -= totalDamage;
		}
	}

	//this method is called inside animation
	public void hitHero(){
		bool critical = attribute.getCritical ();
		hero.hit (attribute.getDamage(critical,1),critical, attribute.getDex());
	}

	private void die(){
		alive = false;
		anim.Play (dieHash);
	}

	public void dropMenu(){
		menu.drop ("Victory");
	}


	private void setEnemyStatus(){
		switch (LevelController.currentLevel){

		case 1:
			attribute.setStr (10);
			attribute.setDef (1);
			attribute.setAgi (1);
			attribute.setDex (15);
			attribute.setLuck (1);
			break;
		case 2:
			attribute.setStr (30);
			attribute.setDef (20);
			attribute.setAgi (10);
			attribute.setDex (30);
			attribute.setLuck (30);
			break;
		default:
			attribute.setStr (10);
			attribute.setDef (10);
			attribute.setAgi (10);
			attribute.setDex (10);
			attribute.setLuck (10);
			break;
		}

	}
}
