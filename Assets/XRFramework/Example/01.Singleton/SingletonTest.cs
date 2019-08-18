using XRFramework;
using UnityEngine;

public class SingletonTest : MonoBehaviour
{
    void Start()
    {
        MyLog.Log($"XXXManager.Instance1 == XXXManager.Instance2 : {ReferenceEquals(XXXManager.Instance, XXXManager.Instance)}"); ;
        XXXManager.Instance.Show("Hello!");
    }

    private void OnDestroy()
    {
        XXXManager.Instance.ClearSingleton();
    }
}
