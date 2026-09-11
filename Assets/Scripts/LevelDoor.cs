using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class LevelDoor : MonoBehaviour
	{
		[SerializeField]
		float closeTime = 0.5f;
		[SerializeField]
		float endY =1;

		private void Awake()
		{
			EventBus<ArenaEnteredEventArgs>.Subscribe(OnArenaEntered);
		}
		private void OnDestroy()
		{
			EventBus<ArenaEnteredEventArgs>.Unsubscribe(OnArenaEntered);
		}

		private void OnArenaEntered(ArenaEnteredEventArgs obj)
		{
			StartCoroutine(CloseDoor());
		}

		IEnumerator CloseDoor()
		{
			if (closeTime <= 0)
			{
				throw new Exception("Переменная closeTime двери должна быть >0!");
			}
			var startingY = transform.position.y;
			float timeElapsed = 0;

			while (timeElapsed < closeTime)
			{
				transform.position = new Vector3(transform.position.x, Mathf.Lerp(startingY, endY, timeElapsed/closeTime), transform.position.z);
				yield return null;
				timeElapsed += Time.deltaTime;
			}
		}
	}
}