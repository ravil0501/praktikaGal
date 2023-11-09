namespace praktikaGal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coutry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coutry()
        {
            Users = new HashSet<User>();
        }

        [Required]
        [StringLength(50)]
        public string SymbolCode { get; set; }

        [Key]
        public int NumbersCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameRus { get; set; }

        [Required]
        [StringLength(100)]
        public string NameEng { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
