using System.Collections;
using System.Collections.Generic;
using XRFramework;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    void Start()
    {
        ObjectPool<Item> itemPool = new ObjectPool<Item>(3, () => new Item(), GetItem, Release);
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:0

        Item item = new Item();
        itemPool.Release(item);
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:1

        item = itemPool.Get();
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:0

        Item item1 = itemPool.Get();
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:0

        itemPool.Release(item);
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:1

        itemPool.Release(item1);
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:2

        itemPool.Release(new Item());
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:3

        itemPool.Release(new Item());
        MyLog.Log($"itemPool.CurCount:{itemPool.CurCount}"); //itemPool.CurCount:3

    }

    void GetItem(Item item)
    {
        MyLog.Log("GetItem");
    }
    void Release(Item item)
    {
        MyLog.Log("Release");
    }
}
