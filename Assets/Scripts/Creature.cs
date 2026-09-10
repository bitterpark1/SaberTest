using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class Creature : MonoBehaviour, IDamageDealer, IDamageTaker
	{
		IDamageModifier[] IDamageDealer.Modifiers => dealDamageModifiers;

		IDamageModifier[] IDamageTaker.Modifiers => takeDamageModifiers;

		Stats IDamageDealer.Stats => stats;
		Stats IDamageTaker.Stats => stats;

		[SerializeField]
		Stats stats;

		//[SerializeField]
		IDamageModifier[] dealDamageModifiers = new IDamageModifier[0];
		//[SerializeField]
		IDamageModifier[] takeDamageModifiers = new IDamageModifier[0];
	}
}