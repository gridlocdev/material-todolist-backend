using System.ComponentModel.DataAnnotations;

namespace Todo.Models
{
    public class TodoItem
    {
        [Required]
        [Key]
        public int id { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public bool completed { get; set; }


        public TodoItem(int id, string text, bool completed)
        {
            this.id = id;
            this.text = text;
            this.completed = completed;
        }

    }


}

