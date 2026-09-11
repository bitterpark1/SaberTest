using System;
using System.Collections;
using UnityEngine;

public class PlayerAbilityUI : MonoBehaviour
{
	[SerializeField]
	AbilityCooldownView abilityViewPrefab;

	AbilityCooldownView[] abilityViews;


	private void Awake()
	{
		EventBus<AbilityCooldownEventArgs>.Subscribe(OnAbilityCooldownRefresh);
		EventBus<PlayerAbilitiesSetEventArgs>.Subscribe(OnAbilitiesSet);
	}

	private void OnDestroy()
	{
		EventBus<AbilityCooldownEventArgs>.Unsubscribe(OnAbilityCooldownRefresh);
		EventBus<PlayerAbilitiesSetEventArgs>.Unsubscribe(OnAbilitiesSet);
	}

	private void OnAbilityCooldownRefresh(AbilityCooldownEventArgs obj)
	{
		if (obj.AbilityIndex < abilityViews.Length)
		{
			abilityViews[obj.AbilityIndex].SetState(obj.CooldownPercentage);
		}
	}

	private void OnAbilitiesSet(PlayerAbilitiesSetEventArgs obj)
	{
		var abilitiesCount = obj.Abilities.Length;

		abilityViews = new AbilityCooldownView[abilitiesCount];
		for (int i = 0; i < abilitiesCount; i++)
		{
			var newView = Instantiate(abilityViewPrefab, transform);
			newView.SetIcon(obj.Abilities[i].Icon);
			abilityViews[i] = newView;
		}
	}
}
