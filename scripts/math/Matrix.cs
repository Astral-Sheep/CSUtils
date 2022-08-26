using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a matrix
	/// </summary>
	public struct Matrix
	{
		#region PROPERTIES

		public int Columns
		{
			get => _columns;
			private set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The number of columns must be greater than 0.");

				_columns = value;
			}
		}

		public int Lines
		{
			get => _lines;
			private set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The number of lines must be greater than 0.");

				_lines = value;
			}
		}

		public float this[int line, int column]
		{
			get
			{
				if (line < 0 || line >= Lines)
					throw new ArgumentOutOfRangeException($"The line must greater than or equal to 0 and lesser than {Lines}");

				if (column < 0 || column >= Columns)
					throw new ArgumentOutOfRangeException($"The column must greater than or equal to 0 and lesser than {Columns}");

				return values[line, column];
			}
			set
			{
				if (line < 0 || line >= Lines)
					throw new ArgumentOutOfRangeException($"The line must greater than or equal to 0 and lesser than {Lines}");
				
				if (column < 0 || column >= Columns)
					throw new ArgumentOutOfRangeException($"The column must greater than or equal to 0 and lesser than {Columns}");

				values[line, column] = value;
			}
		}

		#endregion PROPERTIES

		private int _columns;
		private int _lines;
		private float[,] values;

		public Matrix(int lines, int columns, params float[] numbers)
		{
			_lines = lines;
			_columns = columns;

			if (numbers.Length != lines * columns)
				throw new TypeInitializationException("The number of lines and columns doesn't correspond to the number of values given.", new ArgumentException());

			values = new float[lines, columns];
			int index = 0;

			for (int i = 0; i < lines; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					values[i, j] = numbers[index];
					++index;
				}
			}
		}

		public Matrix(Matrix matrix)
		{
			_lines = matrix.Lines;
			_columns = matrix.Columns;

			values = new float[_lines, _columns];

			for (int i = 0; i < _lines; i++)
			{
				for (int j = 0; j < _columns; j++)
				{
					values[i, j] = matrix[i, j];
				}
			}
		}
	}
}
