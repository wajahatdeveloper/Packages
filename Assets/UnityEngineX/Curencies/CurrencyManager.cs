using ByteSheep.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineX;

public class CurrencyManager : SingletonBehaviour<CurrencyManager>
{
	public enum CurrencyType
	{
		Score,
		Money,
		Gems
	}

	[System.Serializable] public class CurrencyEvent : QuickEvent<CurrencyType> { }

	public CurrencyEvent OnCurrencyIncrease;
	public CurrencyEvent OnCurrencyDecrease;
	public CurrencyEvent OnCurrencyChange;

	private CurrencyType currencyType;
	private string currencyIDString = "currency_";
	private Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();

	private void Start()
	{
		foreach (var currency in Enum.GetValues(typeof(CurrencyType)))
		{
			LoadCurrencyData( (CurrencyType)currency );
		}
	}

	public void IncreaseCurrency(CurrencyType currencyType, int amount)
	{
		currencies[currencyType] += amount;
		OnCurrencyIncrease?.Invoke( currencyType );
		SaveCurrencyData( currencyType );
	}

	public void DecreaseCurrency( CurrencyType currencyType, int amount )
	{
		currencies[currencyType] -= amount;
		OnCurrencyDecrease?.Invoke( currencyType );
		SaveCurrencyData( currencyType );
	}

	public void SetCurrency( CurrencyType currencyType, int amount )
	{
		currencies[currencyType] = amount;
		OnCurrencyChange?.Invoke( currencyType );
		SaveCurrencyData( currencyType );
	}

	public int GetCurrency( CurrencyType currencyType )
	{
		return currencies[currencyType];
	}

	private void SaveCurrencyData(CurrencyType currencyType)
	{
		PlayerPrefs.SetInt( currencyIDString + currencyType.ToString(), currencies[currencyType] );
	}

	private void LoadCurrencyData( CurrencyType currencyType )
	{
		currencies[currencyType] = PlayerPrefs.GetInt( currencyIDString + currencyType.ToString(), 0 );
	}
}