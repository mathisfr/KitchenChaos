using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
    private const string ANIMATOR_BOOL_ISFLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(ANIMATOR_BOOL_ISFLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool playWarningAnimationFlash = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        animator.SetBool(ANIMATOR_BOOL_ISFLASHING, playWarningAnimationFlash);
    }
}
