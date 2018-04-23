using System.Collections;
using System.Linq;
using DevExpressMvcSchedulerFetchAppointments;
using DevExpress.Web.Mvc;
using DevExpress.XtraScheduler;
using System;
using DevExpress.Web.ASPxScheduler;
using DevExpressMvcSchedulerFetchAppointments.Models;

#region #schedulerdataobject
public class SchedulerDataObject
{
    public IEnumerable Appointments { get; set; }
    public IEnumerable Resources { get; set; }
    public DevExpress.Web.Mvc.FetchAppointmentsMethod FetchAppointments { get; set; }
}
#endregion #schedulerdataobject

public class SchedulerDataHelper
    {
        public static IEnumerable GetResources()
        {
            SchedulingDataClassesDataContext db = new SchedulingDataClassesDataContext();
            return from res in db.DBResources select res;
        }
        public static IEnumerable GetAppointments()
        {
            SchedulingDataClassesDataContext db = new SchedulingDataClassesDataContext();
            return from apt in db.DBAppointments select apt;
        }

        #region #fetchappointmentshelpermethod
        public static object FetchAppointmentsHelperMethod(FetchAppointmentsEventArgs args)
        {
            args.ForceReloadAppointments = true;
            SchedulingDataClassesDataContext db = new SchedulingDataClassesDataContext();
            return db.DBAppointments.Where(e => e.StartDate > args.Interval.Start && e.EndDate < args.Interval.End);
        }
        #endregion #fetchappointmentshelpermethod

        public static SchedulerDataObject DataObject
        {
            get
            {
                SchedulerDataObject sdo = new SchedulerDataObject();
                sdo.Appointments = GetAppointments();
                sdo.Resources = GetResources();
                sdo.FetchAppointments = FetchAppointmentsHelperMethod;
                return sdo;
            }
        }

        static MVCxAppointmentStorage defaultAppointmentStorage;
        public static MVCxAppointmentStorage DefaultAppointmentStorage
        {
            get
            {
                if (defaultAppointmentStorage == null)
                    defaultAppointmentStorage = CreateDefaultAppointmentStorage();
                return defaultAppointmentStorage;
            }
        }

        static MVCxAppointmentStorage CreateDefaultAppointmentStorage()
        {
            MVCxAppointmentStorage appointmentStorage = new MVCxAppointmentStorage();
            appointmentStorage.Mappings.AppointmentId = "UniqueID";
            appointmentStorage.Mappings.Start = "StartDate";
            appointmentStorage.Mappings.End = "EndDate";
            appointmentStorage.Mappings.Subject = "Subject";
            appointmentStorage.Mappings.Description = "Description";
            appointmentStorage.Mappings.Location = "Location";
            appointmentStorage.Mappings.AllDay = "AllDay";
            appointmentStorage.Mappings.Type = "Type";
            appointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            appointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
            appointmentStorage.Mappings.Label = "Label";
            appointmentStorage.Mappings.Status = "Status";
            appointmentStorage.Mappings.ResourceId = "ResourceID";
            return appointmentStorage;
        }

        static MVCxResourceStorage defaultResourceStorage;
        public static MVCxResourceStorage DefaultResourceStorage
        {
            get
            {
                if (defaultResourceStorage == null)
                    defaultResourceStorage = CreateDefaultResourceStorage();
                return defaultResourceStorage;
            }
        }
        static MVCxResourceStorage CreateDefaultResourceStorage()
        {
            MVCxResourceStorage resourceStorage = new MVCxResourceStorage();
            resourceStorage.Mappings.ResourceId = "ResourceID";
            resourceStorage.Mappings.Caption = "ResourceName";
            return resourceStorage;
        }

        public static void InsertAppointment(DBAppointment appt)
        {
            if (appt == null)
                return;
            SchedulingDataClassesDataContext db = new SchedulingDataClassesDataContext();
            appt.UniqueID = appt.GetHashCode();
            db.DBAppointments.InsertOnSubmit(appt);
            db.SubmitChanges();
        }
        public static void UpdateAppointment(DBAppointment appt)
        {
            if (appt == null)
                return;
            SchedulingDataClassesDataContext db = new SchedulingDataClassesDataContext();
            DBAppointment query = (DBAppointment)(from carSchedule in db.DBAppointments where carSchedule.UniqueID == appt.UniqueID select carSchedule).SingleOrDefault();

            query.UniqueID = appt.UniqueID;
            query.StartDate = appt.StartDate;
            query.EndDate = appt.EndDate;
            query.AllDay = appt.AllDay;
            query.Subject = appt.Subject;
            query.Description = appt.Description;
            query.Location = appt.Location;
            query.RecurrenceInfo = appt.RecurrenceInfo;
            query.ReminderInfo = appt.ReminderInfo;
            query.Status = appt.Status;
            query.Type = appt.Type;
            query.Label = appt.Label;
            query.ResourceID = appt.ResourceID;
            db.SubmitChanges();
        }
        public static void RemoveAppointment(DBAppointment appt)
        {
            SchedulingDataClassesDataContext db = new SchedulingDataClassesDataContext();
            DBAppointment query = (DBAppointment)(from carSchedule in db.DBAppointments where carSchedule.UniqueID == appt.UniqueID select carSchedule).SingleOrDefault();
            db.DBAppointments.DeleteOnSubmit(query);
            db.SubmitChanges();
        }
    }