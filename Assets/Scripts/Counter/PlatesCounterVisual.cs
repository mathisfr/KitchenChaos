using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter paltesCounter;

    private List<GameObject> plateVisuaklGameObjectList;

    private void Awake()
    {
        plateVisuaklGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        paltesCounter.OnPlateSpawned += PaltesCounter_OnPlateSpawned;
        paltesCounter.OnPlateRemove += PaltesCounter_OnPlateRemove;
    }

    private void PaltesCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisuaklGameObjectList[plateVisuaklGameObjectList.Count - 1];
        plateVisuaklGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PaltesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3 (0, plateOffsetY * plateVisuaklGameObjectList.Count, 0);
        plateVisuaklGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
