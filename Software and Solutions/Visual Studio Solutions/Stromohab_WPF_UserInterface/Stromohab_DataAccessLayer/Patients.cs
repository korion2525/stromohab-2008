using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Stromohab_DataAccessLayer
{
    static class Patients
    {

        internal static IQueryable  PatientDataSetForClinician(string userName)
        {
            stromohabDevEntities db = new stromohabDevEntities();

            var clinicansPatients = from p in db.patients
                                    where p.Clinicians_cUserName == userName
                                    select
                                        new
                                            {
                                                p.idPatient,
                                                p.pFirstName,
                                                p.pLastName,
                                                p.pDateOfBirth,
                                                p.pGender,
                                                p.pContactNumber
                                            };

            return clinicansPatients;
        }

        internal static void AddPatient(string firstNames, string lastName, DateTime dateOfBirth, string gender, string contactNumber, string clinicianUserName)
        {
           stromohabDevEntities db = new stromohabDevEntities();

            clinician patientsClinician = db.clinicians.Single(c => c.cUserName == clinicianUserName);

            patientsClinician.patients.Add(new patient
                                               {
                                                   pFirstName = firstNames,
                                                   pLastName = lastName,
                                                   pDateOfBirth = dateOfBirth,
                                                   pGender = gender,
                                                   pContactNumber = contactNumber,
                                                   Clinicians_cUserName = clinicianUserName
                                               });

            db.SaveChanges();
        }

        internal static void UpdatePatient(patient patientToUpdate)
        {
            stromohabDevEntities db = new stromohabDevEntities();

            var dbPatientInfo = db.patients
                .Where(w => w.idPatient == patientToUpdate.idPatient)
                .SingleOrDefault();

            if (dbPatientInfo != null)
            {
                dbPatientInfo.idPatient = patientToUpdate.idPatient;
                dbPatientInfo.pContactNumber = patientToUpdate.pContactNumber;
                dbPatientInfo.pDateOfBirth = patientToUpdate.pDateOfBirth;
                dbPatientInfo.pFirstName = patientToUpdate.pFirstName;
                dbPatientInfo.pGender = patientToUpdate.pGender;
                dbPatientInfo.pLastName = patientToUpdate.pLastName;
                db.SaveChanges();
            }
        }

        internal static void DeletePatientByID(int patientId)
        {
            stromohabDevEntities db = new stromohabDevEntities();

            var patientToEdit = (from p in db.patients
                                 where p.idPatient == patientId
                                 select p).FirstOrDefault();

            db.DeleteObject(patientToEdit);
            db.SaveChanges();
        }

        internal static string PatientFirstNamesFromPatientId(int patientId)
        {
            patient ctx = SelectPatientFromPatientId(patientId);
            return (ctx.pFirstName);
        }

        internal static string PatientLastNameFromPatientId(int patientId)
        {
            patient ctx = SelectPatientFromPatientId(patientId);
            return (ctx.pLastName);
        }

        internal static patient SelectPatientFromPatientId(int patientToEditId)
        {
            stromohabDevEntities db = new stromohabDevEntities();

            return ((from p in db.patients
                     where p.idPatient == patientToEditId
                     select p).FirstOrDefault());
        }
    }
}
