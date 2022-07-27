using System;

namespace Com.Surbon.CSUtils
{
	/// <summary>
	/// 
	/// </summary>
	public static class MathT
	{
		public struct Vector2
		{
			public float x;
			public float y;

			public Vector2(float pX = 0f, float pY = 0f)
			{
				x = pX;
				y = pY;
			}

			public float Angle() => MathF.Atan2(y, x);

			public bool IsNormalized() => LengthSquared() == 1f;

			public float Length() => MathF.Sqrt(x * x + y * y);

			public float LengthSquared() => x * x + y * y;

			public void Normalize(float length = 1)
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l) / length;
					x /= l;
					y /= l;
				}
			}

			public Vector2 Normalized()
			{
				float l = LengthSquared();

				if (l == 0)
					throw new InvalidOperationException("The vector's length must be greater than 0");

				l = MathF.Sqrt(l);
				return new Vector2(x / l, y / l);
			}
		}
	}
}
