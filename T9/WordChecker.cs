namespace TypoFixer;

public class WordChecker
{
    private string filePath;
    private string _givenWord;
    private StreamReader _reader;
    private MaxHeap<Node> _top5;

    public WordChecker(string wordsListFilePath = "C:\\Users\\q1112\\RiderProjects\\T9\\T9\\Files\\words_list.txt")
    {
        if (!File.Exists(wordsListFilePath))
            throw new Exception("Invalid filepath to list of words txt file");
        filePath = wordsListFilePath;
    }

    public string[] GetTop5SimilarStartingWithSameLetter(string word)
    {
        _givenWord = word;
        _top5 = new MaxHeap<Node>();
        
        try
        {
            OpenResources();

            while (!IsAllFileRead())
            {
                ProcessLine();
            }
        }
        finally
        {
            CloseResources();
        }

        string[] top5Words = new string[Math.Min(5, _top5.Size())];

        for (int i = 1; i <= top5Words.Length; i++)
        {
            top5Words[top5Words.Length - i] = _top5.ExtractMax().Word;
        }
        return top5Words;

    }

    public string[] GetTop5Similar(string word)
    {
        _givenWord = word;
        _top5 = new MaxHeap<Node>();

        try
        {
            OpenResources();

            while (!IsAllFileRead())
            {
                string line = _reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    int distance = DamerauLevenshteinDistance.CalculateDistance(line, _givenWord);
                    if (_top5.Size() < 5)
                    {
                        _top5.Add(new Node(line, distance));
                    }
                    else if (_top5.PeekMax().Distance > distance)
                    {
                        _top5.ExtractMax();
                        _top5.Add(new Node(line, distance));
                    }
                }
            }
        }
        finally
        {
            CloseResources();
        }

        string[] topWords = new string[Math.Min(5, _top5.Size())];

        for (int i = 1; i <= topWords.Length; i++)
        {
            topWords[topWords.Length - i] = _top5.ExtractMax().Word;
        }

       
        return topWords;
    }


    private void OpenResources()
    {
        _reader = new StreamReader(File.Open(filePath, FileMode.Open));
    }

    private void CloseResources()
    {
        _reader.Close();
    }

    private bool IsAllFileRead()
    {
        return _reader.Peek() == -1;
    }

    private void ProcessLine()
    {
        string word = _reader.ReadLine();
        if (word.StartsWith(_givenWord))                        
        {
            int distance = DamerauLevenshteinDistance.CalculateDistance(word, _givenWord);
            if (_top5.Size() < 5)
            {
                _top5.Add(new Node(word, distance));
            }
            else if (_top5.PeekMax().Distance > distance)
            {
                _top5.ExtractMax();
                _top5.Add(new Node(word, distance));
            }

        }

    }

    public bool IsWordCorrect(string word)
    {
        using StreamReader reader = new(filePath);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (string.Equals(line, word, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
    public HashSet<string> CreateHashSet()
    {
        HashSet<string> hashSet = new HashSet<string>();
        try
        {
            OpenResources();
            while (!IsAllFileRead())
            {
                string word = _reader.ReadLine();
                hashSet.Add(word);
            }
        }
        finally
        {
            CloseResources();
        }
        return hashSet;
    }

}