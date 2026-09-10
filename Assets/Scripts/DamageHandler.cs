using System;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{

	private void Awake()
	{
		EventBus<DamageDealtEventArgs>.Subscribe(OnDamageDealt);		
	}
	private void OnDestroy()
	{
		EventBus<DamageDealtEventArgs>.Unsubscribe(OnDamageDealt);
	}

	private void OnDamageDealt(DamageDealtEventArgs obj)
	{
		HandleDamage(
			obj.DamageDealer.Stats.BaseDamage,
			obj.Target,
			obj.DamageDealer.Stats,
			obj.DamageTaker.Stats,
			obj.DamageDealer.Modifiers,
			obj.DamageTaker.Modifiers);
	}

	void HandleDamage(int baseDamage, GameObject targetObj, Stats sourceStats, Stats targetStats, IDamageModifier[] sourceModifiers, IDamageModifier[] targetModifiers)
	{
		if (baseDamage == 0)
		{
			return;
		}

		var allMods = new IDamageModifier[sourceModifiers.Length + targetModifiers.Length];
		sourceModifiers.CopyTo(allMods, 0);
		targetModifiers.CopyTo(allMods, sourceModifiers.Length);

		Array.Sort(allMods, ComparePriority);

		int damage = baseDamage;
		foreach (var mod in allMods)
		{
			damage = mod.ModifyDamage(damage, sourceStats, targetStats);
		}

		if (damage > 0)
		{
			EventBus<ApplyDamageEventArgs>.Invoke(new ApplyDamageEventArgs() { Damage = damage, ApplyTo = targetObj });
		}
	}

	int ComparePriority(IDamageModifier x, IDamageModifier y)
	{
		return x.Priority.CompareTo(y.Priority) * -1;
	}
}

[System.Serializable]
public struct Stats
{
	public int BaseDamage;
	public float AttackSpeed;
	public float ProjectileSpeed;
	public float MoveSpeed;
}

public interface IDamageDealer
{
	IDamageModifier[] Modifiers { get; }
	Stats Stats { get; }
}
public interface IDamageTaker
{
	IDamageModifier[] Modifiers { get; }
	Stats Stats { get; }
}

public interface IDamageModifier
{
	public int Priority { get; }
	public int ModifyDamage(int damage, Stats sourceStats, Stats targetStats);
}

[System.Serializable]
public abstract class DamageModifier : IDamageModifier
{
	[SerializeField]
	int priority;
	public int Priority { get => priority; }

	public abstract int ModifyDamage(int damage, Stats sourceStats, Stats targetStats);
}

public class DamageModifierMult : DamageModifier
{
	[SerializeField]
	float multiplier;

	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		return Mathf.RoundToInt(damage * multiplier);
	}
}

public class DamageModifierSet : DamageModifier
{
	[SerializeField]
	int priority;
	[SerializeField]
	int setDamageToNumber;

	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		return setDamageToNumber;
	}
}
