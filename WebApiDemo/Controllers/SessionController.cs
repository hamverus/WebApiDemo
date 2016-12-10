using DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class SessionController : ApiController
    {
        EmployeeDBEntities1 entities = new EmployeeDBEntities1();
        
        // GET api/values
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;


            //switch (username.ToLower())
            //{
            //    case "male":
            //        return Request.CreateResponse(HttpStatusCode.OK,
            //            entities.Sessions.Where(e => e.Gender.ToLower() == "male").ToList());
            //    case "female":
            //        return Request.CreateResponse(HttpStatusCode.OK,
            //            entities.Sessions.Where(e => e.Gender.ToLower() == "female").ToList());
            //    default:
            //        return Request.CreateResponse(HttpStatusCode.BadRequest);
            //}
            return Request.CreateResponse(HttpStatusCode.OK,
                  entities.Sessions.ToString());

        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            
                var entity = entities.Sessions.FirstOrDefault(e => e.nosession.Equals(id.ToString()));
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Sessions with Id " + id.ToString() + " not found");
                }
            
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] Session session)
        {
            try
            {
                
                    entities.Sessions.Add(session);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, session);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        session.nosession.ToString());

                    return message;
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                
                    var entity = entities.Sessions.FirstOrDefault(e => e.nosession.Equals(id.ToString()));
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Sessions with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Sessions.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
