using Tao.OpenGl;

namespace Skeleton
{
    public abstract class BodyPart
    {
        protected const float SCALLINGFACTOR = 600f;


        protected abstract void DrawBodyPart();

        /// <summary>
        /// Draws the body part based on the trackable
        /// </summary>
        /// <param name="trackable"></param>
        public virtual void Draw(Stromohab_MCE.Trackable trackable)
        {
            MoveIn(trackable);
            SetDrawingMode(true);
            DrawBodyPart();
            SetDrawingMode(false);
            DrawBodyPart();
            MoveOut(trackable);
        }

        /// <summary>
        /// Moves to the center of the trackalbe and sets the orientation so that the body part can be drawn
        /// </summary>
        /// <param name="trackable"></param>
        protected virtual void MoveIn(Stromohab_MCE.Trackable trackable)
        {
            // Move to center point of the trackalbe
            Gl.glTranslatef(trackable.xCoordinate / SCALLINGFACTOR, trackable.yCoordinate / SCALLINGFACTOR -0.6f, - trackable.zCoordinate / SCALLINGFACTOR);
            // Rotate based on Pitch Yaw and Roll
            Gl.glRotatef((float)trackable.Pitch, 1f, 0f, 0f);
            Gl.glRotatef((float)trackable.Yaw, 0f, -1f, 0f);
            Gl.glRotatef((float)trackable.Roll, 0f, 0f, -1f);
        }

        /// <summary>
        /// Moves back to x/y/z = 0 and resets the orientation
        /// </summary>
        /// <param name="trackable"></param>
        protected virtual void MoveOut(Stromohab_MCE.Trackable trackable)
        {
            // Rotate back
            Gl.glRotatef((float)trackable.Roll, 0f, 0f, 1f);
            Gl.glRotatef((float)trackable.Yaw, 0f, 1f, 0f);
            Gl.glRotatef((float)trackable.Pitch, -1f, 0f, 0f);
            // Move back
            Gl.glTranslatef(-trackable.xCoordinate / SCALLINGFACTOR, -trackable.yCoordinate / SCALLINGFACTOR + 0.6f, trackable.zCoordinate / SCALLINGFACTOR);
        }

        /// <summary>
        /// Sets the Polygon drawing mode to use
        /// </summary>
        /// <param name="fillNotMesh"></param>
        protected virtual void SetDrawingMode(bool fillNotMesh)
        {
            if(fillNotMesh) // Draw a filled polygon
            {
                Gl.glColor4f(1f, 1f, 1f, 0f);
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            }
            else // Draw a wire mesh (line) polygon
            {
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                Gl.glColor4f(0f, 0f, 0f, 0f);
            }
        }
    }
}