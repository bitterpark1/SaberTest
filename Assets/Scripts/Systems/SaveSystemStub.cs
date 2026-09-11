using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemStub : MonoBehaviour
{

	List<object> dataAwaitingSave = new List<object>();

	private void Awake()
	{
		EventBus<SaveGamePressedEventArgs>.Subscribe(OnSaveGamePressed);
		EventBus<LoadGamePressedEventArgs>.Subscribe(OnLoadGamePressed);
	}

	private void OnDestroy()
	{
		EventBus<SaveGamePressedEventArgs>.Unsubscribe(OnSaveGamePressed);
		EventBus<LoadGamePressedEventArgs>.Unsubscribe(OnLoadGamePressed);
	}

	private void OnSaveGamePressed(SaveGamePressedEventArgs obj)
	{
		//Подписываемся на ивент отправки данных для сохранения, и вызываем GatheringSaveData ивент, чтобы отправители
		//начали отправлять данные для сейва
		EventBus<SendingDataToSaveEventArgs>.Subscribe(OnDataPushedToSaveSystem);
		EventBus<GatheringSaveDataEventArgs>.Invoke(new GatheringSaveDataEventArgs());
		//Получив данные отписываемся
		EventBus<SendingDataToSaveEventArgs>.Unsubscribe(OnDataPushedToSaveSystem);
		SerializeRecievedData();
		Debug.Log("Сохранение вызвано");
	}
	private void SerializeRecievedData()
	{
		foreach (var data in dataAwaitingSave)
		{
			//Тут должна быть сериализация полученных данных
		}
	}
	private void OnDataPushedToSaveSystem(SendingDataToSaveEventArgs obj)
	{
		//Добавляем полученные данные в список на сейв
		dataAwaitingSave.Add(obj.DataToSave);	
	}

	private void OnLoadGamePressed(LoadGamePressedEventArgs obj)
	{
		object[] deserializedData = GetDeserializedSaveData();
		//Отправляем данные всем подписчикам
		EventBus<RecievingLoadedDataEventArgs>.Invoke(new RecievingLoadedDataEventArgs {loadedSaveData = deserializedData });
		Debug.Log("Загрузка сохранения вызвана");
	}

	private object[] GetDeserializedSaveData()
	{
		//Тут должна быть десериализация сейва
		return new object[0];
	}
}
