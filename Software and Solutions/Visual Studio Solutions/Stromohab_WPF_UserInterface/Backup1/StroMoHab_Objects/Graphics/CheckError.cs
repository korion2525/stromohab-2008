using Tao.OpenGl;
using System;


namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Provides error checking routines for Stromohab.
    /// </summary>
    public class CheckError
    {
        private string _glErrorMessage = "OpenGL Error. Code: ";


        /// <summary>
        /// Instatiates a new CheckError object.
        /// </summary>
        public CheckError()
        {

        }

        public void CheckOpenGLErrors()
        {
            int glError = Gl.glGetError();

            switch (glError)
            {
                case 0:
                    {
                        break;
                    }
                case Gl.GL_INVALID_ENUM:
                    {
                        throw new Exception("GL_INVALID_ENUM. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_INVALID_VALUE:
                    {
                        throw new Exception("GL_INVALID_VALUE. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_INVALID_OPERATION:
                    {
                        throw new Exception("GL_INVALID_OPERATION. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_STACK_OVERFLOW:
                    {
                        throw new Exception("GL_STACK_OVERFLOW. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_STACK_UNDERFLOW:
                    {
                        throw new Exception("GL_STACK_UNDERFLOW. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_OUT_OF_MEMORY:
                    {
                        throw new Exception("GL_OUT_OF_MEMORY. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_TABLE_TOO_LARGE:
                    {
                        throw new Exception("GL_TABLE_TOO_LARGE. " + _glErrorMessage + glError.ToString());
                    }
                case Gl.GL_INVALID_FRAMEBUFFER_OPERATION_EXT:
                    {
                        throw new Exception("GL_INVALID_FRAMEBUFFER_OPERATION_EXT. " + _glErrorMessage + glError.ToString());
                    }
                default:
                    {
                        throw new Exception("Unknown OpenGL Error. Code: " + glError.ToString() + " . Try Google..!");
                    }
            }
        }


    }
}
