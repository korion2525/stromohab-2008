using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using system.Text;
using System.IO;
using System.Collections.Generic;

namespace StromoLight_Diagnostics
{
    [Serializable]
    public class PersonData
    {
        string personID;
        List<string> fileList;
        public PersonData(string personID)
        {
            this.personID = personID;
            
        }
    }
}
