using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//面板基类
//找到所有自己面板下的控件对象
//提供显式/隐藏的行为
public class BasePanel : MonoBehaviour
{
    //通过里式转换原则，来存储所有的UI控件
    private Dictionary<string, List<UIBehaviour>> controlDic 
        = new Dictionary<string, List<UIBehaviour>>();
    private void Awake()
    {
        FindChildControl<Button>();
        FindChildControl<Image>();
        FindChildControl<Text>();
        FindChildControl<Toggle>();
        FindChildControl<ScrollRect>();
        FindChildControl<Slider>();
    }
    //得到对应名字的对应控件脚本
    protected T GetControl<T>(string controlName) where T:UIBehaviour {
        if (controlDic.ContainsKey(controlName)) {
            for (int i = 0; i < controlDic[controlName].Count; i++) {
                //对应字典的值（是个集合）中，符合要求的类型的
                //则返回出去，这样外部就
                if (controlDic[controlName][i] is T) {
                    return controlDic[controlName][i] as T;
                }
            }
        }
        return null;
    }
    //找到相对应的UI控件并记录到字典中
    private void FindChildControl<T>() where T:UIBehaviour {
        T[] controls = this.GetComponentsInChildren<T>();
        string objname;
        for (int i = 0; i < controls.Length; i++)
        {
            objname = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objname))
            {
                controlDic[objname].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objname, new List<UIBehaviour>() { controls[i] });
            }            
        }
    }
    //让子类重写（覆盖）此方法，来实现UI的隐藏与出现

    public virtual void ShowMe() {

    }
    public virtual void HideMe() {

    }
}
