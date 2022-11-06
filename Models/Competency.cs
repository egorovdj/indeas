using System;
using System.Collections.Generic;

namespace indeas.Models
{
    public partial class Competency
    {
        public int Id { get; set; }
        /// <summary>
        /// Лицо
        /// </summary>
        public int? Person { get; set; }
        /// <summary>
        /// Профессия
        /// </summary>
        public string? Profession { get; set; }
        /// <summary>
        /// Специалист
        /// </summary>
        public string? Specialist { get; set; }
        /// <summary>
        /// Теги
        /// </summary>
        public string? Tags { get; set; }

        public virtual Person? PersonNavigation { get; set; }
    }
}
