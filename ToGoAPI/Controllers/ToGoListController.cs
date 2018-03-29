using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Cors;

using System.Security.Claims;
using ToGoAPI.DAL;
using ToGoAPI.Models;
using System.Configuration;
using System.Web.Http.Cors;


namespace ToGoAPI.Controllers
{
    [Authorize]
    [EnableCors(origins: "https://localhost:44326", headers: "*", methods: "*")]
    public class ToGoListController : ApiController
    {
        
        private static List<ToGo> _inMemList;

        private ToGoListController()
        {
            if (_inMemList == null)
            {
                _inMemList = new List<ToGo>();
            }
        }
        // GET: api/ToGoList
        public IEnumerable<ToGo> Get()
        {
            // string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            
            IEnumerable<ToGo> currentUserToGos = _inMemList.Where(a => a.Owner == owner);
            return currentUserToGos;
        }

        // GET: api/ToGoList/5
        public ToGo Get(int id)
        {
            //string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            ToGo toGo = _inMemList.First(a => a.Owner == owner && a.ID == id);
            return toGo;
        }

        // POST: api/ToGoList
        public void Post(ToGo ToGo)
        {
            //string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            ToGo.Owner = owner;
            _inMemList.Add(ToGo);
            
        }

        public void Put(ToGo ToGo)
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            ToGo xToGo = _inMemList.First(a => a.Owner == owner && a.ID == ToGo.ID);
            if (ToGo != null)
            {
                xToGo.Description = ToGo.Description;
               
            }
        }

        // DELETE: api/ToGoList/5
        public void Delete(int id)
        {
            //string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            ToGo ToGo = _inMemList.First(a => a.Owner == owner && a.ID == id);
            if (ToGo != null)
            {
                _inMemList.Remove(ToGo);
               
            }
        }
    }
}
