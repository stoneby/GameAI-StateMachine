﻿using UnityEngine;
using System.Collections;

public class TurretTargeting : MonoBehaviour {
	[HideInInspector]public GameObject goPlayer;
	public GameObject goBullet;
	[HideInInspector]public Transform tBarrel;
	private int m_intStartTime;
	public int intFireRate = 60;
	public float fltBulletSpeed = 2.5f;
	public int intBulletLife = 180;
	public int intBulletSize = 1;
	public bool isFiring = false;
	SeekPlayer seekPlayer;
	
	void Start () {
		m_intStartTime = 0;
		goPlayer = GameObject.FindGameObjectWithTag ("Player");
		tBarrel = this.transform.GetChild (0).transform.GetChild (0);
		seekPlayer = (SeekPlayer)goBullet.GetComponent<SeekPlayer>();
	}

	void Update () 
	{
		if(goPlayer != null)
		{
			this.transform.GetChild(0).transform.LookAt(new Vector3(goPlayer.transform.position.x, 
			                                                        this.transform.GetChild (0).transform.position.y, 
			                                                        goPlayer.transform.position.z));
			
			if(isFiring)
			{
				if(m_intStartTime >= intFireRate)
					ShootBullet ();
				m_intStartTime++;
			}
		}
		else
		{
			goPlayer = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	void ShootBullet (){
		GameObject firedBullet = (GameObject)Instantiate (goBullet, tBarrel.position, Quaternion.identity);
		firedBullet.name = "Bullet";
		firedBullet.transform.localScale *= intBulletSize;
		firedBullet.rigidbody.velocity = tBarrel.forward * fltBulletSpeed;
		firedBullet.transform.GetComponent<DeleteObject>().intDeathTimer = intBulletLife;
		m_intStartTime = 0;
		seekPlayer.fltBulletSpeed = fltBulletSpeed;
	}
	void StartShooting () 
	{
		if(!isFiring)
			isFiring = true;
	}
	void StopShooting () 
	{
		if(isFiring)
			isFiring = false;
	}
}
