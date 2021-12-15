using EntityLib;

namespace ConsoleUI
{
    public class UI : IViewConsole
    {
        const int width = 130;
        const int pageSize = 15;

        //IViewData _viewData;
        public void Display(IViewData viewData)
        {
            Console.Clear();
            string hBorder = "+" + new string('-', width) + "+";
            string vBorder = "|" + new string(' ', width) + "|";

            Console.WriteLine(hBorder);
            Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);

            for (int i = 0; i < pageSize + 1; i++)
                Console.WriteLine(vBorder);

            Console.WriteLine(hBorder);
            Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);
            for (int i = 0; i < 6; i++)
                Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);
            Console.WriteLine(vBorder);
            Console.WriteLine(hBorder);

            // Fill panels
            var bottom = Console.GetCursorPosition();
            if (viewData != null)
            {
                Console.SetCursorPosition(left: 4, top: 1); Console.Write(viewData.Path);
                for (int i = 0; i < viewData.DirectoryListing.Length; i++)
                {
                    Console.SetCursorPosition(left: 4, top: 3 + i);
                    Console.Write(viewData.DirectoryListing[i]);
                }
            }

            Console.SetCursorPosition(left: 4, top: bottom.Top - 11);
            Console.Write("Information:");

            if (viewData?.FileSystemItemInfo != null)
            {
                Console.SetCursorPosition(left: 4, top: bottom.Top - 9);
                Console.Write("Item: {0}", viewData.FileSystemItemInfo.Path);

                Console.SetCursorPosition(left: 4, top: bottom.Top - 8);
                Console.Write("CreationTime: {0}", viewData.FileSystemItemInfo.CreationTime);

                Console.SetCursorPosition(left: 4, top: bottom.Top - 7);
                Console.Write("LastAccessTime: {0}\tLastWriteTime: {1}", viewData.FileSystemItemInfo.LastAccessTime, viewData?.FileSystemItemInfo.LastWriteTime);

                Console.SetCursorPosition(left: 4, top: bottom.Top - 6);
                Console.Write("Attributes: {0}", viewData?.FileSystemItemInfo.Attributes.ToString());

                Console.SetCursorPosition(left: 4, top: bottom.Top - 5);
                Console.Write("Size in bytes: {0}", viewData?.FileSystemItemInfo.Size);

                if (viewData?.FileSystemItemInfo.TextFileInfo != null)
                {
                    Console.SetCursorPosition(left: 4, top: bottom.Top - 4);
                    Console.Write($"LineCount: {viewData.FileSystemItemInfo.TextFileInfo.LineCount} " +
                        $"ParagraphCount: {viewData.FileSystemItemInfo.TextFileInfo.ParagraphCount} " +
                        $"WordCount: {viewData.FileSystemItemInfo.TextFileInfo.WordCount} " +
                        $"SpaceCount: {viewData.FileSystemItemInfo.TextFileInfo.SpaceCount}");
                }

                Console.SetCursorPosition(left: 0, top: bottom.Top);
            }

            Console.SetCursorPosition(left: 4, top: bottom.Top - 2);
            Console.Write("> ");
        }

    }
}
