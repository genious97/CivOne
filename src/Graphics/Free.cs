// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System.Collections.Generic;
using System.Linq;
using CivOne.Enums;
using CivOne.IO;

namespace CivOne.Graphics
{
	internal class Free
	{
		private Bytemap _panelGrey, _panelBlue;
		private Bytemap _landBase, _seaBase, _city, _fortify;
		private Bytemap[] _terrain = new Bytemap[10];

		private IEnumerable<byte> GenerateNoise(params byte[] values)
		{
			Random r = new Random(0x4701);
			while (true)
			{
				yield return values[r.Next(values.Length)];
			}
		}

		private IEnumerable<byte> GenerateUnit()
		{
			for (int yy = 0; yy < 16; yy++)
			for (int xx = 0; xx < 16; xx++)
			{
				if ((xx == 0 || xx == 15 || yy == 0 || yy == 15) || ((xx == 1 || xx == 14) && (yy == 1 || yy == 14)))
				{
					yield return 0;
				}
				else if (xx == 1 || yy == 14)
				{
					yield return 15;
				}
				else if (xx == 14 || yy == 1)
				{
					yield return 2;
				}
				else
				{
					yield return 10;
				}
			}
		}

		public Bytemap PanelGrey
		{
			get
			{
				if (_panelGrey == null)
				{
					_panelGrey = new Bytemap(16, 16).FromByteArray(GenerateNoise(7, 22).Take(16 * 16).ToArray());
				}
				return _panelGrey;
			}
		}

		public Bytemap PanelBlue
		{
			get
			{
				if (_panelBlue == null)
				{
					_panelBlue = new Bytemap(16, 16).FromByteArray(GenerateNoise(57, 9).Take(16 * 16).ToArray());
				}
				return _panelBlue;
			}
		}

		public Bytemap LandBase
		{
			get
			{
				if (_landBase == null)
				{
					_landBase = new Bytemap(16, 16).FromByteArray(GenerateNoise(37, 38, 39).Take(16 * 16).ToArray());
				}
				return _landBase;
			}
		}

		public Bytemap OceanBase
		{
			get
			{
				if (_seaBase == null)
				{
					_seaBase = new Bytemap(16, 16).FromByteArray(GenerateNoise(77, 78, 79).Take(16 * 16).ToArray());
				}
				return _seaBase;
			}
		}

		public Bytemap Plains
		{
			get
			{
				if (_terrain[(int)Terrain.Plains] == null)
				{
					_terrain[(int)Terrain.Plains] = new Bytemap(16, 16).FromByteArray(GenerateNoise(0, 0, 0, 47, 48, 0, 0, 0, 0).Take(16 * 16).ToArray());
				}
				return _terrain[(int)Terrain.Plains];
			}
		}

		public Bytemap Forest
		{
			get
			{
				if (_terrain[(int)Terrain.Forest] == null)
				{
					_terrain[(int)Terrain.Forest] = new Bytemap(16, 16).FromByteArray(
						0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
						0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
						0,  0,  0,  0,  0,  0,  5,  5,  0,  0,  0,  5,  5,  0,  0,  0,
						0,  0,  0,  0,  0,  5, 39, 38,  5,  0,  5, 38, 39,  5,  0,  0,
						0,  0,  0,  0,  0,  5,  5, 39,  5,  0,  5, 39, 38,  5,  0,  0,
						0,  0,  0,  0,  5, 39, 38,  5, 39,  5, 38, 38, 39, 38,  5,  0,
						0,  0,  0,  0,  5, 38, 39,  5, 38,  5, 39, 39, 38, 38,  5,  0,
						0,  0,  0,  5, 39, 39, 38, 39,  5, 38, 38, 38, 39, 38, 38,  5,
						0,  0,  0,  5, 39, 38, 38, 38,  5,  5,  5, 47, 47,  5,  5,  5,
						0,  0,  5, 38, 38, 38, 38, 39, 39,  5, 39,  5,  5,  0,  0,  0,
						0,  0,  5, 39, 38, 39, 39, 38, 38,  5,  5,  5,  0,  0,  0,  0,
						0,  5, 38, 38, 39, 38, 39, 39, 38, 39,  5,  0,  0,  0,  0,  0,
						0,  5,  5,  5, 38, 39, 39, 38,  5,  5,  5,  0,  0,  0,  0,  0,
						0,  0,  0,  0,  5, 47, 47,  5,  0,  0,  0,  0,  0,  0,  0,  0,
						0,  0,  0,  0,  0,  5,  5,  0,  0,  0,  0,  0,  0,  0,  0,  0,
						0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0
					);
				}
				return _terrain[(int)Terrain.Forest];
			}
		}

		public Bytemap City
		{
			get
			{
				if (_city == null)
				{
					Random r = new Random(0x4701);
					_city = new Picture(16, 16)
						.DrawLine(7, 3, 11, 3)
						.DrawLine(4, 5, 9, 5)
						.DrawLine(3, 7, 11, 7)
						.DrawLine(5, 9, 9, 9)
						.DrawLine(3, 11, 6, 11)
						.DrawLine(3, 6, 3, 8)
						.DrawLine(7, 3, 7, 11)
						.DrawLine(11, 5, 11, 11).Bitmap;
				}
				return _city;
			}
		}

