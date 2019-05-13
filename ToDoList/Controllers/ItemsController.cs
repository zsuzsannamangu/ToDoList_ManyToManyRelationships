using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {

    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/items")]
    public ActionResult Create(string description)
    {
      //Here we simply create and save a new Item with the form-provided description value. We then retrieve allItems so we may pass them into the index view this route will load upon completion.
      Item newItem = new Item(description);
      newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("Index", allItems);
    }

    [HttpGet("/items/{id}")]
      public ActionResult Show(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Item selectedItem = Item.Find(id);
        List<Category> itemCategories = selectedItem.GetCategories();
        List<Category> allCategories = Category.GetAll();
        //selectedItem has the selected Item we're viewing details for as its value.
        model.Add("selectedItem", selectedItem);
        //itemCategories has a list of all of the selected Item's associated Categorys as its value.
        model.Add("itemCategories", itemCategories);
        //allCategories has all Categorys that have been added regardless of their associated Item(s) as its value.
        model.Add("allCategories", allCategories);
        return View(model);
      }

//Now let's create the route for adding a Category to an Item
      [HttpPost("/items/{itemId}/categories/new")]
      public ActionResult AddCategory(int itemId, int categoryId)
      {
        Item item = Item.Find(itemId);
        Category category = Category.Find(categoryId);
        item.AddCategory(category);
        return RedirectToAction("Show",  new { id = itemId });
      }


    // [HttpGet("/categories/{categoryId}/items/new")]
    // public ActionResult New(int categoryId)
    // {
    //  Category category = Category.Find(categoryId);
    //  return View(category);
    // }
    //

    //
    // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Item.ClearAll();
    //   return View();
    // }
    //
    // [HttpGet("/categories/{categoryId}/items/{itemId}/edit")]
    // public ActionResult Edit(int categoryId, int itemId)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category category = Category.Find(categoryId);
    //   model.Add("category", category);
    //   Item item = Item.Find(itemId);
    //   model.Add("item", item);
    //   return View(model);
    // }
    //
    // [HttpPost("/categories/{categoryId}/items/{itemId}")]
    // public ActionResult Update(int categoryId, int itemId, string newDescription)
    // {
    //   Item item = Item.Find(itemId);
    //   item.Edit(newDescription);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category category = Category.Find(categoryId);
    //   model.Add("category", category);
    //   model.Add("item", item);
    //   return View("Show", model);
    // }

  }
}
