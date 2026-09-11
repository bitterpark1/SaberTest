using System;
using System.Collections;
using UnityEngine;


public class StatsTrackingSystem : MonoBehaviour
{
	SessionStats sessionStats;


	const string PLAYER_LAYER_NAME = "Player";

	private void Awake()
	{
		EventBus<TryDealDamageEventArgs>.Subscribe(OnDamageDealt);
		EventBus<ProjectileSpawnedEventArgs>.Subscribe(OnProjectileSpawned);
		EventBus<CreatureKilledEventArgs>.Subscribe(OnCreatureKilled);
		EventBus<ArenaEnteredEventArgs>.Subscribe(OnArenaEntered);
		EventBus<GameOverEventArgs>.Subscribe(OnGameOver);
		EventBus<ApplyDamageEventArgs>.Subscribe(OnApplyDamage);
	}
	private void OnDestroy()
	{
		EventBus<TryDealDamageEventArgs>.Unsubscribe(OnDamageDealt);
		EventBus<ProjectileSpawnedEventArgs>.Unsubscribe(OnProjectileSpawned);
		EventBus<CreatureKilledEventArgs>.Unsubscribe(OnCreatureKilled);
		EventBus<ArenaEnteredEventArgs>.Unsubscribe(OnArenaEntered);
		EventBus<GameOverEventArgs>.Unsubscribe(OnGameOver);
		EventBus<ApplyDamageEventArgs>.Unsubscribe(OnApplyDamage);
	}

	private void OnApplyDamage(ApplyDamageEventArgs obj)
	{
		if (obj.ApplyTo.IsPlayer())
		{
			sessionStats.TotalDamageTaken += obj.Damage;
		} else
		{
			sessionStats.TotalDamageDealt += obj.Damage;
		}
	}

	private void OnGameOver(GameOverEventArgs obj)
	{
		sessionStats.FinishTime = Time.time;
		EventBus<ShowResultsEventArgs>.Invoke(new ShowResultsEventArgs() { PlayerWon = obj.PlayerWon, Stats = sessionStats });
		sessionStats = new SessionStats();
	}

	private void OnArenaEntered(ArenaEnteredEventArgs obj)
	{
		sessionStats.StartTime = Time.time;
	}

	private void OnCreatureKilled(CreatureKilledEventArgs obj)
	{
		if (!obj.Creature.gameObject.IsPlayer())
		{
			sessionStats.EnemiesKilled++;
		}
	}

	private void OnProjectileSpawned(ProjectileSpawnedEventArgs obj)
	{
		if (obj.Owner.IsPlayer())
		{
			sessionStats.ShotsFired++;
		}
	}

	private void OnDamageDealt(TryDealDamageEventArgs obj)
	{
		if (!obj.Target.IsPlayer() && obj.CountAsSeparateHit)
		{
			sessionStats.ShotsHit++;
		}
	}
}

public struct SessionStats
{
	public int TotalDamageDealt;
	public int TotalDamageTaken;
	public int EnemiesKilled;
	public float StartTime;
	public float FinishTime;
	public int ShotsFired;
	public int ShotsHit;
}

public static class GameObjExtension
{
	public static bool IsPlayer(this GameObject gameObj)
	{
		return gameObj.layer == LayerMask.NameToLayer("Player");
	}
}