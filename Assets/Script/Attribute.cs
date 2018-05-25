using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute {
	
	private float str;
	private float def;
	private float agi;
	private float dex;
	private float luck;
	private float energy;

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
	public float getDex(){
		return dex;
	}
	public void setLuck(float luck){
		this.luck = luck;
	}
	public void setEnergy(float energy){
		this.energy = energy;
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

	public bool getDodge(float dex){
		return (Random.value <= dodgeRate(dex));
	}
		
	public float criticalRate(){
		float luckRate = luck / 9999;
		float dexRate = (dex/2) / 9999;
		return luckRate + dexRate - (luckRate * dexRate);
	}

	public float dodgeRate(float dexEnemy){
		float luckRate = (luck/2) / 9999;
		float agiRate = (agi/4) / dexEnemy;
		return Mathf.Min(luckRate + agiRate - (luckRate * agiRate), 0.80f);
	}

	public int getHit(float damage, float dex){
		float total = damage - getBlock();

		//min block damage = 1
		if (total <= 0)
			total = 1;
		
		if (getDodge(dex)) {
			total = 0;
		}

		return (int) total;
	}

	public float getTotalEnergy(){
		return 100 + (energy/10);
	}

	public float getRecovery(){
		return 0.05f + ((int)energy/10) / 100;
	}

	public float getLife(){
		return 50 + (def * 0.4f); 
	}

	public float getBlock(){
		return def * 0.3f;
	}

	public float getDelay(){
		return 2f - (agi / 5000);
	}
}
