using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using UnSugarCodedCS.Models;

namespace UnSugarCodedCS.Controllers
{
  public class LoginsController : Controller
  {
    [HttpGet("/logins")]
    public ActionResult Index()
    {
      List<Login> allLogins = Login.GetAll();
      return View(allLogins);
    }

    [HttpGet("/logins/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/logins")]
    public ActionResult Create(string loginName, string loginEmail, int loginHeight, int loginWeight)
    {
      Login newLogin = new Login(loginName, loginEmail, loginHeight, loginWeight);
      newLogin.Save();
      List<Login> allLogins = Login.GetAll();
      return View("Index", allLogins);
    }

    [HttpGet("/logins/{loginId}/edit")]
    public ActionResult Edit(int loginId)
    {
      Login login = Login.Find(loginId);
      return View(login);
    }

    [HttpPost("/logins/{loginId}/update")]
    public ActionResult Update(int loginId, string newName, string newEmail, int newHeight, int newWeight)
    {
      Login login = Login.Find(loginId);
      login.Edit(newName, newEmail, newHeight, newWeight);
      List<Login> allLogins = Login.GetAll();
      return View("Index", allLogins);
    }

    [HttpPost("/logins/{loginId}/delete")]
    public ActionResult Delete(int loginId)
    {
      Login login = Login.Find(loginId);
      login.Delete();
      return RedirectToAction("Index");
    }
  }
}
