using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoGPRC.Data;

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
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the todo item is complete.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Gets or sets the description of the todo item.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the date and time the todo item was created.
        /// </summary>
        /// <remarks>The date and time is in UTC.</remarks>
        public DateTime CreatedAt { get; set; }
    }
}
