/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Yarn.Unity.Example
{
    public class Movement : MonoBehaviour
    {
        public bool canmove = true;
        public CharacterController charController;
        public AudioSource Footsteps;
        public float moveSpeed = 1f;
        public float gravityForce = 9.8f;
        public float interactionRadius = 2.0f;
        public GameObject footstepl;
        public GameObject footstepr;
        public GameObject footstepprefab;
        private bool whichfoot;
        // Draw the range at which we'll start talking to people.
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            // Flatten the sphere into a disk, which looks nicer in 2D games
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 1, 1));

            // Need to draw at position zero because we set position in the line above
            Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
        }

        /// Update is called once per frame
        void Update()
        {

            // Remove all player control when we're in dialogue
            if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true || canmove == false)
            {
                return;
            }

            // Move the player, clamping them to within the boundaries 
            // of the level.
            // NOTES    
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            // Get forward (relative to cam)
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0f;
            forward = forward.normalized;
            // Get right
            Vector3 right = new Vector3(forward.z, 0f, -forward.x);
            // Make a local move direction (right + forward)
            Vector3 localMoveDir = new Vector3(horizontal, 0f, vertical);
            // Turn to face, smooth
            if (localMoveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(localMoveDir), 10f * Time.smoothDeltaTime);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
            // Diagonal speed clamp
            if (localMoveDir.sqrMagnitude > 1f)
                localMoveDir = localMoveDir.normalized;
            // Gravity
            localMoveDir.y = (Physics.gravity * gravityForce * Time.deltaTime).y;
            // APPLY MOVE
            charController.Move(localMoveDir * moveSpeed * Time.deltaTime);
            // See if footstep sfx should play and if footsteps should place
            if (((Mathf.Abs(Input.GetAxis("Horizontal")) > .7) && (!Footsteps.isPlaying)) || ((Mathf.Abs(Input.GetAxis("Vertical")) > .7) && (!Footsteps.isPlaying)))
            {
                Footsteps.Play();
                //make footstep appear at two predefined locations
                if (whichfoot == false)
                {
                    Instantiate(footstepprefab, new Vector3(footstepl.transform.position.x, footstepl.transform.position.y, footstepl.transform.position.z), this.transform.rotation);
                    whichfoot = true;
                }
                else
                {
                    Instantiate(footstepprefab, new Vector3(footstepr.transform.position.x, footstepr.transform.position.y, footstepr.transform.position.z), this.transform.rotation);
                    whichfoot = false;
                }
              
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckForNearbyNPC();
            }
        }

        /// Find all DialogueParticipants
        /** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
        public void CheckForNearbyNPC()
        {
            var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
            var target = allParticipants.Find(delegate (NPC p)
            {
                return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius;
            });
            if (target != null)
            {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
            }
        }
    }
};