using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class BaseUI:MonoBehaviour
    {
        //TODO:基地UI
        private Base _baseData;
        public Base BaseData {
            get {
                if (_baseData==null)
                {
                    _baseData = new Base(10, 10, 6);
                }
                return _baseData;
            }
            set => _baseData = value;
        }
        public GameObject BaseCanvasGameObject;
        public Text HpText;
        public Text MoneyText;
        
        private GameObject _cardSlotsGroup;
        private List<BaseCardSlotUI> _cardSlotsUIList = new List<BaseCardSlotUI>();

        private void Start()
        {
            BaseData.OnBaseDestroy += Destroy;
        }

        private void Destroy(Base baseData)
        {
            Destroy(this.gameObject);
        }
        private void FindCardSlotsGroupInChild()
        {
            for (int i = 0; i < BaseCanvasGameObject.transform.childCount; i++)
            {
                GameObject child = BaseCanvasGameObject.transform.GetChild(i).gameObject;
                if (child.name == "CardSlotsGroup")
                {
                    _cardSlotsGroup =child;
                    break;
                } 
                //Debug.Log(child.name);
            }
            if (_cardSlotsGroup == null)
            {
                Debug.LogError("Can't Find CardSlotsGroup In Child");
            }
        }

        public void InitBaseUI(Base baseData)
        {
            BaseData = baseData;
            
            //更新基地UI状态
            UpdateBaseUI();
            BaseData.OnBaseStateChange += UpdateBaseUI;
            
            //创建卡槽
            FindCardSlotsGroupInChild(); //找到CardSlotsGroup
            CreateCardSlots(); //在CardSlotsGroup创建CardSlots
            GetAllCardSlotsInGroupChild(); //获取CardSlotsGroup下的所有CardSlots
        }

        private void CreateCardSlots()
        {
            //TODO: 这个创建逻辑只能在第一次创建时使用，如果有后续增加删除基地卡牌槽位的机制，需要重新设计
            for (int i = 0; i < BaseData.MaxCardSlotsNumber; i++)
            {
                {
                    GameObject CardSoltsUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/CardSlotUI"), _cardSlotsGroup.transform);
                    CardSoltsUI.name = "CardSlotUI" + i;
                    BaseCardSlotUI baseCardSlotUI = CardSoltsUI.GetComponent<BaseCardSlotUI>();
                    baseCardSlotUI.InitBaseCardSlotUI(BaseData.CardSlotsList[i]);
                    _cardSlotsUIList.Add(baseCardSlotUI);
                }
            }
            Debug.Log("Create CardSlots Over");
        }
        
        private void GetAllCardSlotsInGroupChild()
        {
            int childCount = _cardSlotsGroup.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                _cardSlotsUIList.Add(_cardSlotsGroup.transform.GetChild(i).gameObject.GetComponent<BaseCardSlotUI>());
            }
        }
        
        private void UpdateBaseUI()
        {
            HpText.text = BaseData.CurrentHp.ToString();
            MoneyText.text = BaseData.Money.ToString();
        }
    }
}