using UnityEngine;

[CreateAssetMenu(fileName = "DamageModifierMult", menuName = "ScriptableObjects/DamageModifiers/DamageModifierMult")]
public class DamageModifierSet : DamageModifier
{
	[SerializeField]
	int setDamageToNumber;

	public override int ModifyDamage(int damage, Stats sourceStats, Stats targetStats)
	{
		return setDamageToNumber;
	}
}