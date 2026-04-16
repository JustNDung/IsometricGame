using System;
using DefaultNamespace;
using DefaultNamespace.AllMessages;
using UI.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DoorExperimentUI : ExperimentUI
    {
        [SerializeField] private Image hingePoint;
        [SerializeField] private Image handlePoint;
        
        private void OnEnable()
        {
            MessageDispatcher.Subscribe<DoorExperimentUIDisplayMessage>(Show);
            MessageDispatcher.Subscribe<DoorExperimentUIHideMessage>(Hide);
        }
        
        private void OnDisable()
        {
            MessageDispatcher.Unsubscribe<DoorExperimentUIDisplayMessage>(Show);
            MessageDispatcher.Unsubscribe<DoorExperimentUIHideMessage>(Hide);
        }
        
        public override void Show(IMessage message)
        {
            if (message is not DoorExperimentUIDisplayMessage msg) return;
            hingePoint.gameObject.GetComponent<CinematicUIAnimator>().PlayShow();
            handlePoint.gameObject.GetComponent<CinematicUIAnimator>().PlayShow();
        }
        

        public override void Hide(IMessage message)
        {
            if (message is not DoorExperimentUIHideMessage msg) return;
            hingePoint.gameObject.GetComponent<CinematicUIAnimator>().PlayHide();
            handlePoint.gameObject.GetComponent<CinematicUIAnimator>().PlayHide();
        }
    }
}