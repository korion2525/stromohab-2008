using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tao.OpenGl;
using System.Drawing.Imaging;
using System.Drawing;

namespace StroMoHab_Objects.Graphics
{
    public class TextureObject
    {
#region Member Variables

        string m_filepath;

        int m_textureID;

#endregion Member Variables

#region Properties

        /// <summary>
        /// The filename the texture is created from.
        /// </summary>
        public string FilePath
        {
            get
            {
                return m_filepath;
            }
            set
            {
                m_filepath = value;
            }
        }

        /// <summary>
        /// The OpenGL texture ID associated with the texture.
        /// </summary>
        public int TextureID
        {
            get
            {
                return m_textureID;
            }
        }

   

#endregion Properties

        public TextureObject(string bitmapFilepath)
        {
            m_filepath = Path.GetFileName(bitmapFilepath);

            System.Diagnostics.Debug.WriteLine("Currently generating texture from file: " + bitmapFilepath);
            //Load the bitmap into memory
            Bitmap currentBitmap = new Bitmap(bitmapFilepath);

            //Create locked storage space for image data
            Rectangle imageStorageSpace = new Rectangle(0, 0, currentBitmap.Width, currentBitmap.Height);

            //Populate storage space
            BitmapData currentBitmapData = new BitmapData();
            currentBitmapData = currentBitmap.LockBits(imageStorageSpace, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            //Generate new texture ID
            Gl.glGenTextures(1, out m_textureID);

            //Enable hardware generation of all mipmap levels
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_GENERATE_MIPMAP, Gl.GL_TRUE);

            Gl.glBindTexture(Gl.GL_TEXTURE_2D, m_textureID);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_R, Gl.GL_REPEAT);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 10, Gl.GL_RGB, currentBitmap.Width, currentBitmap.Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, currentBitmapData.Scan0);

            //Use maximum Anisotropic filtering if available
            if (((string)Gl.glGetString(Gl.GL_EXTENSIONS)).Contains("GL_EXT_texture_filter_anisotropic"))
            {
                float maxAnisotropy;
                Gl.glGetFloatv(Gl.GL_MAX_TEXTURE_MAX_ANISOTROPY_EXT, out maxAnisotropy);
                Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAX_ANISOTROPY_EXT, maxAnisotropy);
            }

            Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, Gl.GL_RGB, currentBitmap.Width, currentBitmap.Height, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, currentBitmapData.Scan0);

            currentBitmap.UnlockBits(currentBitmapData);
            currentBitmap.Dispose();


        }

        public TextureObject(string bitmapFilepath, int textureNumberToOverwrite)
        {
            m_filepath = Path.GetFileName(bitmapFilepath);

            System.Diagnostics.Debug.WriteLine("Currently generating texture from file: " + bitmapFilepath);
            //Load the bitmap into memory
            Bitmap currentBitmap = new Bitmap(bitmapFilepath);
            currentBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);

            //Create locked storage space for image data
            Rectangle imageStorageSpace = new Rectangle(0, 0, currentBitmap.Width, currentBitmap.Height);

            //Populate storage space
            BitmapData currentBitmapData = new BitmapData();
            currentBitmapData = currentBitmap.LockBits(imageStorageSpace, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            //Overwrite current texture ID
            m_textureID = textureNumberToOverwrite;
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, m_textureID);


            //Enable hardware generation of all mipmap levels
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_GENERATE_MIPMAP, Gl.GL_TRUE);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_R, Gl.GL_REPEAT);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 10, Gl.GL_RGB, currentBitmap.Width, currentBitmap.Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, currentBitmapData.Scan0);

            //Use maximum Anisotropic filtering if available
            if (((string)Gl.glGetString(Gl.GL_EXTENSIONS)).Contains("GL_EXT_texture_filter_anisotropic"))
            {
                float maxAnisotropy;
                Gl.glGetFloatv(Gl.GL_MAX_TEXTURE_MAX_ANISOTROPY_EXT, out maxAnisotropy);
                Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAX_ANISOTROPY_EXT, maxAnisotropy);
            }

            Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, Gl.GL_RGB, currentBitmap.Width, currentBitmap.Height, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, currentBitmapData.Scan0);

            currentBitmap.UnlockBits(currentBitmapData);
            currentBitmap.Dispose();


        }


    }
}
