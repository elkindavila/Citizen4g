//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Citizen4g.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class users_profiles
    {
        public int idUsers_Profiles { get; set; }
        public int idUsers { get; set; }
        public int idProfiles { get; set; }
    
        public virtual profile profile { get; set; }
        public virtual user user { get; set; }
    }
}
