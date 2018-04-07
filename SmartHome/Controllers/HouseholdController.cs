﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHome.DAL.Mappers;
using SmartHome.Models;

namespace SmartHome.Controllers
{
    public class HouseholdController : HomeController
    {
        // POST: Household/Edit/5
        
        public ActionResult Edit(int id)
        {
            _session = Session.getInstance;
            Household householduser = (Household)_session.GetUser();
            return View(householduser);
        }

        // POST: Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string street, int postalCode, string unitNo, string surname, string contactNo, string email)
        {
            _session = Session.getInstance;
            Household householduser = (Household)_session.GetUser();
            householduser.Street = street;
            householduser.PostalCode = postalCode;
            householduser.UnitNo = unitNo;
            householduser.Surname = surname;
            householduser.ContactNo = contactNo;
            householduser.Email = email;
            
            new HouseholdMapper().Update(householduser).Save().Commit();

            return View(nameof(Profile),householduser);
        }

        public ActionResult ViewNeighbours([FromServices] IAppLogCreator appLogCreator)
        {
            _session = Session.getInstance;
            
           if (_session.IsLogin())
            {
                model = (List<Household>) new HouseholdMapper().SelectAll();
                appLogCreator.AddLog(this, "PAGE*/-View-Neighbours", DateTime.Now);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

     
     

        public ActionResult Logout()
        {
            _session = Session.getInstance;
            Household householdUser = (Household) _session.GetUser();
            householdUser.IsLogin = false;
            new HouseholdMapper().Update(householdUser).Save().Commit();
            _session.endSession();
            

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

    }

}