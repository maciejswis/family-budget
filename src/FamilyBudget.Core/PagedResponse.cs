namespace FamilyBudget.Core
{
    public class PagedResponse<TEntity> 
    {
        public ICollection<TEntity> Results { get; set; }

        public long Total { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public long TotalPages => (long)Math.Ceiling((decimal)Total / PageSize);

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => TotalPages > CurrentPage;
    }
}

