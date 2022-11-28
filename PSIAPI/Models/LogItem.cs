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
        public string ID { get; set; }
        public string? Date { get; set; }
        public string? Details { get; set; }
    }
}
