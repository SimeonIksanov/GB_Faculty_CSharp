using System;
using System.Collections.Generic;
using System.IO;

namespace ScanApp
{
    public class CompositeSaver : ISaveStrategy
    {
        private readonly IEnumerable<ISaveStrategy> _saveStrategies;

        public CompositeSaver(IEnumerable<ISaveStrategy> saveStrategies)
        {
            _saveStrategies = saveStrategies;
        }

        public void Save(Stream stream)
        {
            foreach (var saver in _saveStrategies)
            {
                stream.Position = 0;
                saver.Save(stream);
            }
        }
    }
}
