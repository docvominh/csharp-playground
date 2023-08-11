namespace CSharp.OOP.Generic;

public class GenericService<T>
{
    public T Save(T t)
    {
        return t;
    }

    public List<T> FindAll()
    {
        return new List<T>();
    }
}