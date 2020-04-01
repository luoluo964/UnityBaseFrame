using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//抽屉数据，池子中的一列容器
public class PoolData
{
    //抽屉中，对象挂载的父节点
    public GameObject fatherObj;
    //对象的容器
    public List<GameObject> poolList;

    public PoolData(GameObject obj, GameObject poolObj)
    {
        //根据obj创建一个同名父类空物体，它的父物体为总Pool空物体
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;

        poolList =  new List<GameObject>() {  };

        PushObj(obj);
    }

    //像抽屉里面压东西并且设置好父对象
    public void PushObj(GameObject obj)
    {
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.parent = fatherObj.transform;
        //失活，让其隐藏
        obj.SetActive(false);
    }

    //像抽屉中取东西
    public GameObject GetObj() {
        GameObject obj = null;
        //取出第一个
        obj = poolList[0];
        poolList.RemoveAt(0);
        //激活，让其展示
        obj.SetActive(true);
        //断开父子关系
        obj.transform.parent = null;

        return obj;
    }
}


public class PoolMgr : BaseManager<PoolMgr>
{
    //这里是缓存池模块

    //创建字段存储容器
    public Dictionary<string, PoolData> pool1Dic
        =new Dictionary<string, PoolData>();

    private GameObject poolObj;

    //取得游戏物体
    public void GetObj(string name,UnityAction<GameObject> callback) {
        if (pool1Dic.ContainsKey(name) && pool1Dic[name].poolList.Count > 0)
        {
            //拖过委托返回给外部，让外部进行使用
            callback(pool1Dic[name].GetObj());
        }
        else {
            //缓存池中没有该物体，我们去目录中加载
            //外面传一个预设体的路径和名字，我内部就去加载它
            ResMgr.GetInstance().LoadAsync<GameObject>(name,(o)=> {
                o.name = name;
                callback(o);
            });
        }
    }

    //外界返还游戏物体
    public void PushObj(string name,GameObject obj) {
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");

        }
        //里面有记录这个键
        if (pool1Dic.ContainsKey(name))
        {
            pool1Dic[name].PushObj(obj);
        }
        //未曾记录这个键
        else {
            pool1Dic.Add(name, new PoolData(obj,poolObj) { });
        }
    }
    
    //清空缓存池的方法
    //主要用在场景切换时
    public void Clear() {
        pool1Dic.Clear();
        poolObj = null;
    }
}
