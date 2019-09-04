using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citizen4g.DTO
{
    public class CiudadanosDto
    {
        public int idCitizen4 { get; set; }
        public string FullName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public string CellPhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<int> economicActivity { get; set; }
        public Nullable<int> educationLevel { get; set; }
        public Nullable<sbyte> HeadFamily { get; set; }
        public Nullable<sbyte> EmployeedNow { get; set; }
        public string WageLevel { get; set; }
        public Nullable<int> TypeTransportUse { get; set; }
        public string Profession { get; set; }
        public Nullable<sbyte> WorkEast { get; set; }
        public Nullable<int> CivilStatus { get; set; }
        public int idTown { get; set; }
        public int idUsers { get; set; }
        public Nullable<int> idCandidates { get; set; }
        public Nullable<int> idSector { get; set; }

    }
}