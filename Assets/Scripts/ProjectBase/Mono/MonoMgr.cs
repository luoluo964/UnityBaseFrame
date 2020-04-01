using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController controller;
    public MonoMgr() {
        //新建一个物体
        GameObject obj = new GameObject("MonoController");
        //给物体添加组件
        controller = obj.AddComponent<MonoController>();
    }
    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);

    }
    public void RemoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }
    public Coroutine StartCoroutine(IEnumerator routine) {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) {
        return controller.StartCoroutine(methodName,value);
    }
    public Coroutine StartCoroutine(string methodName) {
        return controller.StartCoroutine(methodName);
    }
}
