using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail{
public class GetAuthorDetailQuery
{
	public int AuthorId { get; set; }
	private readonly IBookStoreDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}
	
	public AuthorDetailViewModel Handle()
	{
		var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);
		
		if (author is null)
			throw new InvalidOperationException("ID is not correct!");
		
		AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
		
		
		return vm;
	}
}
	
	public class AuthorDetailViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
	}}