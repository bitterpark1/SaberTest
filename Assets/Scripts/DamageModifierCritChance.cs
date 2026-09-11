using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageModifierCritChance", menuName = "ScriptableObjects/DamageModifiers/DamageModifierCritChance")]
public class DamageModifierCritChance : DamageModifier
{
	[SerializeField, Range(0, 1f)]
	float chance;
	[SerializeField]
	float critMultiplier;
	
	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		if (Random.value < chance)
		{
			return Mathf.RoundToInt(damage * critMultiplier);
		} else
		{
			return damage;
		}
	}
}
