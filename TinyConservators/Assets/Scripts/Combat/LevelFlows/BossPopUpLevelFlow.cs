using System.Collections;
using UnityEngine;

public class BossPopUpLevelFlow : MonoBehaviour, ILevelFlowComponent
{
    [SerializeField] BossPopUp popUpVisual;
    [SerializeField] Sprite popUpGraphic;
    [SerializeField] float movementLength;
    [SerializeField] float waitLength;
    [SerializeField] AudioClip shout;

   
    LevelFlowManager owner;

    public void FinishSection()
    {
        owner.ProgressFlow();
    }

    public void StartSection(LevelFlowManager flowManager)
    {
        owner = flowManager;
        
        popUpVisual.PopUp(movementLength, waitLength, null, gameObject, shout);
        popUpVisual.SetVisual(popUpGraphic);
    }

    
}
