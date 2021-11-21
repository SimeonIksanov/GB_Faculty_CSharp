using System;

namespace BuildingLib
{
    public class Building
    {
        private const int STOREYHEIGHT = 3; //in meters

        private static int _idCounter = 0;

        private readonly int _id;
        private readonly int _height;
        private readonly int _numberOfStoreys;
        private readonly int _numberOfFlats;
        private readonly int _numberOfEntrances;

        internal Building(int height, int numberOfStoreys, int numberOfFlats, int numberOfEntrances)
        {
            IncrementBuildingCounter();

            _id = _idCounter;
            _height = height;
            _numberOfStoreys = numberOfStoreys;
            _numberOfFlats = numberOfFlats;
            _numberOfEntrances = numberOfEntrances;
        }

        private static void IncrementBuildingCounter()
        {
            _idCounter++;
        }

        public int Id { get => _id; }

        public int Height { get => _height; }

        public int NumberOfStoreys { get => _numberOfStoreys; }

        public int NumberOfFlats { get => _numberOfFlats; }

        public int NumberOfEntrances { get => _numberOfEntrances; }

        public int GetHeightOfStorey(int storey)
        {
            if (storey < 1 || storey > _numberOfStoreys)
            {
                throw new ArgumentException("wrong value for storey");
            }
            return storey * STOREYHEIGHT;
        }

        public int GetNumberOfFlatsInEntrance()
        {
            return _numberOfFlats / _numberOfEntrances;
        }

        public int GetNumberOfFlatsInStorey()
        {
            return _numberOfFlats / _numberOfStoreys;
        }

        public override string ToString()
        {
            return $"Building number {Id} is {Height} meters high, has {NumberOfStoreys} storeys, {NumberOfFlats} flats and {NumberOfEntrances} entrances";
        }
    }
}
