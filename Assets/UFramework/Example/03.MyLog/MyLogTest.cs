using System;
using UFramework;
using UnityEngine;
using UnityEngine.Assertions;

public class MyLogTest : MonoBehaviour
{
    void Start()
    {
        MyLog.Log("MyLog.Log test.");
        MyLog.LogWarning("MyLog.LogWarning test.");
        MyLog.LogError("MyLog.LogError test.");
        Assert.IsTrue(false, "Assert test.");
        throw new Exception("Exception test.");
    }
}
