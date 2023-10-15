using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Core
{
    public enum SortDirection
    {
        ASC,
        DESC
    }

    public class Pageable
    {
        [Range(1, 1000)]
        public int PageNumber { get; set; }
        [Range(1, 1000)]
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }

        public Pageable()
        {
            this.PageSize = 20;
            this.PageNumber = 1;
        }

        public Pageable(int size, int number)
        {
            this.PageSize = size;
            this.PageNumber = number < 1 ? 1 : number;
        }
    }
}

