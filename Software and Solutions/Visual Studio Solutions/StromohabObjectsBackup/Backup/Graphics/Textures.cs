using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using Tao.OpenGl;
using System.IO;

namespace StroMoHab_Objects.Graphics
{
    /// <summary>
    /// Loads and stores the texture files for OpenGl use.
    /// </summary>
    public class Textures
    {
        #region Member Variables
        
        //Number of textures to load
        const int MAX_NUMBER_OF_TEXTURES = 99;
        private static int m_actualNumberOfTexturesLoaded = 0;
        private static int m_actualNumberOfVideoTexturesLoaded = 0;

        //Array that stores loaded textures
        static int[] m_textures = new int[MAX_NUMBER_OF_TEXTURES];
        static int[] m_videoTextures = new int[10];

        #endregion Member Variables

        #region Properties

        /// <summary>
        /// List of loaded textured objects from the Textures directory
        /// </summary>
        public static List<TextureObject> listOfTextureObjects = null;


        /// <summary>
        /// The number of textures currently loaded into memory.
        /// </summary>
        public static int NumberOfLoadedTextures
        {
            get 
            {
                return m_actualNumberOfTexturesLoaded; 
            }
        }

        #endregion Properties


        #region Public Methods

        /// <summary>
        /// Returns a texture from the loaded texture array
        /// </summary>
        public static int GetTexture(int textureNumber)
        {
            //return (m_textures[textureNumber]);
            return (listOfTextureObjects[textureNumber].TextureID);
        }   
        
