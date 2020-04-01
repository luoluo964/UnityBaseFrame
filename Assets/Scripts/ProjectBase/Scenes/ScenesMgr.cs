using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//场景切换模块
public class SceneMgr : BaseManager<SceneMgr>
{

    //切换场景
    public void LoadScene(string name,UnityAction func) {
        //场景同步加载
        SceneManager.LoadScene(name);
        //加载完成过后才会执行func
        func();
    }
    public void LoadSceneAsyn(string name, UnityAction func) {
        //公共Mono模块
        MonoMgr.GetInstance().StartCoroutine(ReallyLoadSceneAsyn(name,func));
    }
    private IEnumerator ReallyLoadSceneAsyn(string name,UnityAction func) {
        AsyncOperation ao=SceneManager.LoadSceneAsync(name);
        while (!ao.isDone) {
            //像事件中心分发进度情况，外面想用就用
            EventCenter.GetInstance().EventTrigger("Loading",ao.progress);
            //挂起一帧
            yield return ao.progress;
        }
        //加载完成后执行func
        func();
    }
}
