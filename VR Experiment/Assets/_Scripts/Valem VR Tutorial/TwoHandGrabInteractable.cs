using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{

    public List<XRSimpleInteractable> secondHandGrabpoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;
    private Quaternion attachIntialRoation;
    public enum TwoHandRotationType { None, First, Second }
    public TwoHandRotationType twoHandRotationType;
    public bool SnapToSecondHand = true;
    private Quaternion intialRotationOffset;




    void Start()
    {
        foreach (var item in secondHandGrabpoints)
        {
            item.selectEntered.AddListener(OnSecondHandGrab);
            item.selectExited.AddListener(OnSecondHandRelease);
        }
    }



    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondInteractor && selectingInteractor)
        {
            if (SnapToSecondHand)
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();

            else
                secondInteractor.attachTransform.rotation = GetTwoHandRotation() * intialRotationOffset;
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;

        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }

        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.transform.up);
        }

        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.transform.up);
        }

        return targetRotation;

    }



    public void OnSecondHandGrab(SelectEnterEventArgs args)
    {

        secondInteractor = args.interactor;
        intialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * secondInteractor.attachTransform.rotation;
    }

    public void OnSecondHandRelease(SelectExitEventArgs args)
    {

        secondInteractor = null;
    }



    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        attachIntialRoation = args.interactor.attachTransform.localRotation;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {


        secondInteractor = null;
        args.interactor.attachTransform.localRotation = attachIntialRoation;
        base.OnSelectExited(args);
    }


    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {

        bool isalreadygrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isalreadygrabbed;
    }

    /*
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    private IXRSelectInteractor firstInteractor, secondInteractor;

    // Start is called before the first frame update
    void Start()
    {
		foreach (var item in secondHandGrabPoints)
		{
            item.selectEntered.AddListener(OnSecondHandGrab);
            item.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        // compute rotation
        if (firstInteractor != null && secondInteractor != null)
        {
            firstInteractor.transform.rotation = Quaternion.LookRotation(secondInteractor.transform.position - firstInteractor.transform.position, firstInteractor.transform.up);

        }
        base.ProcessInteractable(updatePhase);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        firstInteractor = args.interactorObject;

        base.OnSelectEntered(args);
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {

        firstInteractor = null;
        secondInteractor = null;
        base.OnSelectExited(args);
    }
    /*
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        firstInteractor = args.interactorObject;
        Debug.Log("First Grab Enter");
        base.OnSelectEntered(args);
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("First Grab Exit");
        firstInteractor = null;
        secondInteractor = null;
        base.OnSelectExited(args);
    }
    

    public void OnSecondHandGrab(SelectEnterEventArgs args) => secondInteractor = args.interactorObject;
    public void OnSecondHandRelease(SelectExitEventArgs args) => secondInteractor = null;

    
    public override bool IsSelectableBy(XRBaseInteractor interactor)
	{
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(isSelected);
		return (base.IsSelectableBy(interactor) && !isAlreadyGrabbed);
	}*/
    
}
