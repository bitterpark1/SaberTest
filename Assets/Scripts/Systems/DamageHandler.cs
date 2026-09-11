using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
	private void Awake()
	{
		EventBus<TryDealDamageEventArgs>.Subscribe(OnDamageDealt);		
	}
	private void OnDestroy()
	{
		EventBus<TryDealDamageEventArgs>.Unsubscribe(OnDamageDealt);
	}

	private void OnDamageDealt(TryDealDamageEventArgs obj)
	{
		HandleDamage(obj.BaseDamage, obj.Target, obj.DamageDealerStats, obj.DamageTakerStats, obj.AbilityModifiers);
	}

	private void HandleDamage(int baseDamage, GameObject targetObj, Stats sourceStats, Stats targetStats, DamageModifier[] abilityModifiers)
	{
		int damage = baseDamage;
		if (damage == 0)
		{
			return;
		}
		var sourceModifiers = sourceStats.DealerModifiers;
		var targetModifiers = targetStats.TakerModifiers;

		var allMods = new List<DamageModifier>();
		if (sourceModifiers != null)
		{
			allMods.AddRange(sourceModifiers);
		}
		if (targetModifiers != null)
		{
			allMods.AddRange(targetModifiers);
		}
		if (abilityModifiers != null)
		{
			allMods.AddRange(abilityModifiers);
		}

		allMods.Sort(ComparePriority);

		foreach (var mod in allMods)
		{
			damage = mod.ModifyDamage(damage, sourceStats, targetStats);
		}

		if (damage > 0)
		{
			EventBus<ApplyDamageEventArgs>.Invoke(new ApplyDamageEventArgs() { Damage = damage, ApplyTo = targetObj });
		}
	}

	private int ComparePriority(DamageModifier x, DamageModifier y)
	{
		//Модификаторы с высшим приоритетом обрабатываются первыми
		return x.Priority.CompareTo(y.Priority) * -1;
	}
}


