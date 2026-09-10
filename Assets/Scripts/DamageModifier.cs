using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class DamageModifier: ScriptableObject
{
	[SerializeField]
	int priority;
	public int Priority { get => priority; }

	public abstract int ModifyDamage(int damage, Stats sourceStats, Stats targetStats);
}