using System;
using System.Collections;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
	int enemiesAlive;
	
	private void Awake()
	{
		EventBus<CreatureKilledEventArgs>.Subscribe(OnCreatureKilled);
		EventBus<EnemiesSpawnedEventArgs>.Subscribe(OnEnemiesSpawned);
		EventBus<EnemyDiedEventArgs>.Subscribe(OnEnemyDied);
	}

	private void OnDestroy()
	{
		EventBus<CreatureKilledEventArgs>.Unsubscribe(OnCreatureKilled);
		EventBus<EnemiesSpawnedEventArgs>.Unsubscribe(OnEnemiesSpawned);
		EventBus<EnemyDiedEventArgs>.Unsubscribe(OnEnemyDied);
	}

	private void OnEnemiesSpawned(EnemiesSpawnedEventArgs obj)
	{
		enemiesAlive = obj.EnemiesCount;
		if (enemiesAlive == 0 )
		{
			EventBus<GameOverEventArgs>.Invoke(new GameOverEventArgs() { PlayerWon = true });
		}
	}

	private void OnEnemyDied(EnemyDiedEventArgs obj)
	{
		enemiesAlive--;
		if (enemiesAlive == 0)
		{
			EventBus<GameOverEventArgs>.Invoke(new GameOverEventArgs() { PlayerWon = true });
		}
	}

	private void OnCreatureKilled(CreatureKilledEventArgs obj)
	{
		if (obj.Creature.gameObject.IsPlayer())
		{
			EventBus<GameOverEventArgs>.Invoke(new GameOverEventArgs() { PlayerWon = false });
		}
	}
}