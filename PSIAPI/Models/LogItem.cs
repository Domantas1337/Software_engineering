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
        public string? Id { get; set; }
        public string dateTime { get; set; } = DateTime.Now.ToString();
        public string? type {get; set;}
        public string? line { get; set; }
        public string? exceptionDetails { get; set; }
    }
}
