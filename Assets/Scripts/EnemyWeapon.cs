using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemyWeapon : MonoBehaviour
	{

		[SerializeField]
		Projectile projectilePrefab;
		[SerializeField]
		Transform projectileOrigin;
		[SerializeField]
		Transform targetObj;

		IDamageDealer owner;

		private void Awake()
		{
			owner = GetComponent<IDamageDealer>();
		}

		float timeSinceLastShot;

		private void Update()
		{
			timeSinceLastShot += Time.deltaTime;
			if (timeSinceLastShot > owner.Stats.AttackSpeed)
			{
				//Spawn bullet
				SpawnBullet();
				timeSinceLastShot -= owner.Stats.AttackSpeed;
			}

		}

		private void SpawnBullet()
		{
			var newBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			newBullet.Initialize(targetObj.position - transform.position, owner.Stats.ProjectileSpeed, owner);
		}

	}
}