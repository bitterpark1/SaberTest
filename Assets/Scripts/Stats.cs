using System.Collections;
using UnityEngine;

[System.Serializable]
public class Stats
{
	public int BaseHealth { get => baseHealth; }
	public float MoveSpeed { get => moveSpeed; }

	public int KillReward { get => killReward; }

	[SerializeField]
	int baseHealth;
	[SerializeField]
	float moveSpeed;
	[SerializeField]
	int killReward;

	public DamageModifier[] DealerModifiers => damageDealerModifiers;
	public DamageModifier[] TakerModifiers => damageTakerModifiers;

	[SerializeField]
	DamageModifier[] damageDealerModifiers;
	[SerializeField]
	DamageModifier[] damageTakerModifiers;
}