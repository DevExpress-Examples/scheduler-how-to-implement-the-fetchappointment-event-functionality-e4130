Imports System.Collections
Imports System.Linq
Imports DevExpressMvcSchedulerFetchAppointments
Imports DevExpress.Web.Mvc
Imports DevExpress.XtraScheduler
Imports System
Imports DevExpress.Web.ASPxScheduler
Imports DevExpressMvcSchedulerFetchAppointments.Models

#Region "#schedulerdataobject"
Public Class SchedulerDataObject
    Public Property Appointments() As IEnumerable
    Public Property Resources() As IEnumerable
    Public Property FetchAppointments() As DevExpress.Web.Mvc.FetchAppointmentsMethod
End Class
#End Region ' #schedulerdataobject

Public Class SchedulerDataHelper
        Public Shared Function GetResources() As IEnumerable
            Dim db As New SchedulingDataClassesDataContext()
            Return From res In db.DBResources _
                   Select res
        End Function
        Public Shared Function GetAppointments() As IEnumerable
            Dim db As New SchedulingDataClassesDataContext()
            Return From apt In db.DBAppointments _
                   Select apt
        End Function

        #Region "#fetchappointmentshelpermethod"
        Public Shared Function FetchAppointmentsHelperMethod(ByVal args As FetchAppointmentsEventArgs) As Object
            args.ForceReloadAppointments = True
            Dim db As New SchedulingDataClassesDataContext()
		Return db.DBAppointments.Where(Function(e) (Convert.ToDateTime(e.StartDate) > args.Interval.Start AndAlso Convert.ToDateTime(e.EndDate) < args.Interval.End))
        End Function
        #End Region ' #fetchappointmentshelpermethod

        Public Shared ReadOnly Property DataObject() As SchedulerDataObject
            Get
                Dim sdo As New SchedulerDataObject()
                sdo.Appointments = GetAppointments()
                sdo.Resources = GetResources()
                sdo.FetchAppointments = AddressOf FetchAppointmentsHelperMethod
                Return sdo
            End Get
        End Property


        Private Shared defaultAppointmentStorage_Renamed As MVCxAppointmentStorage
        Public Shared ReadOnly Property DefaultAppointmentStorage() As MVCxAppointmentStorage
            Get
                If defaultAppointmentStorage_Renamed Is Nothing Then
                    defaultAppointmentStorage_Renamed = CreateDefaultAppointmentStorage()
                End If
                Return defaultAppointmentStorage_Renamed
            End Get
        End Property

        Private Shared Function CreateDefaultAppointmentStorage() As MVCxAppointmentStorage
            Dim appointmentStorage As New MVCxAppointmentStorage()
            appointmentStorage.Mappings.AppointmentId = "UniqueID"
            appointmentStorage.Mappings.Start = "StartDate"
            appointmentStorage.Mappings.End = "EndDate"
            appointmentStorage.Mappings.Subject = "Subject"
            appointmentStorage.Mappings.Description = "Description"
            appointmentStorage.Mappings.Location = "Location"
            appointmentStorage.Mappings.AllDay = "AllDay"
            appointmentStorage.Mappings.Type = "Type"
            appointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            appointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
            appointmentStorage.Mappings.Label = "Label"
            appointmentStorage.Mappings.Status = "Status"
            appointmentStorage.Mappings.ResourceId = "ResourceID"
            Return appointmentStorage
        End Function


        Private Shared defaultResourceStorage_Renamed As MVCxResourceStorage
        Public Shared ReadOnly Property DefaultResourceStorage() As MVCxResourceStorage
            Get
                If defaultResourceStorage_Renamed Is Nothing Then
                    defaultResourceStorage_Renamed = CreateDefaultResourceStorage()
                End If
                Return defaultResourceStorage_Renamed
            End Get
        End Property
        Private Shared Function CreateDefaultResourceStorage() As MVCxResourceStorage
            Dim resourceStorage As New MVCxResourceStorage()
            resourceStorage.Mappings.ResourceId = "ResourceID"
            resourceStorage.Mappings.Caption = "ResourceName"
            Return resourceStorage
        End Function

        Public Shared Sub InsertAppointment(ByVal appt As DBAppointment)
            If appt Is Nothing Then
                Return
            End If
            Dim db As New SchedulingDataClassesDataContext()
            appt.UniqueID = appt.GetHashCode()
            db.DBAppointments.InsertOnSubmit(appt)
            db.SubmitChanges()
        End Sub
        Public Shared Sub UpdateAppointment(ByVal appt As DBAppointment)
            If appt Is Nothing Then
                Return
            End If
            Dim db As New SchedulingDataClassesDataContext()
            Dim query As DBAppointment = CType(( _
                From carSchedule In db.DBAppointments _
                Where carSchedule.UniqueID = appt.UniqueID _
                Select carSchedule).SingleOrDefault(), DBAppointment)

            query.UniqueID = appt.UniqueID
            query.StartDate = appt.StartDate
            query.EndDate = appt.EndDate
            query.AllDay = appt.AllDay
            query.Subject = appt.Subject
            query.Description = appt.Description
            query.Location = appt.Location
            query.RecurrenceInfo = appt.RecurrenceInfo
            query.ReminderInfo = appt.ReminderInfo
            query.Status = appt.Status
            query.Type = appt.Type
            query.Label = appt.Label
            query.ResourceID = appt.ResourceID
            db.SubmitChanges()
        End Sub
        Public Shared Sub RemoveAppointment(ByVal appt As DBAppointment)
            Dim db As New SchedulingDataClassesDataContext()
            Dim query As DBAppointment = CType(( _
                From carSchedule In db.DBAppointments _
                Where carSchedule.UniqueID = appt.UniqueID _
                Select carSchedule).SingleOrDefault(), DBAppointment)
            db.DBAppointments.DeleteOnSubmit(query)
            db.SubmitChanges()
        End Sub
End Class