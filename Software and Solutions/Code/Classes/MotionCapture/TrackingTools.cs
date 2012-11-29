using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Collections.Generic;
using StroMoHab_Objects.Objects;

namespace StroMoHab_TT_Server.MotionCapture
{
    /// <summary>
    /// TrackingTools contains the methods needed to use the unmanged code inside NPTrackingTools.dll
    /// It should only be used through MotionCapture.cs or a similar class which provieds the needed protection
    /// to only allow method calls once the API is running etc (otherwise the program will access invalid memory and crash)
    /// </summary>
    /// <auther>Will Lunniss</auther>
    public static class TrackingTools
    {
        private const string DllLoc = "NPTrackingTools.dll";


        #region Tracking Tools - DLL Interfacer

        // See TrackingTools API for a full breakdown of the function of each of the methods listed below
        // - http://www.naturalpoint.com/optitrack/support/manuals/trackingtools/tt-index.html

        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_Initialize")]
        private static extern int InitializeAPI();

        /// <summary>
        /// Initialises the Tracking Tools API
        /// </summary>
        /// <returns></returns>
        public static int Initialize()
        {
            if (CheckDLLExists() == false)
            {
                return 100; // Can't find DLL!
            }
            int result = InitializeAPI();

            return result;
        }

        /// <summary>
        /// Shuts down the Tracking Tools API
        /// </summary>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_Shutdown")]
        public static extern int Shutdown();

        /// <summary>
        /// Shuts down the camera drivers and unloads them from memory. The application
        /// must now be restarted
        /// </summary>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_FinalCleanup")]
        public static extern int FinalShutdown();

        /// <summary>
        /// Loads in a calibration file
        /// </summary>
        /// <param name="file">The calibration file</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_LoadCalibration", CharSet = CharSet.Ansi)]
        public static extern int LoadCalibration(string file);

        /// <summary>
        /// Loads in a trackable definition file
        /// </summary>
        /// <param name="file">The trackable file</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_LoadTrackables", CharSet = CharSet.Ansi)]
        public static extern int LoadTrackables(string file);


        /// <summary>
        /// Loads in a project file
        /// </summary>
        /// <param name="file">The project file</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_LoadProject", CharSet = CharSet.Ansi)]
        public static extern int LoadProject(string file);

        /// <summary>
        /// Updates upto the current frame
        /// </summary>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_Update")]
        public static extern int UpdateAll();

        /// <summary>
        /// Updates the next frame in the queue
        /// </summary>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_UpdateSingleFrame")]
        public static extern int UpdateSingle();


        /// <summary>
        /// Gets the number of markers in the current frame
        /// </summary>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_FrameMarkerCount")]
        public static extern int FrameMarkerCount();

        /// <summary>
        /// Gets the X position for the marker
        /// </summary>
        /// <param name="index">The marker index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_FrameMarkerX")]
        public static extern float FrameMarkerX(int index);

        /// <summary>
        /// Gets the Y position for the marker
        /// </summary>
        /// <param name="index">The marker index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_FrameMarkerY")]
        public static extern float FrameMarkerY(int index);

        /// <summary>
        /// Gets the Z position for the marker
        /// </summary>
        /// <param name="index">The marker index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_FrameMarkerZ")]
        public static extern float FrameMarkerZ(int index);

        // Marshal the data to ensure the correct value is returned
        /// <summary>
        /// Checks to see if the trackable is currently being tracked
        /// </summary>
        /// <param name="index">The trackable index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_IsTrackableTracked")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsTrackableTracked(int index);


        // Gets the location of the Trackable (Rigid Body) specified by the index
        // Calling TrackableLocation(int TrackableIndex) creates a new Trackable, then calls the TrackableLocation
        //  inside the .dll with the required out ref, these variables are then filled and added to the new Trackable
        //  which is then returned
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_TrackableLocation", CharSet = CharSet.Ansi)]
        private static extern int GetTrackableLocation(int TrackableIndex, out float x, out float y, out float z,
                                                            out float qx, out float qy, out float qz, out float qw,
                                                            out float yaw, out float pitch, out float roll);


