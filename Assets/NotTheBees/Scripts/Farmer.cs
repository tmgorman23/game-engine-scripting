using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NotTheBees
{
    public class Farmer : MonoBehaviour
    {
        //Create a new farmer class who will go through the hives on a loop to check for honey.
        //The farmer needs to move around just like the bees do
        //When honey is collected the Hive needs to set the amount of honey it has to 0

        [SerializeField] private int farmerHoney;
        [SerializeField] TextMeshProUGUI honeyLabel;
        [SerializeField] private float maxCheckHiveTime;

        // Start is called before the first frame update
        void Start()
        {
            CheckAnyHive();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void CheckAnyHive()
        {
            Hive hive = GetRandomHive();
            float checkTime = Random.Range(0, maxCheckHiveTime);

            transform.DOMove(hive.transform.position, 1f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(checkTime, () =>
                {
                    farmerHoney += hive.CollectHoney();
                    honeyLabel.text = "Honey: " + farmerHoney.ToString();
                    CheckAnyHive();
                });

            }).SetEase(Ease.Linear);
        }

        Hive GetRandomHive()
        {
            Hive[] hives = FindObjectsByType<Hive>(FindObjectsSortMode.None);
            int randomIndex = Random.Range(0, hives.Length);
            return hives[randomIndex];
        }
    }
}

