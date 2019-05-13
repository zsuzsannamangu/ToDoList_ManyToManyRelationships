using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {

    [HttpGet("/categories")]
    public ActionResult Index()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }
    // [HttpGet("/categories")]
    // public ActionResult Index()
    // {
    //   List<Category> allCategories = Category.GetAll();
    //   return View(allCategories);
    // }
    //
    [HttpGet("/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/categories")]
    public ActionResult Create(string categoryName)
    {
      Category newCategory = new Category(categoryName);
      newCategory.Save();
      List<Category> allCategories = Category.GetAll();
      return View("Index", allCategories);
    }
    //
    // [HttpPost("/categories")]
    // public ActionResult Create(string categoryName)
    // {
    //   Category newCategory = new Category(categoryName);
    //   List<Category> allCategories = Category.GetAll();
    //   return View("Index", allCategories);
    // }
    //

    [HttpGet("/categories/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(id);
      List<Item> categoryItems = selectedCategory.GetItems();
      List<Item> allItems = Item.GetAll();
      //category has the selected Category as its value. This is the Category we'll be actively viewing details for when this route and view load.
      model.Add("category", selectedCategory);
      //categoryItems is a list of all Items associated with the Category we're viewing.
      model.Add("categoryItems", categoryItems);
      //allItems has all Items that have been added to the To Do List application, regardless of their associated Category(s). We pass this in because our details page will contain a form where the user can select an existing Item to add to the selected Category.
      model.Add("allItems", allItems);
      return View(model);
    }

    [HttpPost("/categories/{categoryId}/items/new")]
    public ActionResult AddItem(int categoryId, int itemId)
    {
      Category category = Category.Find(categoryId);
      //We find the correct Item object and the correct Category object from the form inputs.
      Item item = Item.Find(itemId);
      category.AddItem(item);
      //We run our existing AddItem() method on our Category object and pass in the Item we want to add. We want to render the Category/Show view, so we return RedirectToAction() and pass in "Show" as our first argument.
      //The second argument to View() is the value we want to pass to Show(). The Show() method has one parameter, an id for the category we'd like to show. So, we need to pass in the id for the Category we just added an Item to.
      return RedirectToAction("Show",  new { id = categoryId });
    }

    // // This one creates new Items within a given Category, not new Categories:
    // [HttpPost("/categories/{categoryId}/items")]
    // public ActionResult Create(int categoryId, string itemDescription)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category foundCategory = Category.Find(categoryId);
    //   Item newItem = new Item(itemDescription);
    //   newItem.Save();
    //   foundCategory.AddItem(newItem);
    //   List<Item> categoryItems = foundCategory.GetItems();
    //   model.Add("items", categoryItems);
    //   model.Add("category", foundCategory);
    //   return View("Show", model);
    // }

  }
}
