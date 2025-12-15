using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Todo.Shared
{
    public class CreateTaskDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public string Priority { get; set; } = "Medium";
    }
}
