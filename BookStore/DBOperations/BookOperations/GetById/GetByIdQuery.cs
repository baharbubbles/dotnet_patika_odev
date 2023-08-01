using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetByIdQuery
{
    public class GetByIdQuery
    {
        public int BookId {get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetByIdQuery(BookStoreDbContext dbContext,int id, IMapper mapper)
        {
            BookId=id;
            _dbContext=dbContext;
            _mapper = mapper;
        }

        public BooksDetailViewModel Handle(){
            var book =  _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }

            BooksDetailViewModel Model = _mapper.Map<BooksDetailViewModel>(book); 

            return Model;
        }
    }

    public class BooksDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string Genre { get; set; }
    }
}