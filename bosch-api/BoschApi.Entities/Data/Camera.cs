using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoschApi.Entities.Data
{
    [Table("Cameras")]
    public class Camera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CameraId { get; set; }
        public string CameraName { get; set; }
        public string CameraIP { get; set; }
        public string Model { get; set; }
        public string AdditionalDetail { get; set; }

        [ForeignKey("GateId")]
        public int GateId { get; set; }
        public virtual Gate Gate { get; set; }

    }
}
