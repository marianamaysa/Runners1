using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Wave2 : NetworkBehaviour
{
    public GameObject[] obstacles;
    public WaveManagement zPosGeneral;
    public int zPos;
    public bool spawnobs = false;
    public int obsNum;

    private List<GameObject> obsActive = new List<GameObject>();

    void Start()
    {
        zPosGeneral = FindObjectOfType<WaveManagement>();
        zPos = zPosGeneral.generalPosition;
        zPos += 40;
    }
    void Update()
    {
        if (spawnobs == false)
        {
            spawnobs = true;
            StartCoroutine(SpawnObstacle());
            StartCoroutine(DeleteObstacle());
        }
    }


    IEnumerator SpawnObstacle()
    {
        CreateWaveServerRpc();
        yield return new WaitForSeconds(1);
        spawnobs = false;
    }

    [ServerRpc]
    public void CreateWaveServerRpc()
    {
        obsNum = Random.Range(0, 2);
        zPos += 40;
        CreateWaveClientRpc(obsNum, zPos);
    }

    [ClientRpc]
    public void CreateWaveClientRpc(int obsNum, int zPos)
    {
        GameObject go = Instantiate(obstacles[obsNum], new Vector3(0, -1, zPos), Quaternion.identity);
        obsActive.Add(go);
    }

    IEnumerator DeleteObstacle()
    {
        yield return new WaitForSeconds(40);
        Destroy(obsActive[0]);
        obsActive.RemoveAt(0);
    }
}
