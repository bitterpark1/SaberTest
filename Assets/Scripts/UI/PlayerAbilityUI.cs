using System;
using System.Collections;
using UnityEngine;

public class PlayerAbilityUI : MonoBehaviour
{
	[SerializeField]
	AbilityCooldownView[] extraAbilityViews;

	[SerializeField]
	AbilityCooldownView mainAttackView;

	private void Awake()
	{
		EventBus<AbilityCooldownEventArgs>.Subscribe(OnAbilityCooldownRefresh);
		EventBus<PlayerAbilitiesSet>.Subscribe(OnAbilitiesSet);
	}

	private void OnDestroy()
	{
		EventBus<AbilityCooldownEventArgs>.Unsubscribe(OnAbilityCooldownRefresh);
		EventBus<PlayerAbilitiesSet>.Unsubscribe(OnAbilitiesSet);
	}

	private void OnAbilityCooldownRefresh(AbilityCooldownEventArgs obj)
	{
		if (obj.AbilityIndex < extraAbilityViews.Length)
		{
			extraAbilityViews[obj.AbilityIndex].SetState(obj.CooldownPercentage);
		}
	}

	private void OnAbilitiesSet(PlayerAbilitiesSet obj)
	{
		for (int i = 0; i < Mathf.Min(extraAbilityViews.Length, obj.Abilities.Length); i++)
		{
			extraAbilityViews[i].SetIcon(obj.Abilities[i].Icon);
		}
	}
}
