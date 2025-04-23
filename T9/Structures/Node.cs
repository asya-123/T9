namespace TypoFixer;

public class Node : IComparable<Node>
{
    public string Word { get; set; }   
    public int Distance { get; set; }   

    public Node(string word, int distance)
    {
        Word = word;
        Distance = distance;
    }

    public int CompareTo(Node other)
    {
        return Distance.CompareTo(other.Distance);
    }
}