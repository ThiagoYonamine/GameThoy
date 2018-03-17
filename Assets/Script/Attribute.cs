using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute {
	
	private float str;
	private float def;
	private float agi;
	private float dex;
	private float luck;

	public void setStr(float str){
		this.str = str;
	}
	public void setDef(float def){
		this.def = def;
	}
	public void setAgi(float agi){
		this.agi = agi;
	}
	public void setDex(float dex){
		this.dex = dex;
	}
	public void setLuck(float luck){
		this.luck = luck;
	}

	public bool randomCritical(){
		return Random.value <= this.dex / 100;
	}

	public int getDamage(bool critical, float power){
		float dispersion = str/10f;
		float damage = Random.Range (str-dispersion, str+dispersion);
		damage *= power;
		if (critical)
			damage *= 3;
		
		return (int) damage;
	}

	public bool getCritical(){
		return (Random.value <= criticalRate());
	}

	public bool getDodge(){
		return (Random.value <= dodgeRate());
	}
		
	public float criticalRate(){
		float luckRate = luck / 200;
		float dexRate = dex / 150;
		return luckRate + dexRate - (luckRate * dexRate);
	}

	public float dodgeRate(){
		float luckRate = luck / 200;
		float agiRate = agi / 150;
		return luckRate + agiRate - (luckRate * agiRate);
	}

	public int getHit(float damage){
		float total = damage;
		float porcentBlock = (def / (damage*2));
		total -= porcentBlock*damage;
		if (total <= 0)
			total = 1;

		Debug.Log (dodgeRate ());
		if (getDodge()) {
			total = 0;
		}

		return (int) total;
	}
}
