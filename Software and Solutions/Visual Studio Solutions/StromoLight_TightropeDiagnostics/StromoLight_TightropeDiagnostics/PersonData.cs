using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace StromoLight_Diagnostics
{
    [Serializable]
    public class PersonData
    {
        string personID = "";
        List<string> fileList;
        string personNotes = "";
        uint age = 0;
        string gender = "F";
        string footPreference = "N";

        public PersonData()
        {
            fileList = new List<string>();
        }

        public string PersonID
        {
            get { return this.personID; }
            set { this.personID = value; }
        }

        public void AddFileName(string fileName)
        {
            this.fileList.Add(fileName);
        }

        public void RemoveFilename(string fileName)
        {
            this.fileList.Remove(fileName);
        }

        public void RemoveLastFilename()
        {
            this.fileList.RemoveAt(fileList.Count - 1);
        }

        public void AddPersonNotes(string notes)
        {
            this.personNotes += notes;
        }

        /*public string PersonNotes
        {
            get { return this.personNotes; }
            set { this.personNotes = value; }
        }*/

        public List<string> FileList
        {
            get { return this.fileList; }
            set { this.fileList = value; }
        }

        public uint Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string FootPreference
        {
            get { return footPreference; }
            set { footPreference = value; }
        }
    }
}
