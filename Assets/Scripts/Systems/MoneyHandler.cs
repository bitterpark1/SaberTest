using System;
using System.Collections;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
	
	public struct MySaveData
	{
		public int Money;
	}

	int playerMoney;

	private void Awake()
	{
		EventBus<CreatureKilledEventArgs>.Subscribe(OnCreatureKilled);
		EventBus<GatheringSaveDataEventArgs>.Subscribe(OnNewSaveStarting);
		EventBus<RecievingLoadedDataEventArgs>.Subscribe(OnGameLoaded);
		
	}
	private void OnDestroy()
	{
		EventBus<CreatureKilledEventArgs>.Unsubscribe(OnCreatureKilled);
		EventBus<GatheringSaveDataEventArgs>.Unsubscribe(OnNewSaveStarting);
		EventBus<RecievingLoadedDataEventArgs>.Unsubscribe(OnGameLoaded);
	}

	private void OnNewSaveStarting(GatheringSaveDataEventArgs obj)
	{
		EventBus<SendingDataToSaveEventArgs>.Invoke(new SendingDataToSaveEventArgs { DataToSave = new MySaveData() { Money = playerMoney } });
	}

	private void OnGameLoaded(RecievingLoadedDataEventArgs obj)
	{
		foreach (var data in obj.loadedSaveData)
		{
			if (data is MySaveData myData)
			{
				playerMoney = myData.Money;
			}
		}
	}


	private void Start()
	{
		EventBus<MoneyChangedEventArgs>.Invoke(new MoneyChangedEventArgs() { NewAmount = playerMoney, OldAmount = playerMoney });
	}

	

	private void OnCreatureKilled(CreatureKilledEventArgs obj)
	{
		if (!obj.Creature.gameObject.IsPlayer())
		{
			int reward = obj.Creature.Stats.KillReward;
			if (reward != 0)
			{
				int oldAmount = playerMoney;
				playerMoney += reward;
				EventBus<MoneyChangedEventArgs>.Invoke(new MoneyChangedEventArgs() { NewAmount = playerMoney, OldAmount = oldAmount });
			}	
		}
	}

}
