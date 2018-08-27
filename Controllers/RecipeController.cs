using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RecipeApi.Models;


namespace RecipeApi.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController
    {
        private readonly RecipeContext _context;

        public RecipeController(RecipeContext context)
        {
            _context = context;

            if( _context.Recipes.Count() == 0)
            {
                _context.Recipes.Add(new Recipe { Name = "Carbonara" });
                _context.Recipes.Add(new Recipe { Name = "Bolognese" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Recipe>> GetAll()
        {
            return _context.Recipes.ToList();
        }

        [HttpGet("{id}", Name = "GetRecipe")]
        public ActionResult<Recipe> GetById(long id)
        {
            var item = _context.Recipes.Find(id);

            if (item == null)
            {
                return new NotFoundResult();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetRecipe", new { id = recipe.Id }, recipe);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Recipe item)
        {
            var recipe = _context.Recipes.Find(id);

            if (recipe == null)
            {
                return new NotFoundResult();
            }
            recipe.Name = recipe.Name;
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var recipe = _context.Recipes.Find(id);

            if (recipe == null)
            {
                return new NotFoundResult();
            }
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
