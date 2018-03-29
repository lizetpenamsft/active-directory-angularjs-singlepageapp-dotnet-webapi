using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using TodoSPA.DAL;

namespace TodoSPA.Controllers
{

    [Authorize]
    public class TodoListController : ApiController
    {


        private static List<Todo> _inMemList;

        private TodoListController()
        {
            if (_inMemList==null)
            {
                _inMemList = new List<Todo>();
            }
        
        }

        // GET: api/TodoList
        public IEnumerable<Todo> Get()
        {
            //string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            IEnumerable<Todo> currentUserToDos = _inMemList.Where(a => a.Owner == owner);
            return currentUserToDos;
        }

        // GET: api/TodoList/5
        public Todo Get(int id)
        {
            //string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            Todo todo = _inMemList.First(a => a.Owner == owner && a.ID == id);             
            return todo;
        }

        // POST: api/TodoList
        public void Post(Todo todo)
        {
            // string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;  
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            todo.Owner = owner;
            _inMemList.Add(todo);
                    
        }

        public void Put(Todo todo)
        {
            // string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            Todo xtodo = _inMemList.First(a => a.Owner == owner && a.ID == todo.ID);
            if (todo != null)
            {
                xtodo.Description = todo.Description;
                
            }
        }

        // DELETE: api/TodoList/5
        public void Delete(int id)
        {
            //string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            Todo todo = _inMemList.First(a => a.Owner == owner && a.ID == id);
            if (todo != null)
            {
                _inMemList.Remove(todo);
               
            }
        }        
    }
}
