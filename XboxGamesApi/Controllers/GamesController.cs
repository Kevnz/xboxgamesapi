using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XboxGamesApi.Controllers
{
  public class GamesController : ApiController
  {
    XboxGamesEntities _ctx;

    public GamesController(XboxGamesEntities ctx)
    {
      _ctx = ctx;
      _ctx.Configuration.ProxyCreationEnabled = false;
      _ctx.Configuration.LazyLoadingEnabled = false;
    }

    const int PAGE_SIZE = 25;

    // GET api/values
    public object Get(int page = 1)
    {
      try
      {
        var totalCount = (from g in _ctx.Games
                          select g).Count();

        var totalPages = Math.Ceiling((double)totalCount / PAGE_SIZE);

        var qry = from g in _ctx.Games.Include("Genre").Include("Rating")
                  orderby g.Name
                  select g;

        var data = qry.Skip((page - 1) * PAGE_SIZE)
                      .Take(PAGE_SIZE)
                      .ToList();

        if (data.Count == 0)
        {
          return new { success = false, totalPages = totalPages };
        }

        var results = data.Select(g =>
                            new
                            {
                              id = g.GameID,
                              name = g.Name,
                              genre = g.Genre == null ? "Unknown" : g.Genre.Name,
                              releaseDate = g.ReleaseDate.HasValue ? g.ReleaseDate.GetValueOrDefault().Date.ToShortDateString()  : "",
                              price = g.Price,
                              imageUrl = g.ImageUrl,
                              description = g.Description,
                              rating = g.Rating == null ? "Unknown" : g.Rating.Name
                            }).ToList();

        return new { success = true, totalPages = totalPages, results = results };
      }
      catch (Exception ex)
      {
        return new { success = false, error = ex.ToString() };
      }
    }

    // GET api/values/5
    //public Game Get(int id)
    //{
    //  return _ctx.Games.Where(g => g.GameID == id).FirstOrDefault();
    //}

    // POST api/values
    public void Post([FromBody]Game value)
    {
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]Game value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }
}