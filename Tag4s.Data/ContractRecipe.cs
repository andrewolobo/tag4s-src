//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tag4s.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContractRecipe
    {
        public ContractRecipe()
        {
            this.UserContracts = new HashSet<UserContract>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public System.Guid ContractId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }
        public string CssClass { get; set; }
    
        public virtual ICollection<UserContract> UserContracts { get; set; }
        public virtual Contract Contract { get; set; }
    }
}