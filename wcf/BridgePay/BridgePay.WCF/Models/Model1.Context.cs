//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BridgePay.WCF.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BridgepayDBEntities : DbContext
    {
        public BridgepayDBEntities()
            : base("name=BridgepayDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Transactions> Transactions { get; set; }
    
        public virtual int ApprovePendings()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ApprovePendings");
        }
    
        public virtual int SetToStatus(Nullable<int> id, Nullable<int> status)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SetToStatus", idParameter, statusParameter);
        }
    }
}
