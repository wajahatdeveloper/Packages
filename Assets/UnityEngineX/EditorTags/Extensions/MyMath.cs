﻿using UnityEngine;
using System;

namespace UnityEngineX
{
	public static class MyMath
	{
		/// <summary>
		/// Swap two reference values
		/// </summary>
		public static void Swap<T>(ref T a, ref T b)
		{
			T x = a;
			a = b;
			b = x;
		}

		/// <summary>
		/// Snap to grid of "round" size
		/// </summary>
		public static float Snap(this float val, float round)
		{
			return round * Mathf.Round(val / round);
		}
		
		/// <summary>
		/// Returns the sign 1/-1 evaluated at the given value.
		/// </summary>
		public static int Sign(IComparable x) => x.CompareTo(0);
		
		/// <summary>
		/// Shortcut for Mathf.Approximately
		/// </summary>
		public static bool Approximately(this float value, float compare)
		{
			return Mathf.Approximately(value, compare);
		}
		
		/// <summary>
		/// Value is in [0, 1) range.
		/// </summary>
		public static bool InRange01(this float value)
		{
			return InRange(value, 0, 1);
		}

		/// <summary>
		/// Value is in [closedLeft, openRight) range.
		/// </summary>
		public static bool InRange<T>(this T value, T closedLeft, T openRight)
			where T : IComparable =>
			value.CompareTo(closedLeft) >= 0 && value.CompareTo(openRight) < 0;

		/// <summary>
		/// Value is in a RangedFloat.
		/// </summary>
		public static bool InRange(this float value, RangedFloat range) =>
			value.InRange(range.Min, range.Max);

		/// <summary>
		/// Value is in a RangedInt.
		/// </summary>
		public static bool InRange(this int value, RangedInt range) =>
			value.InRange(range.Min, range.Max);

		/// <summary>
		/// Value is in [closedLeft, closedRight] range, max-inclusive.
		/// </summary>
		public static bool InRangeInclusive<T>(this T value, T closedLeft, T closedRight)
			where T : IComparable =>
			value.CompareTo(closedLeft) >= 0 && value.CompareTo(closedRight) <= 0;

		/// <summary>
		/// Value is in a RangedFloat, max-inclusive.
		/// </summary>
		public static bool InRangeInclusive(this float value, RangedFloat range) =>
			value.InRangeInclusive(range.Min, range.Max);

		/// <summary>
		/// Value is in a RangedInt, max-inclusive.
		/// </summary>
		public static bool InRangeInclusive(this int value, RangedInt range) =>
			value.InRangeInclusive(range.Min, range.Max);

		/// <summary>
		/// Clamp value to less than min or more than max
		/// </summary>
		public static float NotInRange(this float num, float min, float max)
		{
			if (min > max)
			{
				var x = min;
				min = max;
				max = x;
			}

			if (num < min || num > max) return num;

			float mid = (max - min) / 2;

			if (num > min) return num + mid < max ? min : max;
			return num - mid > min ? max : min;
		}

		/// <summary>
		/// Clamp value to less than min or more than max
		/// </summary>
		public static int NotInRange(this int num, int min, int max)
		{
			return (int) ((float) num).NotInRange(min, max);
		}

		/// <summary>
		/// Return point A or B, closest to num
		/// </summary>
		public static float ClosestPoint(this float num, float pointA, float pointB)
		{
			if (pointA > pointB)
			{
				var x = pointA;
				pointA = pointB;
				pointB = x;
			}

			float middle = (pointB - pointA) / 2;
			float withOffset = num.NotInRange(pointA, pointB) + middle;
			return (withOffset >= pointB) ? pointB : pointA;
		}

		/// <summary>
		/// Check if pointA closer to num than pointB
		/// </summary>
		public static bool ClosestPointIsA(this float num, float pointA, float pointB)
		{
			var closestPoint = num.ClosestPoint(pointA, pointB);
			return Mathf.Approximately(closestPoint, pointA);
		}
	}
}