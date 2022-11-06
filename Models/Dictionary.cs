using System;
using System.Collections.Generic;

namespace indeas.Models
{
    public partial class Dictionary
    {
        public int Id { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public int? Category { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Тэги
        /// </summary>
        public string? Tags { get; set; }

        public virtual Category? CategoryNavigation { get; set; }
    }
}
