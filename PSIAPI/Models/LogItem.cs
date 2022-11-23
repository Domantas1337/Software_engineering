using PSIAPI.States;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIAPI.Models
{

    public class LogItem {
        [Key]
        public int? Id { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public string? exceptionDetails { get; set; }
    }
}
