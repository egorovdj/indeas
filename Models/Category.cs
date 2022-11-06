using System;
using System.Collections.Generic;

namespace indeas.Models
{
    public partial class Category
    {
        public Category()
        {
            Dictionaries = new HashSet<Dictionary>();
            Ideas = new HashSet<Idea>();
            InverseSeniorNavigation = new HashSet<Category>();
        }

        public int Id { get; set; }
        /// <summary>
        /// Старшая
        /// </summary>
        public int? Senior { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Тэги
        /// </summary>
        public string? Tags { get; set; }

        public virtual Category? SeniorNavigation { get; set; }
        public virtual ICollection<Dictionary> Dictionaries { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<Category> InverseSeniorNavigation { get; set; }
    }
}