        /// <summary>
        /// Returns a list of filepaths to files in the textures folder.
        /// </summary>
        /// <returns></returns>
        public static List<string> ListOfTextureFilePaths(string fileExtension) //TODO: Validate file extension input
        {

            string textureFilesPath = null;
            List<string> listOfTextureFilePaths = null;
            try
            {
                //load path to textures folder from StromoLight_Objects settings file (defaults to Resources directory in working copy)
                textureFilesPath = Properties.Settings.Default.TexturesPath;
                //attempt to load textures from Resources folder in working copy.
                try
                {
                    listOfTextureFilePaths = new List<string>(Directory.GetFiles(textureFilesPath, "*." + fileExtension));
                }
                catch (DirectoryNotFoundException workingCopyEx)
                {
                    //assume running in installation directory + try to load textures from there.
                    try
                    {
                        textureFilesPath = @"..\Textures";
                        listOfTextureFilePaths = new List<string>(Directory.GetFiles(textureFilesPath, "*." + fileExtension));
                        System.Diagnostics.Debug.WriteLine(workingCopyEx.Message);
                    }
                    catch (DirectoryNotFoundException releasedEx)
                    {
                        if (System.Windows.Forms.MessageBox.Show("Cannot find texture files. Navigate to directory?", "Cannot Find Texture Files", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                            if (System.Environment.UserName == "mag501")
                            {
                                folderBrowser.RootFolder = Environment.SpecialFolder.MyDocuments; // + "\\Working_Copy\\Software and Solutions\\Resources\\Textures";
                            }
                            folderBrowser.ShowDialog();
                            textureFilesPath = folderBrowser.SelectedPath;
                            listOfTextureFilePaths = new List<string>(Directory.GetFiles(textureFilesPath, "*." + fileExtension));
                        }
                        else
                        {

                            MessageBox.Show("Visualiser closing due to exception: " + releasedEx.Message, "Program closing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            //log failure to load and exit.
                            System.Diagnostics.Debug.WriteLine(releasedEx.Message);
                            Environment.Exit(-1);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                System.Diagnostics.Debug.WriteLine("File open failed. Exception message: " + ex.Message);
                System.Windows.Forms.MessageBox.Show("Failed to load all textures. Application closing", "Texture Loading Failed");
                Environment.Exit(-1);
                //return;
            }

            return (listOfTextureFilePaths);
        }

        public static List<string> ListOfTextureFilePaths(string fileExtension, bool areVideoTextures)
        {
            string videoFramesFolderPath = null;
            List<string> listOfVideoFrameFiles = null;
#if DEBUG
            videoFramesFolderPath = "C:\\Users\\mag501\\Desktop\\Path1JPEG";
            listOfVideoFrameFiles = new List<string>(Directory.GetFiles(videoFramesFolderPath, "*." + fileExtension));

            return (listOfVideoFrameFiles);
#endif

#if !DEBUG
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select video folder containing JPEG frames.";


            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                videoFramesFolderPath = folderBrowser.SelectedPath;
                listOfVideoFrameFiles = new List<string>(Directory.GetFiles(videoFramesFolderPath, "*." + fileExtension));

                return (listOfVideoFrameFiles);
            }
            else
            {
                return (null);
            }
#endif
        }




        public static List<TextureObject> listOfVideoTextureObjects = null;

        /// <summary>
        /// Loads all textures. Must be called before attempting to draw object using textures.
        /// </summary>
        public static void LoadTextures()
        {
            //get list of texture file paths
            List<string> listOfTextureFilePaths = new List<string>(ListOfTextureFilePaths("bmp"));

                if (listOfTextureFilePaths != null)
                {
                    listOfTextureObjects = new List<TextureObject>(listOfTextureFilePaths.Count);
                    foreach (string currentTextureFilepath in listOfTextureFilePaths)
                    {
                        listOfTextureObjects.Add(new TextureObject(currentTextureFilepath));
                    }
                }

            //    listOfTextureFilePaths = new List<string>(ListOfTextureFilePaths("jpeg",true));

            //if (listOfTextureFilePaths != null)
            //{
            //    listOfVideoTextureObjects = new List<TextureObject>(listOfTextureFilePaths.Count);
            //    for(int i=0;i<10;i++)
            //    {
            //        listOfVideoTextureObjects.Add(new TextureObject(listOfTextureFilePaths[i]));
            //    }
            //}

        }


        

        public static void LoadVideoTextures()
        {
            //Loaded Bitmaps
            BitmapData[] videoTextureFileData = new BitmapData[MAX_NUMBER_OF_TEXTURES];
            Bitmap[] videoTextureImages = new Bitmap[MAX_NUMBER_OF_TEXTURES];
            List<string> videoTextureFilesPath = null; //Texture file paths
            m_actualNumberOfVideoTexturesLoaded = 0;

            Gl.glGenTextures(MAX_NUMBER_OF_TEXTURES, m_videoTextures);

            videoTextureFilesPath = new List<string>(GetVideoTextureFiles());
            
                //load bitmaps into array for OpenGl.
                foreach (string currentVideoTexture in videoTextureFilesPath)
                {
                    videoTextureImages[m_actualNumberOfVideoTexturesLoaded] = new Bitmap(currentVideoTexture);
                    m_actualNumberOfVideoTexturesLoaded++;
                }
  
            //generate textures from all bitmaps
            for (int i = 0; i < videoTextureFilesPath.Count; i++)
            {
                Rectangle rectangle = new Rectangle(0, 0, videoTextureImages[i].Width, videoTextureImages[i].Height);

                videoTextureFileData[i] = videoTextureImages[i].LockBits(rectangle, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                //Enable hardware generation of all mipmap levels
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_GENERATE_MIPMAP, Gl.GL_TRUE);

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, m_videoTextures[i]);

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);

                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_R, Gl.GL_REPEAT);

                Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 10, Gl.GL_RGB, videoTextureImages[i].Width, videoTextureImages[i].Height, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, videoTextureFileData[i].Scan0);

                //Use maximum Anisotropic filtering if available
                if (((string)Gl.glGetString(Gl.GL_EXTENSIONS)).Contains("GL_EXT_texture_filter_anisotropic"))
                {
                    float maxAnisotropy;
                    Gl.glGetFloatv(Gl.GL_MAX_TEXTURE_MAX_ANISOTROPY_EXT, out maxAnisotropy);
                    Gl.glTexParameterf(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAX_ANISOTROPY_EXT, maxAnisotropy);
                }

                Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, Gl.GL_RGB, videoTextureImages[i].Width, videoTextureImages[i].Height, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, videoTextureFileData[i].Scan0);

                if (videoTextureImages[i] != null)
                {
                    videoTextureImages[i].UnlockBits(videoTextureFileData[i]);
                    videoTextureImages[i].Dispose();
                }
            }
        }

        #endregion Public Methods


        private static List<string> GetVideoTextureFiles()
        {
            //Get frame file paths
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.Description = "Browse for video frame files";

            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                System.Diagnostics.Debug.WriteLine("Starting filepath retrieval..");
 
                List<string> fileList = new List<string>(Directory.GetFiles(openFolderDialog.SelectedPath, "*.jpeg"));
                System.Diagnostics.Debug.WriteLine("Filepath retrieval complete");
                return (fileList);
            }
            else
            {
                return (null);
            }

        }
    }
}
