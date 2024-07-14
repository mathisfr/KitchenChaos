using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private const string ANIMATOR_CUT = "Cut";

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        cuttingCounter.OnCut += ContainerCounter_OnCut;
    }

    private void ContainerCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ANIMATOR_CUT);
    }
}
