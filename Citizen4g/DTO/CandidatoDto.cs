using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citizen4g.DTO
{
    public class CandidatoDto
    {
        public int idCandidates { get; set; }
        public int idCard { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public string DescriptionCampaign { get; set; }
        public string LinkCampaign { get; set; }
        public byte[] LogoCampaign { get; set; }
        public int idTown { get; set; }
        public int idUsers { get; set; }
        public IEnumerable<CiudadanosDto> CitizensxCandidate { get; set; }
    }
}