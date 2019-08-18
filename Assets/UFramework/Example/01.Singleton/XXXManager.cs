using XRFramework;

public class XXXManager : Singleton<XXXManager>
{
    private XXXManager()
    {
        MyLog.Log("XXXManager Init");
    }


    public override void ClearSingleton()
    {
        base.ClearSingleton();
        MyLog.Log("XXXManager ClearSingleton");
    }

    public void Show(string s)
    {
        MyLog.Log($"XXXManager Show {s}");
    }
}
