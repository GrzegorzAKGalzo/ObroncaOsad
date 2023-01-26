using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : MonoBehaviour

{
	public GameOverScreen GameOverScreen;
	public float health = 100;
	public void PlayerTakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		GameOverScreen.Setup();
	}
}
