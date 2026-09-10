using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class HealthComponent : MonoBehaviour
	{

		int startingHp;

		int currentHp;


		private void Awake()
		{
			startingHp = GetComponent<Creature>().Stats.BaseHealth;
			currentHp = startingHp;
			EventBus<ApplyDamageEventArgs>.Subscribe(OnTakeDamageEvent);
			EventBus<HPUpdatedEventArgs>.Invoke(new HPUpdatedEventArgs() { OldHP = currentHp, NewHP = currentHp, MaxHP = startingHp, Owner = gameObject });
		}
		private void OnDestroy()
		{
			EventBus<ApplyDamageEventArgs>.Unsubscribe(OnTakeDamageEvent);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				EventBus<ApplyDamageEventArgs>.Invoke(new ApplyDamageEventArgs() { Damage = 10, ApplyTo = gameObject });
				Debug.Log("HP:" + currentHp);
			}
		}

		private void OnTakeDamageEvent(ApplyDamageEventArgs obj)
		{
			if (obj.Damage > 0 && obj.ApplyTo == gameObject)
			{
				var oldHp = currentHp;
				currentHp = Mathf.Max(0, currentHp - obj.Damage);
				EventBus<HPUpdatedEventArgs>.Invoke(new HPUpdatedEventArgs() { OldHP = oldHp, NewHP = currentHp, MaxHP = startingHp, Owner = gameObject });
				if (currentHp <=0)
				{
					//Death
					Destroy(gameObject);
				}
			}

		}
	}
}