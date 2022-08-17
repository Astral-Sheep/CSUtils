using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a vector in a N dimensional space
	/// </summary>
	public struct VectorN
	{
		public readonly int Size;

		/// <summary>
		/// Returns the coordinate on the given axis. Example: x for 0, y for 1, z for 2, w for 3...
		/// </summary>
		public float this[int index]
		{
			get => values[index];
			set
			{
				values[index] = value;
			}
		}

		private float[] values;

		public VectorN(params float[] pValues)
		{
			values = pValues == null ? new float[4] : pValues;
			Size = values.Length;
		}

		public VectorN(int size)
		{
			values = new float[size];
			Size = size;
		}

		#region OPERATORS

		private static VectorN Operate(VectorN vector1, VectorN vector2, Func<float, float, float> operation)
		{
			if (vector1.Size != vector2.Size)
				throw new InvalidOperationException("Both vectors must have the same dimensions.");

			int dimension = vector1.Size;

			float[] lValues = new float[dimension];

			for (int i = 0; i < dimension; i++)
			{
				lValues[i] = operation(vector1[i], vector2[i]);
			}

			return new VectorN(lValues);
		}

		private static VectorN Operate(VectorN vector, float scalar, Func<float, float, float> operation)
		{
			int dimension = vector.Size;

			float[] lValues = new float[dimension];

			for (int i = 0; i < dimension; i++)
			{
				lValues[i] = operation(vector[i], scalar);
			}

			return new VectorN(lValues);
		}

		public static VectorN operator +(VectorN vector1, VectorN vector2)
		{
			return Operate(vector1, vector2, delegate (float n1, float n2)
			{
				return n1 + n2;
			});
		}

		public static VectorN operator -(VectorN vector1, VectorN vector2)
		{
			return Operate(vector1, vector2, delegate (float n1, float n2)
			{
				return n1 - n2;
			});
		}

		public static VectorN operator *(VectorN vector1, VectorN vector2)
		{
			return Operate(vector1, vector2, delegate (float n1, float n2)
			{
				return n1 * n2;
			});
		}

		public static VectorN operator *(VectorN vector, float scalar)
		{
			return Operate(vector, scalar, delegate (float n1, float n2)
			{
				return n1 * n2;
			});
		}

		public static VectorN operator *(float scalar, VectorN vector)
		{
			return vector * scalar;
		}

		public static VectorN operator /(VectorN vector1, VectorN vector2)
		{
			return Operate(vector1, vector2, delegate (float n1, float n2)
			{
				return n1 / n2;
			});
		}

		public static VectorN operator /(VectorN vector, float scalar)
		{
			return Operate(vector, scalar, delegate (float n1, float n2)
			{
				return n1 / n2;
			});
		}

		private static bool IsEqual(VectorN vector1, VectorN vector2, bool equality)
		{
			if (vector1.Size == vector2.Size)
			{
				for (int i = 0; i < vector1.Size; i++)
				{
					if (vector1[i] != vector2[i])
						return !equality;
				}

				return equality;
			}

			return !equality;
		}

		public static bool operator ==(VectorN vector1, VectorN vector2)
		{
			return IsEqual(vector1, vector2, true);
		}

		public static bool operator !=(VectorN vector1, VectorN vector2)
		{
			return IsEqual(vector1, vector2, false);
		}

		#endregion OPERATORS

		#region INSTANCE

		/// <summary>
		/// Returns the vector with absolute values.
		/// </summary>
		public VectorN Abs()
		{
			float[] lValues = new float[Size];

			for (int i = 0; i < Size; i++)
			{
				lValues[i] = MathF.Abs(values[i]);
			}

			return new VectorN(lValues);
		}

		/// <summary>
		/// Rounds up the length of the vector.
		/// </summary>
		public void CeilLength()
		{
			float l = LengthSquared();

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				float ceil = MathF.Ceiling(l);

				for (int i = 0; i < Size; i++)
				{
					values[i] /= l / ceil;
				}
			}
		}

		/// <summary>
		/// Rounds up the values of the vector.
		/// </summary>
		public void CeilValues()
		{
			for (int i = 0; i < Size; i++)
			{
				values[i] = MathF.Ceiling(values[i]);
			}
		}

		/// <summary>
		/// Clamps the length of the vector between min and max.
		/// </summary>
		public void ClampLength(float min, float max)
		{
			float l = LengthSquared();

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				float clamp = MathT.Clamp(l, min, max);

				for (int i = 0; i < Size; i++)
				{
					values[i] /= l / clamp;
				}
			}
		}

		/// <summary>
		/// Clamps the values of the vector between the corresponding min and max in range.
		/// </summary>
		public void ClampValues(params (float min, float max)[] range)
		{
			if (range.Length != Size)
				throw new ArgumentException($"Range must have {Size} elements in it (the same dimensions as the vector).");

			for (int i = 0; i < Size; i++)
			{
				values[i] = MathT.Clamp(values[i], range[i].min, range[i].max);
			}
		}

		/// <summary>
		/// Clamps the values of the vector between min and max.
		/// </summary>
		public void ClampValuesUniform(float min, float max)
		{
			for (int i = 0; i < Size; i++)
			{
				values[i] = MathT.Clamp(values[i], min, max);
			}
		}

		/// <summary>
		/// Returns the cross product between the vector (this) and the given vector.
		/// </summary>
		public VectorN Cross(VectorN vector)
		{
			//if (Dimension != vector.Dimension)
			//	throw new ArgumentException("Both vectors must have the same dimension.");

			//VectorN result = new VectorN(Dimension);
			//float length = MathF.Floor(Dimension / 2f);
			//float coord = 0f;

			//for (int i = 0; i < Dimension; i++)
			//{
			//	coord = 0f;

			//	for (int j = 0; j < length; j++)
			//	{
			//		coord += this[Congruence(i + j, Dimension) + 1] - vector[Congruence()]
			//	}

			//	result[i] = coord;
			//}

			throw new NotImplementedException("Don't use it, it's a work in progress.");
		}

		/// <summary>
		/// Returns the distance between the vector (this) and the given vector.
		/// </summary>
		public float Distance(VectorN vector)
		{
			if (Size != vector.Size)
				throw new ArgumentException("Both vectors must have the same dimension.");

			float l = 0f;

			for (int i = 0; i < Size; i++)
			{
				l += (this[i] - vector[i]) * (this[i] - vector[i]);
			}

			return MathF.Sqrt(l);
		}

		/// <summary>
		/// Returns the squared distance between the vector (this) and the given vector.
		/// </summary>
		public float DistanceSquared(VectorN vector)
		{
			if (Size != vector.Size)
				throw new ArgumentException("Both vectors must have the same dimension.");

			float l = 0f;

			for (int i = 0; i < Size; i++)
			{
				l += (this[i] - vector[i]) * (this[i] - vector[i]);
			}

			return l;
		}

		/// <summary>
		/// Returns the dot product of the vector (this) and the given vector.
		/// </summary>
		public float Dot(VectorN vector)
		{
			if (Size != vector.Size)
				throw new ArgumentException("Both vectors must have the same dimension.");

			float result = 0f;

			for (int i = 0; i < Size; i++)
			{
				result += this[i] * vector[i];
			}

			return result;
		}

		public override bool Equals(object obj)
		{
			if (obj is VectorN)
				return (VectorN)obj == this;
			return false;
		}

		/// <summary>
		/// Rounds the length of the vector downward.
		/// </summary>
		public void FloorLength()
		{
			float l = LengthSquared();

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				float floor = MathF.Floor(l);

				for (int i = 0; i < Size; i++)
				{
					values[i] /= l / floor;
				}
			}
		}

		/// <summary>
		/// Rounds the values of the vector downward.
		/// </summary>
		public void FloorValues()
		{
			for (int i = 0; i < Size; i++)
			{
				values[i] = MathF.Floor(values[i]);
			}
		}

		/// <summary>
		/// Says if the vector is normalized (if the length is equal to 1).
		/// </summary>
		public bool IsNormalized() => LengthSquared() == 1;

		/// <summary>
		/// Lerp between this and to by weight (weight is clamped between 0 and 1).
		/// </summary>
		public VectorN Lerp(VectorN to, float weight)
		{
			return LerpUnclamped(to, MathT.Clamp(weight, 0, 1));
		}

		/// <summary>
		/// Lerp between this and to by a random number between 0 and 1.
		/// </summary>
		public VectorN LerpRand(VectorN to)
		{
			return LerpUnclamped(to, (float)new Random().NextDouble());
		}

		/// <summary>
		/// Lerp between this and to by weight.
		/// </summary>
		public VectorN LerpUnclamped(VectorN to, float weight)
		{
			if (Size != to.Size)
				throw new ArgumentException("Both vectors must have the same dimension.");

			VectorN result = new VectorN(Size);

			for (int i = 0; i < Size; i++)
			{
				result[i] = values[i] + weight * (to[i] - values[i]);
			}

			return result;
		}

		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		public float Length()
		{
			float l = 0f;

			for (int i = 0; i < Size; i++)
			{
				l += values[i] * values[i];
			}

			return MathF.Sqrt(l);
		}

		/// <summary>
		/// Returns the squared length of the vector.
		/// </summary>
		public float LengthSquared()
		{
			float l = 0f;

			for (int i = 0; i < Size; i++)
			{
				l += values[i] * values[i];
			}

			return l;
		}

		/// <summary>
		/// Performs a modulus operation on each value, where the result is in ]-mod, 0].
		/// </summary>
		public VectorN NegMod(float mod)
		{
			VectorN result = new VectorN(Size);

			for (int i = 0; i < Size; i++)
			{
				result[i] = MathT.Congruence(values[i], mod, false);
			}

			return result;
		}

		/// <summary>
		/// Performs a modulus operation on each value, where the result is in ]-modv[i], 0].
		/// </summary>
		public VectorN NegModv(VectorN modv)
		{
			if (Size != modv.Size)
				throw new ArgumentException("Both vectors must have the same dimension.");

			VectorN result = new VectorN(Size);

			for (int i = 0; i < Size; i++)
			{
				result[i] = MathT.Congruence(values[i], modv[i], false);
			}

			return result;
		}

		/// <summary>
		/// Sets the length of the vector to length.
		/// </summary>
		public void Normalize(float length)
		{
			float l = LengthSquared();

			if (l != 0)
			{
				l = MathF.Sqrt(l);

				for (int i = 0; i < Size; i++)
				{
					values[i] /= l / length;
				}
			}
		}

		/// <summary>
		/// Returns the normalized vector.
		/// </summary>
		public VectorN Normalized()
		{
			VectorN result = new VectorN(Size);
			float l = LengthSquared();

			if (l == 0)
				throw new InvalidOperationException("The vector's length must be greater than 0");

			l = MathF.Sqrt(l);

			for (int i = 0; i < Size; i++)
			{
				result[i] = values[i] / l;
			}

			return result;
		}

		/// <summary>
		/// Performs a modulus operation on each value, where the result is in [0, mod[.
		/// </summary>
		public VectorN PosMod(float mod)
		{
			VectorN result = new VectorN(Size);

			for (int i = 0; i < Size; i++)
			{
				result[i] = MathT.Congruence(values[i], mod);
			}

			return result;
		}

		/// <summary>
		/// Performs a modulus operation on each value, where the result is in [0, modv[i][.
		/// </summary>
		public VectorN PosModv(VectorN modv)
		{
			if (Size != modv.Size)
				throw new ArgumentException("Both vectors must have the same dimension.");

			VectorN result = new VectorN(Size);

			for (int i = 0; i < Size; i++)
			{
				result[i] = MathT.Congruence(values[i], modv[i]);
			}

			return result;
		}

		/// <summary>
		/// Returns the vector with its values to the power of pow.
		/// </summary>
		public VectorN Pow(float pow)
		{
			VectorN result = new VectorN(Size);

			for (int i = 0; i < Size; i++)
			{
				result[i] = MathF.Pow(values[i], pow);
			}

			return result;
		}

		/// <summary>
		/// Rounds the length of the vector.
		/// </summary>
		public void RoundLength()
		{
			float l = LengthSquared();

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				float round = MathF.Round(l);

				for (int i = 0; i < Size; i++)
				{
					values[i] /= l / round;
				}
			}
		}

		/// <summary>
		/// Rounds the values of the vector.
		/// </summary>
		public void RoundValues()
		{
			for (int i = 0; i < Size; i++)
			{
				values[i] = MathF.Round(values[i]);
			}
		}

		#endregion INSTANCE
	}
}
