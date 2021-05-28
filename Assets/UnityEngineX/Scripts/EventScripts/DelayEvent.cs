using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayEvent : MonoBehaviour
{
	public UnityEvent onEvent;
	public bool scaledTime = true;

	public void DelayedEvent(float delay)
	{
		StartCoroutine(OnEvent(delay));
	}

	public IEnumerator OnEvent(float delay)
	{
		if (scaledTime)
		{
			yield return new WaitForSeconds(delay);
		}
		else
		{
			yield return new WaitForSecondsRealtime(delay);
		}

		onEvent?.Invoke();
	}
}
