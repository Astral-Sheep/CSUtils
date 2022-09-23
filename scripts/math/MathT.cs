using System;

namespace Com.Surbon.CSUtils.Math
{
	using MathSys = System.Math;

	/// <summary>
	/// Provides structs and methods for mathematical operations
	/// </summary>
	public static class MathT
	{
		/// <summary>
		/// Clamps value between min and max.
		/// </summary>
		public static float Clamp(float value, float min, float max)
		{
			if (value <= min) return min;
			else if (value >= max) return max;
			return value;
		}

		/// <summary>
		/// Clamps value between min and max.
		/// </summary>
		public static double Clamp(double value, double min, double max)
		{
			if (value <= min) return min;
			else if (value >= max) return max;
			return value;
		}

		/// <summary>
		/// Returns a modulo b.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="isPos">If true, the remainder is negative.</param>
		public static float Mod(float a, float b, bool isPos = true) => EuclidianRemainder(a, b) - (isPos ? 0 : b);

		/// <summary>
		/// Returns a modulo b.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="isPos">If true, the remainder is negative.</param>
		public static double Mod(double a, double b, bool isPos = true) => EuclidianRemainder(a, b) - (isPos ? 0 : b);

		/// <summary>
		/// Returns a modulo b.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="isPos">If true, the remainder is negative.</param>
		public static int Mod(int a, int b, bool isPos = true) => EuclidianRemainder(a, b) - (isPos ? 0 : b);

		/// <summary>
		/// Returns the euclidian quotient of a / b.
		/// </summary>
		public static float EuclidianQuotient(float a, float b) => MathF.Floor(a / b);

		/// <summary>
		/// Returns the euclidian quotient of a / b.
		/// </summary>
		public static double EuclidianQuotient(double a, double b) => MathSys.Floor(a / b);

		/// <summary>
		/// Returns the euclidian remainder of a / b.
		/// </summary>
		public static float EuclidianRemainder(float a, float b) => a - EuclidianQuotient(a, b) * b;

		/// <summary>
		/// Returns the euclidian remainder of a / b.
		/// </summary>
		public static double EuclidianRemainder(double a, double b) => a - EuclidianQuotient(a, b) * b;

		/// <summary>
		/// Returns the euclidian remainder of a / b.
		/// </summary>
		public static int EuclidianRemainder(int a, int b) => a - (a / b) * b;

		/// <summary>
		/// Returns the factorial of the given number.
		/// </summary>
		public static int Factorial(int value)
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException("The value must be greater than 0.");

			int lFact = 1;

			for (int i = 2; i <= value; i++)
			{
				lFact *= i;
			}

			return lFact;
		}

		/// <summary>
		/// Linearly interpolates between to values by a normalized ratio (clamped between 0 and 1).
		/// </summary>
		public static float Lerp(float a, float b, float ratio) => LerpUnclamped(a, b, Clamp(ratio, 0, 1));

		/// <summary>
		/// Linearly interpolates between to values by a normalized ratio (clamped between 0 and 1).
		/// </summary>
		public static double Lerp(double a, double b, double ratio) => LerpUnclamped(a, b, Clamp(ratio, 0, 1));

		/// <summary>
		/// Linearly interpolates between to values by a random normalized ratio (between 0 and 1).
		/// </summary>
		public static float LerpRand(float a, float b) => LerpUnclamped(a, b, (float)new Random().NextDouble());

		/// <summary>
		/// Linearly interpolates between to values by a random normalized ratio (between 0 and 1).
		/// </summary>
		public static double LerpRand(double a, double b) => LerpUnclamped(a, b, new Random().NextDouble());

		/// <summary>
		/// Linearly interpolates between to values by a given ratio.
		/// </summary>
		public static float LerpUnclamped(float a, float b, float ratio) => a + (b - a) * ratio;

		/// <summary>
		/// Linearly interpolates between to values by a given ratio.
		/// </summary>
		public static double LerpUnclamped(double a, double b, double ratio) => a + (b - a) * ratio;

		/// <summary>
		/// Returns a to the power of -b.
		/// </summary>
		public static float NegPow(float a, int b)
		{
			float pow = 1f;

			for (int i = 0; i < b; i++)
			{
				pow /= a;
			}

			return pow;
		}

		/// <summary>
		/// Returns a to the power of -b.
		/// </summary>
		public static double NegPow(double a, int b)
		{
			double pow = 1d;

			for (int i = 0; i < b; i++)
			{
				pow /= a;
			}

			return pow;
		}

		/// <summary>
		/// Returns the nth root of the given number.
		/// </summary>
		public static float NRoot(float value, float n) => MathF.Exp(MathF.Log(value) / n);

		/// <summary>
		/// Returns the nth root of the given number.
		/// </summary>
		public static double NRoot(double value, double n) => MathSys.Exp(MathSys.Log(value) / n);

		/// <summary>
		/// Returns a to the power of b.
		/// </summary>
		public static float PosPow(float a, int b)
		{
			float pow = 1f;

			for (int i = 0; i < b; i++)
			{
				pow *= a;
			}

			return pow;
		}

		/// <summary>
		/// Returns a to the power of b.
		/// </summary>
		public static double PosPow(double a, int b)
		{
			double pow = 1f;

			for (int i = 0; i < b; i++)
			{
				pow *= a;
			}

			return pow;
		}

		/// <summary>
		/// Returns a to the power of b.
		/// </summary>
		public static float Pow(float a, int b) => b < 0 ? NegPow(a, -b) : PosPow(a, b);

		/// <summary>
		/// Returns a to the power of b.
		/// </summary>
		public static double Pow(double a, int b) => b < 0 ? NegPow(a, -b) : PosPow(a, b);
	}
}