		public Bytemap Fortify
		{
			get
			{
				if (_fortify == null)
				{
					_fortify = new Bytemap(16, 16)
						.FromByteArray(GenerateNoise(26, 27, 28).Take(16 * 16).ToArray())
						.AddLayer(new Bytemap(14, 14).FromByteArray(GenerateNoise(24, 25, 26).Take(14 * 14).ToArray()))
						.FillRectangle(2, 2, 12, 12, 0);
				}
				return _fortify;
			}
		}

		public Bytemap Fog(Direction direction)
		{
			Bytemap output = new Bytemap(16, 16);
			switch(direction)
			{
				case Direction.West:
					output.AddLayer(new Bytemap(3, 16).FromByteArray(GenerateNoise(0, 28, 29, 30, 31).Take(3 * 16).ToArray()), 0, 0);
					break;
				case Direction.South:
					output.AddLayer(new Bytemap(16, 3).FromByteArray(GenerateNoise(28, 0, 29, 30, 31).Take(16 * 3).ToArray()), 0, 13);
					break;
				case Direction.East:
					output.AddLayer(new Bytemap(3, 16).FromByteArray(GenerateNoise(28, 29, 0, 30, 31).Take(3 * 16).ToArray()), 13, 0);
					break;
				case Direction.North:
					output.AddLayer(new Bytemap(16, 3).FromByteArray(GenerateNoise(28, 29, 30, 0, 31).Take(16 * 3).ToArray()), 0, 0);
					break;
			}
			return output;
		}

		public Bytemap GetUnit(UnitType type)
		{
			Bytemap output = new Bytemap(16, 16).FromByteArray(GenerateUnit().ToArray());
			char text = ' ';
			switch (type)
			{
				case UnitType.Settlers:
					output.AddLayer(new Bytemap(10, 10).FromByteArray(
						0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
						0,  5,  5,  5,  5,  5,  5,  5,  0,  0,
						5, 15, 15,  8, 15, 15,  8,  7,  5,  0,
						5, 15, 15,  8, 15, 15,  8,  7,  5,  0,
						5, 15, 15, 15, 15, 15,  8,  7,  5,  0,
						0,  5,  8,  8, 15, 15,  8,  8,  5,  0,
						0,  8,  5,  0,  8,  8,  5,  0,  8,  0,
						0,  8,  0,  0,  8,  8,  0,  0,  8,  0,
						0,  0,  8,  8,  0,  0,  8,  8,  0,  0,
						0,  0,  0,  0,  0,  0,  0,  0,  0,  0
					), 3, 3);
					break;
				case UnitType.Militia:
					output.AddLayer(new Bytemap(10, 10).FromByteArray(
						0,  0,  0,  0,  0,  0,  0,  0,  0,  0,
						0,  0,  0,  4,  4,  4,  0,  0,  0,  0,
						0,  0,  4,  4,  4,  4,  4,  0,  0,  0,
						0,  0,  4, 47, 47, 47,  4,  0, 21,  0,
						0,  0, 47, 47, 47, 47, 47,  0, 21, 21,
						0,  0, 47,  9, 47,  9, 47,  0, 21, 21,
						0,  0, 47, 47, 47, 47, 47,  0,  0, 21,
						0,  0, 47,  8,  8,  8, 47,  0, 41, 41,
						0,  0,  5, 47, 47, 47,  5,  0,  5,  5,
						0,  5,  5,  5,  5,  5,  5,  5,  5,  0
					), 3, 3);
					break;
				case UnitType.Phalanx: text = 'P'; break;
				case UnitType.Legion: text = 'L'; break;
				case UnitType.Musketeers: text = 'M'; break;
				case UnitType.Riflemen: text = 'R'; break;
				case UnitType.Cavalry: text = 'c'; break;
				case UnitType.Knights: text = 'K'; break;
				case UnitType.Catapult: text = 'C'; break;
				case UnitType.Cannon: text = 'X'; break;
				case UnitType.Chariot: text = 'W'; break;
				case UnitType.Armor: text = 'a'; break;
				case UnitType.MechInf: text = 'I'; break;
				case UnitType.Artillery: text = 'A'; break;
				case UnitType.Fighter: text = 'F'; break;
				case UnitType.Bomber: text = 'B'; break;
				case UnitType.Trireme: text = 'T'; break;
				case UnitType.Sail: text = 's'; break;
				case UnitType.Frigate: text = 'f'; break;
				case UnitType.Ironclad: text = 'i'; break;
				case UnitType.Cruiser: text = 'Y'; break;
				case UnitType.Battleship: text = 'Z'; break;
				case UnitType.Submarine: text = 'U'; break;
				case UnitType.Carrier: text = 'G'; break;
				case UnitType.Transport: text = 'H'; break;
				case UnitType.Nuclear: text = 'N'; break;
				case UnitType.Diplomat: text = 'D'; break;
				case UnitType.Caravan: text = 't'; break;
			}
			if (text != ' ')
			{
				output.AddLayer(
					new Picture(16, 16)
						.DrawText(text.ToString(), 0, 8, 8, 5, TextAlign.Center)
						.DrawText(text.ToString(), 0, 7, 8, 4, TextAlign.Center)
						.Bitmap);
			}
			return output;
		}

		private static Free _instance;
		public static Free Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Free();
				return _instance;
			}
		}

		private Free()
		{
		}
	}
}