using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors{
public class GetAuthorQuery
{
	private readonly IBookStoreDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetAuthorQuery(IBookStoreDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}
	
	public List<AuthorsViewModel> Handle()
	{
		var authorList = _dbContext.Authors.OrderBy(a => a.Id).ToList();
		
		List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(authorList);
		
		return viewModel;
	}
	
}
	public class AuthorsViewModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
	}}