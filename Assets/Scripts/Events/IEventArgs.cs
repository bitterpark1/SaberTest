using UnityEngine;

public interface IEventArgs { }

public struct ApplyDamageEventArgs : IEventArgs
{
	public int Damage;
	public GameObject ApplyTo;
}

public struct HPUpdatedEventArgs : IEventArgs
{
	public GameObject Owner;
	public int OldHP;
	public int NewHP;
	public int MaxHP;
}

public struct TryDealDamageEventArgs : IEventArgs
{
	public int BaseDamage;
	public GameObject Target;
	public Stats DamageDealerStats;
	public Stats DamageTakerStats;
	public DamageModifier[] AbilityModifiers;
	public bool CountAsSeparateHit;
}

public struct AbilityCooldownEventArgs : IEventArgs
{
	public float CooldownPercentage;
	public int AbilityIndex;
}

public struct PlayerAbilitiesSetEventArgs : IEventArgs
{
	public Ability[] Abilities;
}

public struct ArenaEnteredEventArgs: IEventArgs
{

}

public struct ProjectileSpawnedEventArgs : IEventArgs
{
	public GameObject Owner;
}

public struct CreatureKilledEventArgs : IEventArgs
{
	public Creature Creature;
}

public struct EnemiesSpawnedEventArgs : IEventArgs
{
	public int EnemiesCount;
}
public struct EnemyDiedEventArgs : IEventArgs
{

}

public struct GameOverEventArgs : IEventArgs
{
	public bool PlayerWon;
}

public struct ShowResultsEventArgs : IEventArgs
{
	public bool PlayerWon;
	public SessionStats Stats;
}

public struct MoneyChangedEventArgs : IEventArgs
{
	public int OldAmount;
	public int NewAmount;
}

public struct NewGamePressedEventArgs : IEventArgs
{

}

public struct GameStateChangedEventArgs : IEventArgs
{
	public GameStateManager.State NewState;
}

public struct ReturnToMainMenuEventArgs : IEventArgs
{

}

public struct SaveGamePressedEventArgs : IEventArgs
{

}

public struct GatheringSaveDataEventArgs : IEventArgs
{

}

public struct SendingDataToSaveEventArgs: IEventArgs
{
	public object DataToSave;
}

public struct LoadGamePressedEventArgs : IEventArgs
{

}

public struct RecievingLoadedDataEventArgs : IEventArgs
{
	public object[] loadedSaveData;
}