        /// <summary>
        /// Returns the Trackable for the index specified
        /// </summary>
        /// <param name="TrackableIndex">The index of the trackable</param>
        /// <returns>The result</returns>
        public static Trackable GetTrackableLocation(int TrackableIndex)
        {
            Trackable trackable = new Trackable();

            float x, y, z;
            float qx, qy, qz, qw;
            float yaw, pitch, roll;

            GetTrackableLocation(TrackableIndex, out x, out y, out z, out qx, out qy, out qz, out qw, out yaw, out pitch, out roll);

            trackable.xCoordinate = (int)(x * 1000); // Positive faceing the screen going right
            trackable.yCoordinate = (int)(y * 1000); // Positive Up
            trackable.zCoordinate = (int)(z * 1000); // Positive Towards the screen


            trackable.Yaw = -Math.Round(yaw, 1); // invert yaw
            trackable.Pitch = Math.Round(roll, 1); // Swap around pitch and roll
            trackable.Roll = Math.Round(pitch, 1);
            trackable.TimeStamp = System.DateTime.Now.Ticks;
            trackable.ID = TrackableID(TrackableIndex);
            trackable.Name = TT_TrackableName(TrackableIndex);
            trackable.TrackableIndex = TrackableIndex;
            trackable.QW = qw;
            trackable.QX = qx;
            trackable.QY = qy;
            trackable.QZ = qz;

            Marker trackableMarker = null;
            trackable.TrackableMarkers = new List<Marker>();

            // Build the rotation matrix
            StroMoHab_Matrix.RotationMatrix rotationMatix = new StroMoHab_Matrix.RotationMatrix(Math.PI * trackable.Pitch / 180, Math.PI * trackable.Yaw / 180, Math.PI * trackable.Roll / 180);

            //loop through and get all of the markers that are part of the trackable, apply the rotation matrix, and add them
            for (int j = 0; j < TrackingTools.TrackableMarkerCount(TrackableIndex); j++)
            {
                trackableMarker = TrackingTools.TrackableMarker(TrackableIndex, j); // get the marker
                // rotate the marker
                StroMoHab_Matrix.PointMatrix newPointMatrix = StroMoHab_Matrix.Operations.Rotate(new StroMoHab_Matrix.PointMatrix(trackableMarker.xCoordinate, trackableMarker.yCoordinate, trackableMarker.zCoordinate), rotationMatix);

                //get the new marker position
                trackableMarker.xCoordinate = Math.Round(newPointMatrix.XCoordinate, 1);
                trackableMarker.yCoordinate = Math.Round(newPointMatrix.YCoordinate, 1);
                trackableMarker.zCoordinate = Math.Round(newPointMatrix.ZCoordinate, 1);
                trackable.TrackableMarkers.Add(trackableMarker);
            }
            return trackable;
        }

        /// <summary>
        /// Clears the list of trackables
        /// </summary>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_ClearTrackableList")]
        public static extern void ClearTrackableList();

        /// <summary>
        /// Gets the number of trackables
        /// </summary>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_TrackableCount")]
        public static extern int TrackableCount();

        /// <summary>
        /// Gets the ID number of the trackable
        /// </summary>
        /// <param name="index">The trackable index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_TrackableID")]
        public static extern int TrackableID(int index);

        /// <summary>
        /// Sets the ID number of the trackable
        /// </summary>
        /// <param name="index">The trackable index</param>
        /// <param name="ID">The result</param>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_SetTrackableID")]
        public static extern void SetTrackableID(int index, int ID);


        // Marshal the data to stop memory corruption
        /// <summary>
        /// Gets the name of the trackable
        /// </summary>
        /// <param name="Index">The trackable index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.AnsiBStr)]
        public static extern string TT_TrackableName(int Index);


