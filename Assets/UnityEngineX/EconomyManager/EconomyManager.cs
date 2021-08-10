using ByteSheep.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineX;

[System.Serializable] public class EconomyEvent : QuickEvent<string> { }

public class EconomyManager : SingletonBehaviour<EconomyManager>
{
	public List<string> typesOfEnonomies;
	[Space(16)]
	public EconomyEvent onEconomyIncrease;
	[Space(16)]
	public EconomyEvent onEconomyDecrease;
	[Space(16)]
	public EconomyEvent onEconomyChange;

	private const string CurrencyIdString = "currency_";
	private Dictionary<string, int> _currencies = new Dictionary<string, int>();

	private void Start()
	{
		foreach (var currency in typesOfEnonomies) { LoadEconomyData( currency ); }
	}

	public void IncreaseEconomyValue(string economyType, int amount)
	{
		_currencies[economyType] += amount;
		onEconomyIncrease?.Invoke( economyType );
		SaveEconomyData( economyType );
	}

	public void DecreaseEconomyValue( string economyType, int amount )
	{
		_currencies[economyType] -= amount;
		onEconomyDecrease?.Invoke( economyType );
		SaveEconomyData( economyType );
	}

	public void SetEconomyValue(string economyType, int amount)
	{
		_currencies[economyType] = amount;
		onEconomyChange?.Invoke(economyType);
		SaveEconomyData(economyType);
	}

	public int GetEconomyValue(string economyType)
	{
		return _currencies[economyType];
	}

	private void SaveEconomyData(string economyType)
	{
		PlayerPrefs.SetInt( CurrencyIdString + economyType, _currencies[economyType] );
	}

	private void LoadEconomyData( string economyType )
	{
		_currencies[economyType] = PlayerPrefs.GetInt( CurrencyIdString + economyType, 0 );
	}
}