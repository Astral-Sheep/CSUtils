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

			public float Length()
			{
				return MathF.Sqrt(x * x + y * y);
			}

			public float LengthSquared()
			{
				return x * x + y * y;
			}

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
		}
	}
}
