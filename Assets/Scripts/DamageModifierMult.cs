using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageModifierMult", menuName = "ScriptableObjects/DamageModifiers/DamageModifierMult")]
public class DamageModifierMult : DamageModifier
{
	[SerializeField]
	float multiplier;

	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		return Mathf.RoundToInt(damage * multiplier);
	}
}