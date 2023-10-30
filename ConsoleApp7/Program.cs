class Program
{
    static int CreateFileAndCountCharacters(string filename, string content)
    {
        File.WriteAllText(filename, content);

        return content.Length;
    }

    static async Task Main()
    {
        List<(string, string)> filesContent = new List<(string, string)>
        {
            ("txt1.txt", "Текст для 1 файлу"),
            ("txt2.txt", "Текст для 2 файлу"),
            ("txt3.txt", "Текст для 3 файлу")
        };

        List<Task<int>> results = new List<Task<int>>();

        foreach (var fileContent in filesContent)
        {
            results.Add(Task.Run(() => CreateFileAndCountCharacters(fileContent.Item1, fileContent.Item2)));
        }

        int totalCharacterCount = 0;

        for (int i = 0; i < results.Count; i++)
        {
            int characterCount = await results[i];

            if (characterCount > 0)
            {
                Console.WriteLine($"Файл {filesContent[i].Item1} мiстить {characterCount} символiв.");

                if (File.Exists(filesContent[i].Item1))
                {
                    string fileContent = File.ReadAllText(filesContent[i].Item1);
                    Console.WriteLine($"Вмiст файлу {filesContent[i].Item1}:\n{fileContent}");
                }
                else
                {
                    Console.WriteLine($"ERROR: Не вдалося вiдкрити файл {filesContent[i].Item1} для читання.");
                }

                totalCharacterCount += characterCount;
            }
        }

        Console.WriteLine($"Загальний розмiр файлiв: {totalCharacterCount} символiв.");
    }
}

