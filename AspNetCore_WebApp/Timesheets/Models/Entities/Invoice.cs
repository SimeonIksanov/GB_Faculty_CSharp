using System;
using System.Collections.Generic;
using Models.ValueObjects;

namespace Models.Entities
{
    public class Invoice : Entity
    {
        public Guid ContractId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Money Sum { get; set; }

        public Contract Contract { get; set; }
        public List<Sheet> Sheets { get; set; } = new List<Sheet>();

        public Invoice() { }
    }
}
