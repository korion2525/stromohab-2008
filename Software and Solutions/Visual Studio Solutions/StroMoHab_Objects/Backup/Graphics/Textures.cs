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
                        if (Directory.Exists(Environment.CurrentDirectory + @"\Textures"))
                        {
                            textureFilesPath = new DirectoryInfo(Environment.CurrentDirectory) + @"Textures";
                            listOfTextureFilePaths = new List<string>(Directory.GetFiles(textureFilesPath, "*." + fileExtension));
                        }
                        else //assume running from Visual Studio Solutions
                        {
                            DirectoryInfo dInfo = new DirectoryInfo(Environment.CurrentDirectory);
                            // go back down towards the root looking for Visual studio solutions, if you get to the root then break
                            while (dInfo.Name != "Visual Studio Solutions")
                            {
                                if (dInfo.FullName == dInfo.Root.Name)
                                    break;
                                dInfo = dInfo.Parent;
                            }
                            //go down once more to get to root of working copy.
                            dInfo = dInfo.Parent;
                            textureFilesPath = dInfo.FullName + @"\Resources\Textures";
                            return (new List<string>(Directory.GetFiles(textureFilesPath, "*." + fileExtension)));
                        }
                        
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

//            System.Data.SqlClient.SqlConnection databaseConnection = new System.Data.SqlClient.SqlConnection(@"Data Source=APPC05\SQLEXPRESS;Initial Catalog=Stromohab;Integrated Security=True;Pooling=False;");
//            databaseConnection.Open();

//            foreach (string currentPath in listOfTextureFilePaths)
//            {

//                string insertString = @" INSERT INTO Bitmaps
//                                    (filepath)
//                                    VALUES ('" + currentPath + "')";

//                System.Data.SqlClient.SqlCommand mySqlCommand = new System.Data.SqlClient.SqlCommand(insertString, databaseConnection);


//                mySqlCommand.ExecuteNonQuery();

//            }
//            databaseConnection.Close();






            return (listOfTextureFilePaths);
        }

        public static List<string> ListOfTextureFilePaths(string fileExtension, bool areVideoTextures)
        {
            string videoFramesFolderPath = null;
            List<string> listOfVideoFrameFiles = null;
#if DEBUG
            videoFramesFolderPath = @"C:\Users\mag501\Desktop\DemoPathStablised\Augmented";
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
                    //if list is empty, add a null texture to the list so it matches with the OpenGL generated texture IDs (they start at 1).
                    if (listOfTextureObjects.Count == 0)
                    {
                        listOfTextureObjects.Add(null);
                    }
                    listOfTextureObjects.Add(new TextureObject(currentTextureFilepath));
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
