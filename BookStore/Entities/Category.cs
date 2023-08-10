using System;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Common;

namespace BookStore.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}