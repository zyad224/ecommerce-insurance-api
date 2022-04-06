using Insurance.Domain.Base;
using Insurance.Domain.DomainExceptions;
using Insurance.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Insurance.Domain.Entities
{
    public class Surcharge:BaseEntity
    {
       
        [Key]
        public string Id { get; private set; }
        public float SurChargeFees { get; private set; }
        public int ProductTypeId { get; private set; }

        [Timestamp] // sql server db 
        public DateTime RowVersion { get; set; }

        private Surcharge()
        {

        }
        public Surcharge(int productTypeID, float surChargeFees)
        {
            if ((productTypeID == 0) || (surChargeFees == 0))
                throw new InvalidSurchargeException("Invalid Surcharge Parameters");

            Id = UUIDGenerator.NewUUID();
            SurChargeFees = surChargeFees;
            ProductTypeId = productTypeID;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
        }
        public void SetSurChargeFees(float surChargeFees)
        {
            this.SurChargeFees = surChargeFees;
        }
        public void SetProductTypeId(int productTypeId)
        {
            this.ProductTypeId = productTypeId;
        }
    }
}
