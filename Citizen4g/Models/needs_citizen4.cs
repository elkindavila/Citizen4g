//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Citizen4g.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class needs_citizen4
    {
        public int idNeeds_Citizen4 { get; set; }
        public int idCitizen4 { get; set; }
        public int idNeeds { get; set; }
    
        public virtual citizen4 citizen4 { get; set; }
        public virtual need need { get; set; }
    }
}
