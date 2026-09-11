using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField]
		EnemyTarget enemyPrefab;

		[SerializeField]
		int spawnCount = 4;

		const int CIRCLES_MAX = 3;

		[SerializeField]
		float circleRadiusStep = 5f;

		[SerializeField]
		Transform playerObj;
		[SerializeField]
		Transform enemiesParent;

		private void Awake()
		{
			EventBus<ArenaEnteredEventArgs>.Subscribe(OnArenaEntered);	
		}
		private void OnDestroy()
		{
			EventBus<ArenaEnteredEventArgs>.Unsubscribe(OnArenaEntered);
		}

		private void OnArenaEntered(ArenaEnteredEventArgs obj)
		{
			SpawnEnemiesInCircles(spawnCount);
		}

		private void SpawnEnemiesInCircles(int amount)
		{
			int circleIndex = CIRCLES_MAX;
			int extraPositionsPerIndex = 2;
			int finalCirclePositions = 3;
			int spawned = 0;

			while (spawned < amount && circleIndex > 0)
			{
				int positions = finalCirclePositions + circleIndex * extraPositionsPerIndex;
				for (int i = 0; i < positions; i++)
				{
					float angle = i * (Mathf.PI * 2 / positions) * Mathf.Rad2Deg;
					Vector3 offset = Quaternion.Euler(0, angle, 0) * new Vector3((circleIndex + 1) * circleRadiusStep, 0, 0);
					Vector3 pos = enemiesParent.transform.position + offset;
					var newEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
					newEnemy.Initialize(playerObj);
					newEnemy.transform.SetParent(enemiesParent, true);

					spawned++;
					if (spawned == amount)
					{
						break;
					}
				}
				circleIndex--;	
			}
			EventBus<EnemiesSpawnedEventArgs>.Invoke(new EnemiesSpawnedEventArgs() { EnemiesCount = spawned });
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(enemiesParent.transform.position, CIRCLES_MAX * circleRadiusStep);
		}
	}
}