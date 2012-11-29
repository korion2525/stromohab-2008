using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StroMoHab_Objects.Objects
{
    /// <summary>
    /// Represents a patients next of kin
    /// </summary>
    [Serializable]
    public class SecondaryContact
    {
        private Address _address;
        private ContactNumber _contactNumber;
        private string _firstName;
        private string _lastName;
        public enum Relationship { Partner, Spouce, Parent, Child, Friend, Other};
        private Relationship _relationshipToPatient;


        /// <summary>
        /// Gets or sets the relationship between the Secondary Contact and the patient
        /// </summary>
        public Relationship RelationshipToPatient
        {
            get { return _relationshipToPatient; }
            set { _relationshipToPatient = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }


        /// <summary>
        /// Gets or sets the address
        /// </summary>
        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// Gets of sets the contact telephone number
        /// </summary>
        public ContactNumber ContactNumber
        {
            get { return _contactNumber; }
            set { _contactNumber = value; }
        }
    }
}
