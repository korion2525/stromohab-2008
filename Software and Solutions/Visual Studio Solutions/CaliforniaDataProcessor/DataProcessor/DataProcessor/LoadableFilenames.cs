using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessor
{
    public static class LoadableFilenames
    {
        static List<string> m_Filenames = null;

        public static void Initialise()
        {
            if (m_Filenames == null)
            {
                m_Filenames = new List<string>();
                m_Filenames.Add("PostUnSwapped.csv");
                m_Filenames.Add("_Data.csv");
                m_Filenames.Add("DeSwappedFootFile.csv");
                m_Filenames.Add("FeetDown_Accuracy.csv");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("List Already Initialised");
            }

        }


        public static List<string> Filenames
        {
            get
            {
                return m_Filenames;
            }
        }
                

    }
}
