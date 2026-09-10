using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEventArgs { }

public struct ApplyDamageEventArgs:IEventArgs
{
	public int Damage;
	public GameObject ApplyTo;
}

public struct HPUpdatedEventArgs : IEventArgs
{
	public GameObject Owner;
	public int OldHP;
	public int NewHP;
	public int MaxHP;
}

public struct DamageDealtEventArgs: IEventArgs
{
	public GameObject Target;
	public IDamageDealer DamageDealer;
	public IDamageTaker DamageTaker;
}

public static class EventBus<T> where T: IEventArgs
{
	static Action<T> subDelegates;

	public static void Subscribe(Action<T> @delegate)
	{
		subDelegates += @delegate;
	}
	public static void Unsubscribe(Action<T> @delegate)
	{
		subDelegates -= @delegate;
	}

	public static void Invoke(T @event)
	{
		subDelegates?.Invoke(@event);
	}

	public static void Clear()
	{
		subDelegates = null;
	}

}

public static class EventBusUtil
{
	public static IReadOnlyList<Type> EventTypes { get; set; }
	public static IReadOnlyList<Type> EventBusTypes { get; set; }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Initialize()
	{
		EventTypes = GetInterfaceImplementors();
		EventBusTypes = InitializeEventBuses();
	}

	public static List<Type> GetInterfaceImplementors()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		var interfaceType = typeof(IEventArgs);

		var types = new List<Type>();

		foreach (var asm in assemblies)
		{
			foreach (var type in asm.GetTypes())
			{
				if (type != interfaceType && interfaceType.IsAssignableFrom(type))
				{
					types.Add(type);
				}
			}
		}
		return types;
	}

	static List<Type> InitializeEventBuses()
	{
		var eventBusTypes = new List<Type>();

		var busBaseType = typeof(EventBus<>);

		foreach (var eventType in EventTypes)
		{
			var busType = busBaseType.MakeGenericType(eventType);
			eventBusTypes.Add(busType);
		}

		return eventBusTypes;
	}

	public static void ClearAll()
	{
		foreach (var type in EventBusTypes)
		{
			var clearMethod = type.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			clearMethod.Invoke(null, null);
		}
	}

#if UNITY_EDITOR

	[RuntimeInitializeOnLoadMethod]
	private static void OnEditorInit()
	{
		UnityEditor.EditorApplication.playModeStateChanged -= OnPlayStateChanged;
		UnityEditor.EditorApplication.playModeStateChanged += OnPlayStateChanged;
	}

	private static void OnPlayStateChanged(UnityEditor.PlayModeStateChange stateChange)
	{
		if (stateChange == UnityEditor.PlayModeStateChange.ExitingPlayMode)
		{
			ClearAll();
		}
	}
#endif
}



public static class AssemblyUtil
{
	//enum AssemblyType { }

	public static List<Type> GetInterfaceImplementors(Type @interface) 
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();

		var types = new List<Type>();

		foreach (var asm in assemblies)
		{
			foreach (var type in asm.GetTypes())
			{
				if (type != @interface && @interface.IsAssignableFrom(type))
				{
					types.Add(type);
				}
			}

		}

		return types;
	}
}