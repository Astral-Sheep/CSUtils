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
	/// </summary>
	public interface IVector
	{
		public IVector Abs();
		public void CeilLength();
		public void CeilValues();
		public void ClampLength(float min, float max);
		public void ClampValuesUniform(float min, float max);
		public float Cross(IVector vector);
		public float Distance(IVector vector);
		public float DistanceSquared(IVector vector);
		public float Dot(IVector vector);
		public void FloorLength();
		public void FloorValues();
		public bool IsNormalized();
		public IVector Lerp(IVector to, float ratio);
		public IVector LerpRand(IVector to);
		public IVector LerpUnclamped(IVector to, float ratio);
		public float Length();
		public float LengthSquared();
		public IVector NegMod(float mod);
		public IVector NegModv(IVector modv);
		public void Normalize(float length = 1);
		public IVector Normalized(float length = 1);
		public IVector PosMod(float mod);
		public IVector PosModv(IVector modv);
		public IVector Pow(float pow);
		public void RoundLength();
		public void RoundValues();
		public IVector Sign();
	}
}
