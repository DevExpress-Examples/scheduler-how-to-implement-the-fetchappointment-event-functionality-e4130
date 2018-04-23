using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DevExpressMvcSchedulerFetchAppointments.Models;

namespace DevExpressMvcSchedulerFetchAppointments.Views {
    public class HomeController: Controller {
        //
        // GET: /Home/

        public ActionResult Index() {
            return View(SchedulerDataHelper.DataObject);
        }

        public ActionResult SchedulerPartial() {
            return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject);
        }

        public ActionResult EditAppointment() {
            UpdateAppointment();
            return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject);
        }
        #region #updateappointment
        static void UpdateAppointment() {

            DBAppointment[] insertedAppt = SchedulerExtension.GetAppointmentsToInsert<DBAppointment>(SchedulerSettingsHelper.CommonSchedulerSettings,
                SchedulerDataHelper.FetchAppointmentsHelperMethod,
                SchedulerDataHelper.GetResources());
            foreach (var appt in insertedAppt) {
                SchedulerDataHelper.InsertAppointment(appt);
            }

            DBAppointment[] updatedAppt = SchedulerExtension.GetAppointmentsToUpdate<DBAppointment>(SchedulerSettingsHelper.CommonSchedulerSettings,
                SchedulerDataHelper.FetchAppointmentsHelperMethod,
                SchedulerDataHelper.GetResources());

            foreach (var appt in updatedAppt) {
                SchedulerDataHelper.UpdateAppointment(appt);
            }

            DBAppointment[] removedAppt = SchedulerExtension.GetAppointmentsToRemove<DBAppointment>(SchedulerSettingsHelper.CommonSchedulerSettings,
                SchedulerDataHelper.FetchAppointmentsHelperMethod, 
                SchedulerDataHelper.GetResources());
            foreach (var appt in removedAppt) {
                SchedulerDataHelper.RemoveAppointment(appt);
            }

        }
        #endregion #updateappointment

    }
}
