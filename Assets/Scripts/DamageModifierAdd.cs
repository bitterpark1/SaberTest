using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageModifierAdd", menuName = "ScriptableObjects/DamageModifiers/DamageModifierAdd")]
public class DamageModifierAdd : DamageModifier
{
	[SerializeField]
	int add;

	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		return damage + add;
	}
}
