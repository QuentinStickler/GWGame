using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace _Scripts.Cutscenes
{
    [CreateAssetMenu(fileName = "CutScene", menuName = "Cutscenes/Elements/MovementElement", order = 1)]
    [Serializable]
    public class MovementElement : CutSceneElement
    {
        [SerializeField]
        public MultiObjectMovement multiObjectMovement;

        protected override void onStartElement(MonoBehaviour monoBehaviour, DialogueManager dialogueManager)
        {
            monoBehaviour.StartCoroutine(moveObjectsPerFrame());
        }

        protected override void onEndElement()
        {
            isActive = false;
        }

        IEnumerator moveObjectsPerFrame()
        {
            var hasMultiObjectMovementFinished = false;
            while (!hasMultiObjectMovementFinished)
            {
                hasMultiObjectMovementFinished = true;
                foreach (var singleObjectMovement in multiObjectMovement.movements)
                {

                    var currentPos = singleObjectMovement.objectToMove.position;
                    var targetPos = singleObjectMovement.locationToMoveTo.position;

                    var distanceToGoal = targetPos - currentPos;
                    distanceToGoal.y = 0;
                        
                    var speed = singleObjectMovement.movementSpeed;

                    singleObjectMovement.objectToMove.Translate((speed * Time.deltaTime * distanceToGoal.normalized));
                    if (distanceToGoal.magnitude >= 0.1)
                        hasMultiObjectMovementFinished = false;
                }
                yield return new WaitForEndOfFrame();
            }
            onEndElement();
        }
    }

    [Serializable]
    public class MultiObjectMovement
    {
        [SerializeField]
        public List<SingleObjectMovement> movements;
    }

    [Serializable]
    public class SingleObjectMovement
    {
        public Transform objectToMove;
        public Transform locationToMoveTo;
        public float movementSpeed;
    }
}