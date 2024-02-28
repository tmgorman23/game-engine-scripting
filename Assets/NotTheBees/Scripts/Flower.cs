using UnityEngine;

namespace NotTheBees
{
    public class Flower : MonoBehaviour
    {
        [SerializeField] float nectarRate;

        [SerializeField] Color colorReady;
        
        [SerializeField] Color colorNotReady;

        [SerializeField] GameObject flowerPrefab;

        private Flower flower;

        SpriteRenderer spriteRenderer;

        bool hasNectar;

        int randomNumber = 0;

        float time;

        public void Init(Flower flower)
        {
            this.flower = flower;
        }

        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = colorReady;
            hasNectar = true;
        }

        // Update is called once per frame
        void Update()
        {
            ProduceNectar();
            //ProduceFlowers(1);
        }

        public bool HasNectar()
        {
            return hasNectar;
        }

        public void GetNectar()
        {
            hasNectar = false;
            spriteRenderer.color = colorNotReady;
            time = nectarRate;
        }

        void ProduceNectar()
        {
            if (time <= 0)
            {
                return;
            }

            time -= Time.deltaTime;

            if (time <= 0)
            {
                hasNectar = true;
                spriteRenderer.color = colorReady;
            }
        }

        void ProduceFlowers(int numberOfFlowersToSpawn)
        {
            //GetRandomInt();
            //if (RandomCheck())
            //{
            //    for (int i = 0; i < numberOfFlowersToSpawn; i++)
            //    {
            //        GameObject flower = Instantiate(flowerPrefab, transform.position, flowerPrefab.transform.rotation);
            //        flower.GetComponent<Flower>().Init(this);
            //    }
            //}
            randomNumber = Random.Range(0, 10);
            Debug.Log(randomNumber.ToString());
            if (randomNumber > 8)
            {
                //GameObject flowerNew = Instantiate(flowerPrefab, transform.position, flowerPrefab.transform.rotation);
                //flowerNew.GetComponent<Flower>().Init(this);
            }
        }

        void GetRandomInt()
        {
            int rnd = Random.Range(0, 5);
            randomNumber = rnd;
        }

        bool RandomCheck()
        {
            if (randomNumber == 3)
            {
                Debug.Log("RandomCheck true");
                return true;
                
            }
            return false;
        }
    }
}