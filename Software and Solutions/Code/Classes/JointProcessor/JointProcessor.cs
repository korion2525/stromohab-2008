using System;
using System.Collections.Generic;
using System.Xml;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.DataProcessing
{
    /// <summary>
    /// JointProcessor will turn a joint definition file and a list of currently tracked rigid bodys
    /// into a list of joints. To start call LoadJointDefintion(), then call UpdateJoints with a new
    /// list of Trackables when required, when done an event is fired (JointListAvaliable) containing
    /// the new joint list
    /// 
    /// Note: The JointList that JointProcessor Maintains will always contain all loaded joints, even if
    /// they aren't tracked, to get those that are currently visiable, check the .Exists property
    /// </summary>
    ///  <author>Will Lunniss</author>
    public static class JointProcessor
    {
        #region Member Variables
        private static List<Joint> jointList = null;
        private static int maxTrackableID = 20; // Specifies the Highest Trackable ID number
        // START Added for MEng Project
        private static List<CoRLocator> jointCoRList = null;
        private static bool clearCoRData = false;
        // END Added for MEng Project
        #endregion MemberVariables


        #region Delegates
        /// <summary>
        /// Delegate for the JointListAvaliableEvent
        /// </summary>
        /// <param name="jointList">The updated JointList</param>
        public delegate void JointListAvaliableEventHandler(List<Joint> jointList);

        #endregion Delegates


        #region Events
        /// <summary>
        /// JointListAvaliableEvent containing the update JointList
        /// </summary>
        public static event JointListAvaliableEventHandler JointListAvaliable;
        private static void OnJointListAvaliable(List<Joint> jointList)
        {
            if (JointListAvaliable != null)
                JointListAvaliable(jointList);
        }
        #endregion Events


        #region Methods - Load
        
        /// <summary>
        /// Loads in the joint definitions from an xml based file joints.xml (Returns 0=Success / -1= Failed to find file)
        /// </summary>
        /// <returns></returns>
        public static int LoadJointDefinition()
        {
            return LoadJointDefinition("joints.xml");

        }
        /// <summary>
        /// Loads in the joint definitions from an xml based file (Returns 0=Success / -1= Failed to find file)
        /// </summary>
        /// <param name="file">The file containing the joint definitions</param>
        /// <returns></returns>
        public static int LoadJointDefinition(string file)
        {
            Joint newJoint = null;
            int result = 0;
            jointList = null;
            jointList = new List<Joint>();
            // START Added for MEng Project
            jointCoRList = new List<CoRLocator>();
            try
            {
                //Create the xml reader
                XmlTextReader xmlJoints = new XmlTextReader(file);
               
                //While reading
                while (xmlJoints.Read())
                {
                    //Get the node type
                    XmlNodeType nodeType = xmlJoints.NodeType;
                    if (nodeType == XmlNodeType.Element) // If its an element
                    {
                        switch (xmlJoints.Name)
                        {
                            case "joint": // New joint
                                newJoint = new Joint();
                                break;
                            case "id":
                                newJoint.ID = xmlJoints.ReadElementContentAsInt();
                                break;
                            case "name":
                                newJoint.Name = xmlJoints.ReadElementContentAsString();
                                break;
                            case "trackable1":
                                newJoint.Trackable1 = xmlJoints.ReadElementContentAsInt();
                                break;
                            case "trackable2":
                                newJoint.Trackable2 = xmlJoints.ReadElementContentAsInt();
                                break;
                            case "yawOffset":
                                newJoint.YawOffset = xmlJoints.ReadElementContentAsDouble();
                                break;
                            case "pitchOffset":
                                newJoint.PitchOffset = xmlJoints.ReadElementContentAsDouble();
                                break;
                            case "rollOffset":
                                newJoint.RollOffset = xmlJoints.ReadElementContentAsDouble();
                                break;
                        }
                    }
                    // If its the joint end element add the joint to the joint list
                    if (nodeType == XmlNodeType.EndElement && xmlJoints.Name == "joint")
                    {
                        jointList.Add(newJoint);
                        // START Added for MEng Project
                        // Add a corrisponding CoR Locator to the list
                        jointCoRList.Add(new CoRLocator());
                        // END Added for MEng Project
                    }

                }
             }
            catch
            {
                result = -1; // File not found;
            }

            return result;
        }

      
        #endregion Methods - Load


        #region Methods - Update

        /// <summary>
        /// Based on the given the current rigid body list, it Updates the avaliable joints.
        /// When done an event is fired (JointListAvaliable) containing the updated JointList
        /// </summary>
        /// <param name="trackableList">The trackable list</param>
        public static void UpdateJoints(List<Trackable> trackableList)
        {
            if (clearCoRData)
                ClearCoRData();

            int jointCount = jointList.Count;
            int trackableCount = trackableList.Count;
          
            int[] trackableIndex = new int[maxTrackableID];

            // Build the trackableIndex (add 1 to i (otherwise you need to initialise the intex to a value other than 0 which wasts cycles))
            for (int i = 0; i < trackableCount; i++)
            {
                trackableIndex[trackableList[i].ID] = i + 1;
            }

            // Go through the trackableIndex, if the value in the index equal to the trackable1 and trackable2 isn't equal to 0 then they exist
            for (int j = 0; j < jointCount; j++)
            {
                if (trackableIndex[jointList[j].Trackable1] != 0 && trackableIndex[jointList[j].Trackable2] != 0)
                {
                    jointList[j].Exists = true;
                    // Take 1 off of the value in the Index to make up for it being added when being built
                    jointList[j].Yaw = Math.Round(trackableList[trackableIndex[jointList[j].Trackable2] - 1].Yaw - trackableList[trackableIndex[jointList[j].Trackable1] - 1].Yaw + jointList[j].YawOffset,1);
                    jointList[j].Pitch = Math.Round(trackableList[trackableIndex[jointList[j].Trackable2] - 1].Pitch - trackableList[trackableIndex[jointList[j].Trackable1] - 1].Pitch + jointList[j].PitchOffset, 1);
                    jointList[j].Roll = Math.Round(trackableList[trackableIndex[jointList[j].Trackable2] - 1].Roll - trackableList[trackableIndex[jointList[j].Trackable1] - 1].Roll + jointList[j].RollOffset, 1);
                    jointList[j].TimeStamp = trackableList[trackableIndex[jointList[j].Trackable2] - 1].TimeStamp;
                    
                    // START Added for MEng Project
                    MathNet.Numerics.LinearAlgebra.Matrix CoR = jointCoRList[j].FindCoR(trackableList[trackableIndex[jointList[j].Trackable1] - 1], trackableList[trackableIndex[jointList[j].Trackable2] - 1]);
                    jointList[j].xCoordinate = (int)CoR[0, 0];
                    jointList[j].yCoordinate = (int)CoR[1, 0];
                    jointList[j].zCoordinate = (int)CoR[2, 0];
                    // END Added for MEng Project

                }
                else
                    jointList[j].Exists = false;
            }
            

            OnJointListAvaliable(jointList);
        }

        #endregion Methods - Update


        #region Methods - Clear CoR Data

        /// <summary>
        /// Sets a flag to clear the CoR data at the start of the next update
        /// </summary>
        public static void ResetCoRCalculations()
        {
            clearCoRData = true;
        }

        /// <summary>
        /// Clears the CoR data and resets the flag
        /// </summary>
        private static void ClearCoRData()
        {
            for (int i = 0; i < jointCoRList.Count; i++)
            {
                jointCoRList[i].ClearData();
            }
            clearCoRData = false;
        }

        #endregion Methods - Clear CoR Data
    }
}
