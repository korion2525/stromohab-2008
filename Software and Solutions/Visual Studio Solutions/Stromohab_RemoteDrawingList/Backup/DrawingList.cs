using System;
using System.Collections.Generic;

using StroMoHab_Objects.Graphics;
using StroMoHab_Objects.Interfaces;

using System.IO;
using System.Drawing;

namespace StromoLight_RemoteDrawingList
{
    /// <summary>
    /// 
    /// </summary>
    public class DrawingList : MarshalByRefObject
    {
        #region Member Variables

        private static List<OpenGlObject> m_objectsToDraw = new List<OpenGlObject>();
        private static byte[] taskScreenShot;
        private static bool generatingScreenShot = false;
        private static bool fullScreen = false;
        private static bool _loading = true;

        /// <summary>
        /// Set to true when in full screen mode
        /// </summary>
        public bool FullScreen
        {
            get { return fullScreen; }
            set { fullScreen = value; }
        }

        /// <summary>
        /// Set to true while the visualiser is still loading
        /// </summary>
        public bool VisualiserLoading
        {
            get { return _loading; }
            set { _loading = value; }
        }

        /// <summary>
        /// Set to true when the Visualiser is in the process of generating a new screen shot
        /// </summary>
        public bool GeneratingScreenShot
        {
            get { return generatingScreenShot; }
            set { generatingScreenShot = value; }
        }

        /// <summary>
        /// The byte array containing the screenshot
        /// </summary>
        public byte[] TaskScreenShot
        {
            get { return taskScreenShot; }
            set { taskScreenShot = value; }
        }

        /// <summary>
        /// Avatar moved in task designer delegate
        /// </summary>
        /// <param name="zPosition"></param>
        public delegate void AvatarMovedInTaskDesignerEventHandler(float zPosition);

        /// <summary>
        /// Avatar moved in visualiser delegate.
        /// </summary>
        /// <param name="zPosition"></param>
        public delegate void AvatarMovedInVisualiserEventHandler(float zPosition);

        /// <summary>
        /// Triggered when avatar position is changed in the task designer.
        /// </summary>
        public static event AvatarMovedInTaskDesignerEventHandler AvatarMovedInTaskDesigner;

        private event AvatarMovedInVisualiserEventHandler m_AvatarMovedInVisualiser;

        /// <summary>
        /// Triggered when avatar position is changed in the visualiser.
        /// </summary>
        public event AvatarMovedInVisualiserEventHandler AvatarMovedInVisualiser
        {
            add
            {
                m_AvatarMovedInVisualiser+= value;
            }
            remove
            {
                m_AvatarMovedInVisualiser -= value;
            }
        }

        /// <summary>
        /// Updates the avatar position in the Task Designer.
        /// </summary>
        /// <param name="zPosition"></param>
        public void MoveAvatarInTaskDesigner(float zPosition)
        {
            if (m_AvatarMovedInVisualiser != null)
            {
                m_AvatarMovedInVisualiser(zPosition);
            }
        }


        #endregion Member Variables

        #region Properties

        /// <summary>
        /// The list of objects to draw.
        /// </summary>
        public List<OpenGlObject> ObjectsToDraw
        {
            get 
            { 
                return m_objectsToDraw; 
            }
            set
            {
                m_objectsToDraw = value;
            }
        }

        #endregion Properties

        #region Private Methods
        /// <summary>
        /// Checks the supplied index corresponds to an editable object (usually obstacle) in the drawing list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool ObjectIndexInBoundsAndNotCorridor(int index)
        {
            if (index < m_objectsToDraw.Count && index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Adds an OpenGl object to the drawing list.
        /// </summary>
        /// <param name="objectToAdd"></param>
        public void Add(OpenGlObject objectToAdd)
        {
            m_objectsToDraw.Add(objectToAdd);
        }

        /// <summary>
        /// Removes the object at a specified index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index < m_objectsToDraw.Count)
            {
                m_objectsToDraw.RemoveAt(index);
            }
        }

        /// <summary>
        /// Replaces an object at a specified index with a new one.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newObject"></param>
        public void ReplaceAt(int index, OpenGlObject newObject)
        {
            m_objectsToDraw[index] = newObject;
        }

        /// <summary>
        /// Sets a new width for the object at list position: index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        public void SetNewObjectWidth(int index, float newValue)
        {
            //if (index < m_objectsToDraw.Count &&index >=0)
            {
                m_objectsToDraw[index].Length = newValue;
            }
        }

        /// <summary>
        /// Sets a new Height for the object at list position: index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        public void SetNewObjectHeight(int index, float newValue)
        {
            if (index < m_objectsToDraw.Count && index >=0)
            {
                m_objectsToDraw[index].Height = newValue;
            }
        }

        /// <summary>
        /// Sets a new Depth for the object at list position: index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        public void SetNewObjectDepth(int index, float newValue)
        {
            if (index < m_objectsToDraw.Count && index >= 0)
            {
                m_objectsToDraw[index].Depth = newValue;
            }
        }

        /// <summary>
        /// Sets a new Y coordinate value for the object at list position: index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        public void SetNewObjectY(int index, float newValue)
        {
            if (index < m_objectsToDraw.Count && index >= 0)
            {
                m_objectsToDraw[index].Y = newValue;
            }
        }

        /// <summary>
        /// Sets a new X coordinate value for the object at list position: index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue"></param>
        public void SetNewObjectX(int index, float newValue)
        {
            if (index < m_objectsToDraw.Count && index >= 0)
            {
                m_objectsToDraw[index].X = newValue;
            }
        }

        /// <summary>
        /// Returns the OpenGLObject in the drawing list position specified by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public OpenGlObject GetObjectFromList(int index)
        {
            if (ObjectIndexInBoundsAndNotCorridor(index))
            {
                return m_objectsToDraw[index];
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Object index " + index.ToString() + " does not correspond to an item in the drawing list");
                return null;
            }
        }



        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            m_objectsToDraw.Clear();

        }

