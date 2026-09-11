using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	[SerializeField]
	Transform projectileOrigin;
	[SerializeField]
	SpawnProjectile attackAbility;


	EnemyTarget target;
	Creature owner;
	float timeSinceLastShot;

	private void Awake()
	{
		owner = GetComponent<Creature>();
		target = GetComponent<EnemyTarget>();
	}

	private void Update()
	{
		timeSinceLastShot += Time.deltaTime;
		if (timeSinceLastShot > attackAbility.Cooldown)
		{
			attackAbility.UseAbility(owner, projectileOrigin.position, target.TargetObj.position);
			timeSinceLastShot -= attackAbility.Cooldown;
		}

	}
}
