namespace PlainsAndDepressions;

public class Depression
{
    public int Size { get; private set; }

    public bool InProc { get; set; }

    public Depression()
    {
        InProc = false;
    }

    public static Depression operator ++ (Depression d)
    {
        d.Size++;
        d.InProc = true;
        return d;
    }
}
