using System;
using System.Collections.Generic;

namespace indeas.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        /// <summary>
        /// Идея
        /// </summary>
        public int? Idea { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        public int? Author1 { get; set; }

        public virtual Person? Author1Navigation { get; set; }
        public virtual Idea? IdeaNavigation { get; set; }
    }
}
