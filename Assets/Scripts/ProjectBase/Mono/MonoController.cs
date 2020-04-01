using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Mono的管理者
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    private void Start()
    {
        //此对象不可移除
        //从而方便别的对象找到该物体，从而获取脚本，从而添加方法
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (updateEvent != null) {
            updateEvent();
        }
    }
    //为外部提供的添加帧更新事件的方法
    public void AddUpdateListener(UnityAction func)
    {
        updateEvent += func;
    }
    //为外部提供的移除帧更新事件的方法
    public void RemoveUpdateListener(UnityAction func) {
        updateEvent -= func;
    } 
}
