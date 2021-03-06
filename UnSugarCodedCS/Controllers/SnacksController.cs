using UnSugarCodedCS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace UnSugarCodedCS.Controllers
{
public class SnacksController : Controller
{
	[HttpGet("/logins/{id}/snack")]
	public ActionResult Index(int id)
	{
	  Login login = Login.Find(id);
	  List<Snack> allSnacks = Snack.GetAllSnack();
	  ViewBag.Snack = allSnacks;
	  return View("Index", login);
	}

[HttpPost("/logins/{loginId}/snack")]
public ActionResult Create(string foodSnack, string sugarLevelSnack, DateTime stampTimeSnack, string carbSnack, int loginId)
{
	Login foundLogin = Login.Find(loginId);
	Snack newSnack = new Snack(foodSnack, stampTimeSnack, float.Parse(sugarLevelSnack), float.Parse(carbSnack), loginId);
	newSnack.Save();
	List<Snack> newList = Snack.GetAllSnack();

	ViewBag.Snack = newList;
	return View("Index", foundLogin);
}


[HttpPost("/logins/{id}/snack/{snackId}/delete")]
public ActionResult DeleteSnack(int id, int snackId)
{
  Snack snack = Snack.Find(snackId);
  snack.Delete();
  Login login = Login.Find(id);
  List<Snack> allSnacks = Snack.GetAllSnack();
  ViewBag.Snack = allSnacks;
  return View("Index", login);
}

[HttpGet("/logins/{loginId}/snackChart")]
public ActionResult AllLogBooksForUser(int loginId)
{
  Login foundLogin = Login.Find(loginId);
	List<Snack> newList = foundLogin.GetSnacks();
  StringBuilder sb = new StringBuilder("date,value\n");
	foreach (Snack snack in newList)
	{
			sb.Append(snack.GetSnackStampTime().ToString("MM/dd/yyyy")+","+snack.GetSnackSugar()+"\n");
	}
	return Content(sb.ToString(),"text/csv");
}
}
}
