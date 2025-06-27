class Program
{
    static Dictionary<string, string> vocabulary = new Dictionary<string, string>();
    static Random random = new Random();

    static void Main()
    {
        LoadVocabulary("D:\\Rider\\VocabularyTrainer\\VocabularyTrainer\\VocabularyList.csv");

        while (true)
        {
            Console.WriteLine("Choose quiz mode:");
            Console.WriteLine("1. German to English");
            Console.WriteLine("2. English to German");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            string? choice = Console.ReadLine()?.Trim();

            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    TranslateFromGermanToEnglish();
                    break;
                case "2":
                    TranslateFromEnglishToGerman();
                    break;
                default:
                    Console.WriteLine("Invalid choice.\n");
                    break;
            }
        }
    }

    static void LoadVocabulary(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' not found.");
            Environment.Exit(1);
        }

        foreach (var line in File.ReadLines(filename))
        {
            var parts = line.Split(',', 2);
            if (parts.Length == 2)
            {
                var german = parts[0].Trim();
                var english = parts[1].Trim();
                if (!vocabulary.ContainsKey(german))
                    vocabulary.Add(german, english);
            }
        }

        Console.WriteLine($"Loaded {vocabulary.Count} vocabulary entries.\n");
    }

    static KeyValuePair<string, string> GetRandomWord()
    {
        int index = random.Next(vocabulary.Count);
        return vocabulary.ElementAt(index);
    }

    static void TranslateFromGermanToEnglish()
    {
        Console.WriteLine("\n--- Trasnslate from German to English ---");
        Console.WriteLine("Type 'exit' to stop.\n");

        while (true)
        {
            var word = GetRandomWord();
            Console.Write($"Translate '{word.Key}': ");
            var input = Console.ReadLine()?.Trim();

            if (input?.ToLower() == "exit") break;

            if (string.Equals(input, word.Value, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Wrong. Correct answer: {word.Value}\n");
            }
            Console.ResetColor();
        }
        Console.WriteLine();
    }

    static void TranslateFromEnglishToGerman()
    {
        Console.WriteLine("\n---Trasnslate from English to German ---");
        Console.WriteLine("Type 'exit' to stop.\n");
        
        var reversed = vocabulary.ToDictionary(kv => kv.Value, kv => kv.Key);

        var keys = reversed.Keys.ToList();

        while (true)
        {
            var engWord = keys[random.Next(keys.Count)];
            Console.Write($"Translate '{engWord}': ");
            var input = Console.ReadLine()?.Trim();

            if (input?.ToLower() == "exit") break;

            if (reversed.TryGetValue(engWord, out var correctGerman) &&
                string.Equals(input, correctGerman, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Wrong. Correct answer: {correctGerman}\n");
            }
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}
