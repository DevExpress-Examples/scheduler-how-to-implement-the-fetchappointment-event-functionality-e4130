@Html.DevExpress().Scheduler(FetchAppointmentsTest.SchedulerSettingsHelper.CommonSchedulerSettings).Bind(
    Function(args)
            args.ForceReloadAppointments = True
            Dim db = New FetchAppointmentsTest.SchedulingDataClassesDataContext()
            Return From schedule In db.DBAppointments Where (schedule.StartDate > args.Interval.Start AndAlso schedule.EndDate < args.Interval.End) Select schedule
    End Function, Model.Resources).GetHtml()