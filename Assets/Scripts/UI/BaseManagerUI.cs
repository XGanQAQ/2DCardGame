using System;
using System.Collections.Generic;
using UnityEngine;
using GamePlay;
namespace UI
{
    [RequireComponent(typeof(BaseManager))]
    public class BaseManagerUI : MonoBehaviour
    {
        private BaseManager _baseManager;
        private List<BaseUI> _baseUIList;
        [SerializeField]private float _gap = 5;
        private void Start()
        {
            _baseUIList = new List<BaseUI>();
            _baseManager = GetComponent<BaseManager>();
            _baseManager.OnNewBaseCreate += CreateNewBaseUI;
            _baseManager.OnBaseDestroy += DestoryBaseUI; //注册基地被摧毁时的回调
        }

        private void CreateNewBaseUI(Base baseData)
        {
            Vector3 targetPosition = new Vector3(_baseUIList.Count * _gap, 0, 0);
            BaseUI baseUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/BaseUI"),targetPosition,Quaternion.identity,transform).GetComponent<BaseUI>();
            baseUI.InitBaseUI(baseData);
            _baseUIList.Add(baseUI);
        }
        
        private void DestoryBaseUI(Base baseData)
        {
            foreach (var baseUI in _baseUIList)
            {
                if (baseUI.BaseData == baseData)
                {
                    //Destroy(baseUI.gameObject); //只是删除了列表的存储，BaseUI的删除由自身调用
                    _baseUIList.Remove(baseUI);
                    break;
                }
            }
            ResetBaseUIPosition();
        }
        
        private void ResetBaseUIPosition()
        {
            for (int i = 0; i < _baseUIList.Count; i++)
            {
                _baseUIList[i].transform.position = new Vector3(i * _gap, 0, 0);
            }
        }
    }
}