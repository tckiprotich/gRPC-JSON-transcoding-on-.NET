using Grpc.Core;
using TodoGPRC;
using TodoGPRC.Models;

namespace TodoGPRC.Services
{
    public class ToDoService : ToDoIt.ToDoItBase
    {
        private readonly AppDbContext _context;
        public ToDoService(AppDbContext context)
        {
            _context = context;
        }

        // CREATE TODO : create a new todo
        public override async Task<CreateTodoResponse> CreateTodo(CreateTodoRequest request, ServerCallContext context)
        {
            if(string.IsNullOrEmpty(request.Title))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Title cannot be empty"));
            }

            var todo = new TodoItems
            {
                Title = request.Title,
                Description = request.Description

            };

            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();

            return await Task.FromResult(new CreateTodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description
            });
        }      
        
        
    }
}