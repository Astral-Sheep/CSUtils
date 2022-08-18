using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Interface with vectorial methods.
	/// T is the type of the vector inheriting it.
	/// </summary>
	public interface IVector<T>
	{
		public T Abs();
		public void CeilLength();
		public void CeilValues();
		public void ClampLength(float min, float max);
		public void ClampValuesUniform(float min, float max);
		public float Cross(T vector);
		public float Distance(T vector);
		public float DistanceSquared(T vector);
		public float Dot(T vector);
		public void FloorLength();
		public void FloorValues();
		public bool IsNormalized();
		public float Length();
		public float LengthSquared();
		public T Lerp(T to, float ratio);
		public T LerpRand(T to);
		public T LerpUnclamped(T to, float ratio);
		public T NegMod(float mod);
		public T NegModv(T modv);
		public void Normalize(float length = 1);
		public T Normalized(float length = 1);
		public T PosMod(float mod);
		public T PosModv(T modv);
		public T Pow(float pow);
		public void RoundLength();
		public void RoundValues();
		public T Sign();
	}
}
