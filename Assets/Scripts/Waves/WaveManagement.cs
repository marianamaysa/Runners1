using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WaveManagement : NetworkBehaviour
{
    public int contador = 0;
    public SpawnObstacles zPosGeneral;
    public Wave2 zPosGeneral2;
    public int generalPosition;
    
    // Update is called once per frame
    void Update()
    {
        if (contador >= 0)
        {
            contador += 1;
            zPosGeneral = FindObjectOfType<SpawnObstacles>();
            generalPosition = zPosGeneral.zPos;
        }

        if (contador >= 3500)
        {
            this.GetComponent<SpawnObstacles>().enabled = false;
            this.GetComponent<Wave2>().enabled = true;
        }

        if (contador >= 7000)
        {
            zPosGeneral2 = FindObjectOfType<Wave2>();
            generalPosition = zPosGeneral2.zPos;
            this.GetComponent<Wave2>().enabled = false;
            this.GetComponent<Wave3>().enabled = true;
        }
    }
}
