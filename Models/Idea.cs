using System;
using System.Collections.Generic;

namespace indeas.Models
{
    public partial class Idea
    {
        public Idea()
        {
            Authors = new HashSet<Author>();
        }

        public int Id { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public int? Category { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Аннотация
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Ссылки
        /// </summary>
        public string? Links { get; set; }
        /// <summary>
        /// Файлы
        /// </summary>
        public string? Files { get; set; }
        /// <summary>
        /// Опубликована
        /// </summary>
        public DateTime? Published { get; set; }
        /// <summary>
        /// Закрыта
        /// </summary>
        public DateTime? Closed { get; set; }
        /// <summary>
        /// Модерация
        /// </summary>
        public bool Moderation { get; set; }
        /// <summary>
        /// Инновационная
        /// </summary>
        public bool Innovative { get; set; }
        /// <summary>
        /// Перспективная
        /// </summary>
        public bool Promising { get; set; }
        /// <summary>
        /// Реализуемая
        /// </summary>
        public bool Implemented { get; set; }
        /// <summary>
        /// Тэги
        /// </summary>
        public string? Tags { get; set; }
        /// <summary>
        /// Плюс - число положительных оценок
        /// </summary>
        public long Plus { get; set; }
        /// <summary>
        /// Минус - число отрицательных оценок
        /// </summary>
        public long Minus { get; set; }
        /// <summary>
        /// Рейтинг
        /// </summary>
        public long Rating { get; set; }

        public virtual Category? CategoryNavigation { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}
