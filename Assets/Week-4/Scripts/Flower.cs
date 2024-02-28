using UnityEngine;

namespace Bees
{
    public class Flower : MonoBehaviour
    {
        [SerializeField] float nectarRate;
        [SerializeField] Color colorReady;
        [SerializeField] Color colorNotReady;

        SpriteRenderer spriteRenderer;
        bool hasNectar;

        float time;

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
    }
}