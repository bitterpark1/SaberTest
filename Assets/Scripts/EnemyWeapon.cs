using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemyWeapon : MonoBehaviour
	{
		[SerializeField]
		Transform projectileOrigin;
		[SerializeField]
		Transform targetObj;
		[SerializeField]
		SpawnProjectile attackAbility;

		Creature owner;

		private void Awake()
		{
			owner = GetComponent<Creature>();
		}

		float timeSinceLastShot;

		private void Update()
		{
			timeSinceLastShot += Time.deltaTime;
			if (timeSinceLastShot > attackAbility.Cooldown)
			{
				attackAbility.UseAbility(owner, projectileOrigin.position, targetObj.position);
				timeSinceLastShot -= attackAbility.Cooldown;
			}

		}
	}
}