using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BookStore.Controllers{

[Authorize]
[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
	private readonly IBookStoreDbContext _context; 
	private readonly IMapper _mapper;
	public AuthorController(IBookStoreDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	
	[HttpGet]
	public IActionResult GetAuthors()
	{
		GetAuthorQuery query = new GetAuthorQuery(_context, _mapper);
		return Ok(query.Handle());
	}
	
	[HttpGet("{id}")]
	public IActionResult GetAuthorById(int id)
	{
		GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
		
		GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
		validator.ValidateAndThrow(query);


		query.AuthorId = id;
		
		return Ok(query.Handle());
	}
	
	[HttpPost]
	public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newAuthor)
	{
		CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

		command.Model = newAuthor;
		
		CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
		validator.ValidateAndThrow(command);
		
		command.Handle();
		
		return Ok();
	}
	
	[HttpPut("{id}")]
	public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
	{
		UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
		
		command.AuthorId = id;
		command.Model = updatedAuthor;
		
		UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
		validator.ValidateAndThrow(command);
		
		command.Handle();
		
		return Ok();
	}
	
	[HttpDelete("{id}")]
	public IActionResult RemoveAuthor(int id)
	{
		DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
		
		command.AuthorId = id;
		//VALIDATIONS
		command.Handle();
		
		return Ok();
	}
}
}