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

			public Vector2 Abs() => new Vector2(MathF.Abs(x), MathF.Abs(y));

			public float Angle() => MathF.Atan2(y, x);

			public float AngleTo(Vector2 vector) => MathF.Atan2(Cross(vector), Dot(vector));

			public float AngleToPoint(Vector2 vector) => (vector - this).Angle();

			public void CeilLength()
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					x /= l / MathF.Ceiling(l);
					y /= l / MathF.Ceiling(l);
				}
			}

			public void CeilValues()
			{
				x = MathF.Ceiling(x);
				y = MathF.Ceiling(y);
			}

			public void ClampLength(float min, float max)
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float lClamped = Clamp(l, min, max);
					x /= l / lClamped;
					y /= l / lClamped;
				}
			}

			public void ClampValues(float minX, float maxX, float minY, float maxY)
			{
				x = Clamp(x, minX, maxX);
				y = Clamp(y, minY, maxY);
			}

			public void ClampValuesUniform(float min, float max)
			{
				ClampValues(min, max, min, max);
			}

			public float Cross(Vector2 vector) => x * vector.y - y * vector.x;

			public float Distance(Vector2 vector) => MathF.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.x) * (y - vector.y));

			public float DistanceSquared(Vector2 vector) => (x - vector.x) * (x - vector.x) + (y - vector.x) * (y - vector.y);

			public float Dot(Vector2 vector) => x * vector.x + y * vector.y;

			public override bool Equals(object obj)
			{
				if (obj is Vector2)
					return (Vector2)obj == this;
				return false;
			}

			public void FloorLength()
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					x /= l / MathF.Floor(l);
					y /= l / MathF.Floor(l);
				}
			}

			public void FloorValues()
			{
				x = MathF.Floor(x);
				y = MathF.Floor(y);
			}

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

			public void RoundLength()
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					x /= l / MathF.Round(l);
					y /= l / MathF.Round(l);
				}
			}

			public void RoundValues()
			{
				x = MathF.Round(x);
				y = MathF.Round(y);
			}

			#region OPERATORS

			public static Vector2 operator +(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);

			public static Vector2 operator -(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);

			public static Vector2 operator *(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x * vector2.x, vector1.y * vector2.y);

			public static Vector2 operator *(Vector2 vector, float scalar) => new Vector2(vector.x * scalar, vector.y * scalar);

			public static Vector2 operator *(float scalar, Vector2 vector) => new Vector2(vector.x * scalar, vector.y * scalar);

			public static Vector2 operator /(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x / vector2.x, vector1.y / vector2.y);

			public static Vector2 operator /(Vector2 vector, float scalar) => new Vector2(vector.x / scalar, vector.y / scalar);

			public static bool operator ==(Vector2 vector1, Vector2 vector2) => vector1.x == vector2.x && vector1.y == vector2.y;

			public static bool operator !=(Vector2 vector1, Vector2 vector2) => vector1.x != vector2.x || vector1.y != vector2.y;

			#endregion OPERATORS
		}

		public static float Clamp(float value, float min, float max)
		{
			if (value <= min) return min;
			else if (value >= max) return max;
			return value;
		}
	}
}