        /// <summary>
        /// Enables/Dissables the trackable
        /// </summary>
        /// <param name="index">The trackable index</param>
        /// <param name="enabled">Enable/Dissable</param>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_SetTrackableEnabled")]
        public static extern void SetTrackableEnabled(int index, bool enabled);

        // Marshal the data to ensure the correct value is returned
        /// <summary>
        /// Checks to see if the trackable is enabled
        /// </summary>
        /// <param name="index">The trackable index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_TrackableEnabled")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool TrackableEnabled(int index);

        /// <summary>
        /// Gets the number of markers that make up the trackable
        /// </summary>
        /// <param name="index">The trackable index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_TrackableMarkerCount")]
        public static extern int TrackableMarkerCount(int index);


        /// <summary>
        /// Gets the location of the Marker specified by the index in the Trackable specified by the index
        /// </summary>
        /// <param name="TrackableIndex">The trackable index</param>
        /// <param name="MarkerIndex">The marker index</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="z">Z Position</param>
        /// <returns></returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_TrackableMarker", CharSet = CharSet.Ansi)]
        private static extern int TrackableMarker(int TrackableIndex, int MarkerIndex, out float x, out float y, out float z);

        /// <summary>
        /// Gets the marker that makes up the trackable
        /// </summary>
        /// <param name="TrackableIndex">The trackable index</param>
        /// <param name="MarkerIndex">The marker index</param>
        /// <returns>The marker</returns>
        public static Marker TrackableMarker(int TrackableIndex, int MarkerIndex)
        {
            Marker marker = new Marker();
            float x, y, z;
            TrackableMarker(TrackableIndex, MarkerIndex, out x, out y, out z);
            marker.TimeStamp = System.DateTime.Now.Ticks;
            marker.xCoordinate = (int)(x * 1000);
            marker.yCoordinate = (int)(y * 1000);
            marker.zCoordinate = (int)(z * 1000);
            return marker;
        }


        /// <summary>
        /// Gets the number of connected cameras
        /// </summary>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraCount")]
        public static extern int CameraCount();

        /// <summary>
        /// Gets the X location of the camera
        /// </summary>
        /// <param name="index">The camera index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraXLocation")]
        public static extern float CameraLocationX(int index);

        /// <summary>
        /// Gets the Y location of the camera
        /// </summary>
        /// <param name="index">The camera index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraYLocation")]
        public static extern float CameraLocationY(int index);

        /// <summary>
        /// Gets the Z location of the camera
        /// </summary>
        /// <param name="index">The camera index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraZLocation")]
        public static extern float CameraLocationZ(int index);

        /// <summary>
        /// Gets the orientation of the camera
        /// </summary>
        /// <param name="camera">The camera index</param>
        /// <param name="index">The index of the orientation matrix</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraOrientationMatrix")]
        public static extern float CameraOrientation(int camera, int index);

        /// <summary>
        /// Gets the name of the camera
        /// </summary>
        /// <param name="index">The camera index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraName")]
        public static extern string CameraName(int index);

        /// <summary>
        /// Gets the number of markers that the camera can see
        /// </summary>
        /// <param name="index">The camera index</param>
        /// <returns>The result</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_CameraMarkerCount")]
        public static extern int CameraMarkerCount(int index);


        /// <summary>
        /// Turns a result code into a string
        /// </summary>
        /// <param name="result">The result code</param>
        /// <returns>The result string</returns>
        [DllImport(TrackingTools.DllLoc, EntryPoint = "TT_GetResultString")]
        public static extern string GetResultString(int result);

        #endregion

        #region Tracking Tools - DLL Checker

        /// <summary>
        /// Checks that NPTrackingTools.dll exists
        /// </summary>
        /// <returns></returns>
        public static bool CheckDLLExists()
        {
            bool exists = false;
            return exists = File.Exists(DllLoc);
        }

        #endregion

    }
}
