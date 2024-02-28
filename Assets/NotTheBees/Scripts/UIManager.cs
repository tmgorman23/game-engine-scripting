using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NotTheBees
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI honeyLabel;

        [SerializeField] GameObject hivePrefab;

        private Hive hive;

        // Start is called before the first frame update
        void Start()
        {
            GameObject hive = Instantiate(hivePrefab, transform.position, hivePrefab.transform.rotation);
            //hive.GetComponent<Hive>().Init(this);
        }

        // Update is called once per frame
        void Update()
        {
            honeyLabel.text = hive.GetCurrentHoneyAmount().ToString();
        }
    }
}

