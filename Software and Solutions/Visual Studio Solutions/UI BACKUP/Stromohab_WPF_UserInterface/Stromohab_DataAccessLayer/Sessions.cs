using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace Stromohab_DataAccessLayer
{
    public class Sessions
    {

        internal static IQueryable SessionDatasetForCliniciansPatient(int patientId, string userName)
        {
            stromohabDevEntities db = new stromohabDevEntities();

            var patientSessions = from p in db.sessions
                                  where p.Patients_idPatient == patientId && p.Patients_Clinicians_cUserName == userName
                                  select p;

            return patientSessions;
        }
    }
}
