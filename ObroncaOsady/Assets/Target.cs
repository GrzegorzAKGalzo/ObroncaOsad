using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

	public float health = 100;
	Animator m_Animator;
	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			Die();
		}
	}


	void Die()
	{
		m_Animator.SetBool("IsDying", true);
		StartCoroutine(WaitForDieAnimation());
	}

	IEnumerator WaitForDieAnimation()
    {
		yield return new WaitForSeconds(2.0f);
		Destroy(gameObject);
	}
}
