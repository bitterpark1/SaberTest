using UnityEngine;

[CreateAssetMenu(fileName = "DamageModifierSetDamage", menuName = "ScriptableObjects/DamageModifiers/DamageModifierSetDamage")]
public class DamageModifierSetDamage : DamageModifier
{
	[SerializeField]
	int setDamageToNumber;

	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		return setDamageToNumber;
	}
}