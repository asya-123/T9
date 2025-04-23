namespace TypoFixer;

public class ArrayList<T>
{
    private T[] array;
    private int capacity;
    private int size;

    public ArrayList()
    {
        capacity = 8;
        size = 0;
        array = new T[capacity];
    }


    public void PushBack(T element)
    {
        array[size] = element;
        size++;

        if (size == capacity)
        {
            Resize();
        }
    }
    
    public void PushFront(T element)
    {
        for (int i = size; i > 0; i--)
        {
            array[i] = array[i - 1];
        }

        array[0] = element;
        size++;

        if (size == capacity)
        {
            Resize();
        }
    }

    public T PopBack()
    {
        if (size == 0)
        {
            throw new Exception("ArrayList is empty!");
        }

        size--;
        T result = array[size];

        return result;
    }

    public T PopFront()
    {
        if (size == 0)
        {
            throw new Exception("ArrayList is empty!");
        }

        T result = array[0];

        for (int i = 0; i < size; i++)
        {
            array[i] = array[i + 1];
        }

        size--;

        return result;
    }

    public void SetAt(T element, int index)
    {
        if (index < 0 || index > size - 1)
        {
            throw new Exception("Out of bounds access!");
        }

        array[index] = element;
    }
    
    public T GetAt(int index)
    {
        if (index < 0 || index > size - 1)
        {
            throw new Exception("Out of bounds access!");
        }

        return array[index];
    }

    private void Resize()
    {
        capacity *= 2;
        T[] newArray = new T[capacity];

        for (int i = 0; i < size; i++)
        {
            newArray[i] = array[i];
        }

        array = newArray;
    }

    public int Size() {
        return size;
    }
}