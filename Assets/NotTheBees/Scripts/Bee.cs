using UnityEngine;
using DG.Tweening;

namespace NotTheBees
{
    public class Bee : MonoBehaviour
    {
        private Hive hive;
        [SerializeField] private float maxCheckFlowerTime;

        public void Init(Hive hive)
        {
            this.hive = hive;
            CheckAnyFlower();
        }

        void CheckAnyFlower()
        {
            Flower flower = GetRandomFlower();
            float checkTime = Random.Range(0, maxCheckFlowerTime);

            transform.DOMove(flower.transform.position, 1f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(checkTime, () =>
                {
                    if (flower.HasNectar() == false)
                    {
                        CheckAnyFlower();
                    }
                    else
                    {
                        flower.GetNectar();
                        DeliverNectar();
                    }
                });
                            
            }).SetEase(Ease.Linear);
        }

        void DeliverNectar()
        {
            transform.DOMove(hive.transform.position, 1f).OnComplete(() =>
            {
                hive.GiveNectar();
                CheckAnyFlower();
            });
        }

        Flower GetRandomFlower()
        {
            Flower[] flowers = FindObjectsByType<Flower>(FindObjectsSortMode.None);
            int randomIndex = Random.Range(0, flowers.Length);
            return flowers[randomIndex];
        }
    }
}

