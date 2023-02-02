using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
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
                    if(singleObjectMovement.hasFinishedMovement)
                        continue;

                    var currentPos = singleObjectMovement.objectToMove.position;
                    var targetPos = singleObjectMovement.locationToMoveTo.position;

                    var distanceToGoal = targetPos - currentPos;
                    distanceToGoal.y = 0;
                    
                    // Debug.Log("CURRENT POS: " + singleObjectMovement.objectToMove.position);
                    // Debug.Log("TARGET POS: " + singleObjectMovement.locationToMoveTo.position);
                    // Debug.Log("DIRECTION : " + distanceToGoal.normalized);
                        
                    var speed = singleObjectMovement.movementSpeed;

                    // singleObjectMovement.objectToMove.Translate(speed * Time.deltaTime * distanceToGoal.normalized);
                    singleObjectMovement.objectToMove.position = currentPos + (speed * Time.deltaTime * distanceToGoal.normalized);

                    if (distanceToGoal.magnitude <= 0.1f)
                    {
                        singleObjectMovement.objectToMove.position = targetPos;
                        singleObjectMovement.hasFinishedMovement = true;
                    }

                    hasMultiObjectMovementFinished &= singleObjectMovement.hasFinishedMovement;
                    
                    // if (distanceToGoal.magnitude >= 0.1f)
                        // hasMultiObjectMovementFinished = false;
                }
                yield return null;
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
        public bool hasFinishedMovement = false;
    }
}