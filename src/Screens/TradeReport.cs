// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System;
using System.Linq;
using CivOne.Enums;
using CivOne.Events;
using CivOne.GFX;
using CivOne.Interfaces;
using CivOne.Templates;

namespace CivOne.Screens
{
	internal class TradeReport : BaseStatusScreen
	{
		private const char LUXURIES = '\\';
		private const char GOLD = '$';
		private const char SCIENCE = '~';

		private readonly City[] _cities;

		private bool _update = true;
		private int _page = 0;

		private void DrawCityTrade()
		{
			int totalIncome = _cities.Sum(c => c.Taxes);
			int totalScience = _cities.Sum(c => c.Science);

			_canvas.DrawText("City Trade", 0, 15, 8, 32);

			int yy = 40;
			//foreach (City city in _cities)
			//{
			for (int i = (_page++ * 18); i < _cities.Length && i < (_page * 18); i++)
			{
				City city = _cities[i];

				_canvas.DrawText(city.Name, 0, 5, 16, yy + 1);
				_canvas.DrawText(city.Name, 0, 15, 16, yy);
				
				_canvas.DrawText($"{city.Luxuries}{LUXURIES}/{city.Taxes}{GOLD}/{city.Science}{SCIENCE}", 0, 10, 86, yy);

				yy += Resources.Instance.GetFontHeight(0);
			}
			
			if ((_page * 18) >= _cities.Length)
			{
				yy += 4;
				_canvas.DrawText($"Total Income: {totalIncome}$", 0, 10, 8, yy);
				yy += Resources.Instance.GetFontHeight(0);
				if (totalScience > 0 && yy <= 188)
				{
					_canvas.DrawText($"Discoveries: {(int)Math.Ceiling((double)HumanPlayer.ScienceCost / totalScience)} turns", 0, 10, 8, yy);
				}
			}
		}
		
		private void DrawMaintenanceCost()
		{
			int totalCost = _cities.Sum(c => c.TotalMaintenance);

			_canvas.DrawText("Maintenance Cost", 0, 15, 160, 32);

			int yy = 40;
			foreach (Building entry in Enum.GetValues(typeof(Building)))
			{
				int count = _cities.SelectMany(c => c.Buildings).Count(b => b.Id == (int)entry);
				if (count == 0) continue;

				IBuilding building = _cities.SelectMany(c => c.Buildings).First(b => b.Id == (int)entry);
				if (building.Maintenance == 0) continue;

				_canvas.DrawText($"{count} {building.Name}, {building.Maintenance * count}$", 0, 14, 160, yy);
				yy += Resources.Instance.GetFontHeight(0);
			}

			yy += 4;
			_canvas.DrawText($"Total Cost: {totalCost}$", 0, 14, 160, yy);
		}
		
		public override bool HasUpdate(uint gameTick)
		{
			if (!_update) return false;

			_canvas.FillRectangle(2, 0, 32, 320, 168);
			DrawCityTrade();
			if ((_page * 18) >= _cities.Length)
			{
				DrawMaintenanceCost();
			}

			AddLayer(Portrait[(int)Advisor.Domestic], 278, 2);

			_update = false;
			return true;
		}

		private bool NextPage()
		{
			if ((_page * 18) < _cities.Length)
			{
				_update = true;
			}
			else
			{
				Destroy();
			}
			return true;
		}
		
		public override bool KeyDown(KeyboardEventArgs args)
		{
			return NextPage();
		}
		
		public override bool MouseDown(ScreenEventArgs args)
		{
			return NextPage();
		}

		public TradeReport() : base("TRADE REPORT", 2)
		{
			_cities = Game.Instance.GetCities().Where(c => c.Owner == Game.Instance.PlayerNumber(HumanPlayer)).ToArray();
		}
	}
}