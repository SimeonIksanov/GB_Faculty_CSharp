using System;
using System.Collections.Generic;

namespace BuildingLib
{
	public class Creator
	{
		public static Dictionary<int, Building> AllBuildings = new Dictionary<int, Building>();
		private Creator()
		{
		}

		public static Building CreateBuiling()
		{
			var b = new Building(27, 9, 108, 2);
			AddToHashtable(b);
			return b;
		}

		public static Building CreateBuiling(int numberOfStoreys)
		{
			var b = new Building(3 * numberOfStoreys, numberOfStoreys, numberOfStoreys*4, 1);
			AddToHashtable(b);
			return b;
		}

		public static Building CreateBuiling(int numberOfStoreys, int numberOfEntrances)
		{
			var b = new Building(
					height: 3 * numberOfStoreys,
					numberOfStoreys: numberOfStoreys,
					numberOfFlats: numberOfStoreys * 4 * numberOfEntrances,
					numberOfEntrances: numberOfEntrances);
			AddToHashtable(b);
			return b;
		}

		public static void Remove(int id)
		{
			if(AllBuildings.ContainsKey(id))
			{
				AllBuildings.Remove(id);
			}
		}
		private static void AddToHashtable(Building b)
		{
			AllBuildings.Add(b.Id, b);
		}
	}
}
