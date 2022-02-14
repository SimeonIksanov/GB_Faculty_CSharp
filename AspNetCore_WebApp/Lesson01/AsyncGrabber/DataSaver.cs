using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DataSaver
    {
        public DataSaver()
        {
        }

        public async Task SaveToFileAsync(string filePath, IEnumerable<Post> posts)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendJoin("\n\n", posts);
            await File.WriteAllTextAsync(filePath, sb.ToString());
        }
    }
}
