using TMPro;
using UnityEngine;

namespace Bees
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
            nectar++;
        }

        public int CollectHoney()
        {
            int amt = honey;
            honey = 0;
            return amt;
        }

        bool HasNectar()
        {
            return nectar > 0;
        }

        void ProduceHoney()
        {
            if (time <= 0) return;
            time -= Time.deltaTime;

            if (time <= 0)
            {
                nectar--;
                honey++;
                time = honeyProductionRate;
            }
        }
    }
}