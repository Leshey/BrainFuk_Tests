namespace BrainFuck;

public class ExitToken
{
    public bool IsCanceled { get; private set; }

    public ExitToken()
    {
        IsCanceled = false;
    }

    public void MakeCanceled()
    {
        IsCanceled = true;
    }
}