        /// <summary>
        /// Sets the "Selected" property of an item in the draw list.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="selected"></param>
        public void SetSelected(int index, bool selected)
        {
            if (index < m_objectsToDraw.Count && index >=0)
            {
                m_objectsToDraw[index].Selected = selected;
            }
        }

        /// <summary>
        /// Sets the texture number of an item in the draw list
        /// </summary>
        /// <param name="index"></param>
        /// <param name="textureNumber"></param>
        public void SetTextureNumber(int index, int textureNumber)
        {
            if (index < m_objectsToDraw.Count && index >= 0)
            {
                m_objectsToDraw[index].TextureNumber = textureNumber;
            }
        }



        /// <summary>
        /// Moves the avatar to a new Z Position in the Visualiser.
        /// </summary>
        /// <param name="zPosition"></param>
        public void MoveAvatarInVisualiser(float zPosition)
        {
            if (AvatarMovedInTaskDesigner != null)
            {
                AvatarMovedInTaskDesigner(zPosition);
            }
        }

        /// <summary>
        /// Opens the connection - call to minimise delay when client UI is initally used.
        /// </summary>
        /// <returns>
        /// Bool indicating if connection to Visualiser succeded.
        /// </returns>
        /// <exception cref="System.Net.Sockets.SocketException">
        /// This is thrown if a connection cannot be made to the Visualiser. If the Visualiser is located on the same machine,
        /// catch this exception and lauch the Visualiser using a new System.Diagnostics.Process.
        /// </exception>
        public bool Initialise()
        {
            return true;
        }


        #endregion Public Methods


        private static StroMoHab_Objects.Interfaces.IVisualiser _visualiserController;
        private static StroMoHab_Objects.Interfaces.IVideoBackground _videoBackground;

        /// <summary>
        /// Accesser for VideoBackground stored in the remote list.
        /// </summary>
        public StroMoHab_Objects.Interfaces.IVideoBackground VideoBackground 
        {
            get
            {
                return (_videoBackground);
            }
            set
            {
                _videoBackground = value;
            }
        }

        /// <summary>
        /// Accessor for VisualiserController stored in the remote list.
        /// </summary>
        public StroMoHab_Objects.Interfaces.IVisualiser VisualiserController 
        {
            get
            {
                return _visualiserController;
            }
            set
            {
                _visualiserController = value;
            }

        }

        /// <summary>
        /// Getter for VideoBackground.ContainingFolder.
        /// </summary>
        /// <returns>path to folder containing current video frame files.</returns>
        public string GetVideoBackgroundContainingFolder()
        {
            return this.VideoBackground.ContainingFolder;
        }

        /// <summary>
        /// Setter for VideoBackground.ContainingFolder.
        /// </summary>
        /// <param name="containingFolder">path to folder containing current video frame files.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetVideoBackgroundContainingFolder(string containingFolder)
        {
            if (containingFolder != null)
            {
                this.VideoBackground.ContainingFolder = containingFolder;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Getter for VisualiserController.UseVideoBackground.
        /// </summary>
        /// <returns>flag indicating if video background is currently in use.</returns>
        public bool UseVideoBackground()
        {
            return this.VisualiserController.UseVideoBackground;
        }

        /// <summary>
        /// Setter for VisualiserController.UseVirtualBackground.
        /// </summary>
        /// <param name="isVirtual">flag setting status of requested virtual background use.</param>
        public void SetVirtualTask(bool isVirtual)
        {
            this.VisualiserController.UseVirtualBackground = isVirtual;
        }

        /// <summary>
        /// Setter for VisualiserController.UseVideoBackground.
        /// </summary>
        /// <param name="isVideo">flag setting status of requested video background use.</param>
        public void SetVideoTask(bool isVideo)
        {
            this.VisualiserController.UseVideoBackground = isVideo;
        }

    }
}
