using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoGPRC.Models
{
    /// <summary>
    /// Represents a todo item.
    /// </summary>
    public class TodoItems
    {
        /// <summary>
        /// Gets or sets the ID of the todo item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the todo item.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the todo item is complete.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Gets or sets the description of the todo item.
        /// </summary>
        public string? Description { get; set; }
    }
}
