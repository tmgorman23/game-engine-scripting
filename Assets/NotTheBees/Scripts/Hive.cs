using TMPro;
using UnityEngine;

namespace NotTheBees
{
    public class Hive : MonoBehaviour
    {
        [SerializeField] float honeyProductionRate;
        [SerializeField] int startingNumberOfBees;
        [SerializeField] GameObject beePrefab;
        [SerializeField] TextMeshProUGUI honeyLabel;

        private int nectar;
        private int honey;

        private float time;

        // Start is called before the first frame update
        void Start()
        {
            time = honeyProductionRate;

            for (int i = 0; i < startingNumberOfBees; i++)
            {
                GameObject bee = Instantiate(beePrefab, transform.position, beePrefab.transform.rotation);
                bee.GetComponent<Bee>().Init(this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (HasNectar() == false) return;
            ProduceHoney();
            
        }

        public void GiveNectar()
        {
            Debug.Log($"Nectar Delivered, HasNectar is {HasNectar()}, nectar amount is {nectar}, time: {time}, honey: {honey}");
            honeyLabel.text = "Honey: " + honey.ToString();
            nectar++;
        }

        public int CollectHoney()
        {
            int amt = honey;
            honey = 0;
            return amt;
        }

        public int GetCurrentHoneyAmount()
        {
            return honey;
        }

        bool HasNectar()
        {
            return nectar > 0;
        }

        void ProduceHoney()
        {
            time -= Time.deltaTime;
            if (time >= 0) return;
            if (time <= 0)
            {
                nectar--;
                honey++;
                time = honeyProductionRate;
            }
        }
    }
}