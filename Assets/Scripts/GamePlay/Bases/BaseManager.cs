using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class BaseManager: MonoBehaviour
    {
        //TODO:完善基地管理器
        public event Action<Base> OnNewBaseCreate;
        public event Action<Base> OnBaseDestroy;
        [SerializeField] public int maxBaseCount;
        public List<Base> BasesList = new List<Base>();
        
        private void Start()
        {
            GameManager.Instance.OnGameStart+=GameStartResolution;
            GameManager.Instance.OnTurnEnd+=TurnEndResolution;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                foreach (var @base in BasesList)
                {
                    foreach (var cardSlot in @base.CardSlotsList)   
                    {
                        cardSlot.InsertCard(new MinionCard("TestMinion","Test",2, 2, 1));
                    }
                }
            }
        }
        //游戏开始时初始化基地，注册给GameManager.OnGameStart的回调
        private void GameStartResolution()
        {
            //初始化基地
            for (int i = 0; i < maxBaseCount; i++)
            {
                CreateNewBase();
            }
        }
        
        //回合结束对所有基地进行结算，注册给GameManager.OnTurnEnd的回调
        private void TurnEndResolution()
        {
            //对所有基地进行结算
            foreach (var baseItem in BasesList)
            {
                baseItem.TurnEndResolution();
            }
            
            //检查基地是否被摧毁，如果被摧毁则销毁基地并获得奖励
            //因为在遍历的时候不能修改List，所以先记录下来，再进行销毁
            int baseCount = BasesList.Count;
            for (int i = 0; i < baseCount; i++)
            {
                Base baseItem = BasesList[i];
                if (baseItem.BaseDestroyCheck())
                {
                    baseItem.DestroyBase();
                    baseItem.GetBaseReward(GameManager.Instance.PlayerData);
                    BasesList.Remove(baseItem); //从列表中移除
                    baseCount--; //因为移除了一个元素，所以总数减一
                    i--; //因为移除了一个元素，所以索引减一
                    OnBaseDestroy?.Invoke(baseItem);
                }
            }

            //检查基地数量是否达到上限，没有则生成新的基地
            if (BasesList.Count<maxBaseCount)
            {
                int createBaseCount = maxBaseCount - BasesList.Count;
                for (int i = 0; i < createBaseCount; i++)
                {
                    CreateNewBase();
                }
            }
        }
        
        //生成新的基地，如果基地数量已经达到上限则不生成
        //TODO:完善生成基地算法，这里先简单写了一下
        private void CreateNewBase()
        {
            if (BasesList.Count < maxBaseCount)
            {
                int maxHp = Random.Range(10,20); 
                int money = Random.Range(10,20);
                int maxCardSlotsNumber = 6;
                Base newBae = new Base(maxHp, money, maxCardSlotsNumber);
                BasesList.Add(newBae);
                OnNewBaseCreate?.Invoke(newBae);
            }
            else
            {
                Debug.Log("INFO:BaseList is full, can't add more base");
            }
        }
        
        public static void PrintBaseList(List<Base> bases)
        {
            foreach (var baseItem in bases)
            {
                Debug.Log("Base: Name: "+ baseItem.Name +" MaxHp: " + baseItem.MaxHp + " myMoney: " + baseItem.Money);
            }
        }
    }
}