namespace TypoFixer;

public static class DamerauLevenshteinDistance
{
    public static int CalculateDistance(string str1, string str2)
    {
        int[,] matrix = new int[str1.Length + 1, str2.Length + 1];

        for (int i = 0; i <= str1.Length; i++)
        {
            for (int j = 0; j <= str2.Length; j++)
            {
                List<int> possibleValues = new List<int>();
                if (Math.Min(i, j) == 0)
                {
                    matrix[i, j] = Math.Max(i, j);
                    continue;
                }

                possibleValues.Add(matrix[i - 1, j] + 1);
                possibleValues.Add(matrix[i, j - 1] + 1);

                if (str1[i - 1] != str2[j - 1])
                    possibleValues.Add(matrix[i - 1, j - 1] + 1);
                else
                    possibleValues.Add(matrix[i - 1, j - 1]);

                if (i >= 2 && j >= 2 && str1[i - 2] == str2[j - 1] && str1[i - 1] == str2[j - 2])
                    possibleValues.Add(matrix[i - 2, j - 2] + 1);

                matrix[i, j] = possibleValues.Min();
            }
        }

        return matrix[str1.Length, str2.Length];
    }
}