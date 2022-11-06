using System;
using System.Collections.Generic;

namespace indeas.Models
{
    public partial class Person
    {
        public Person()
        {
            Authors = new HashSet<Author>();
            Competencies = new HashSet<Competency>();
        }

        public int Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Ник
        /// </summary>
        public string? Nic { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? MiddleName { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// О себе
        /// </summary>
        public string? AboutMe { get; set; }
        /// <summary>
        /// Контакты
        /// </summary>
        public string? Contacts { get; set; }
        /// <summary>
        /// Закрытый
        /// </summary>
        public bool Private { get; set; }
        /// <summary>
        /// Модерация
        /// </summary>
        public bool Moderation { get; set; }
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

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Competency> Competencies { get; set; }
    }
}
