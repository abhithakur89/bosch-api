using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoschApi.Entities.Data
{
    public class Site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SiteId { get; set; }
        public string SiteName { get; set; }

        public string SiteDescription { get; set; }

    }
}
