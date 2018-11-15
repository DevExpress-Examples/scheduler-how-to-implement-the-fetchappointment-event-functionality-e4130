<!-- default file list -->
*Files to look at*:

* [SchedulerDataHelper.cs](./CS/DevExpressMvcSchedulerFetchAppointments/Models/SchedulerDataHelper.cs)
* [SchedulerSettingsHelper.cs](./CS/DevExpressMvcSchedulerFetchAppointments/Models/SchedulerSettingsHelper.cs)
* [SchedulingDataClasses.cs](./CS/DevExpressMvcSchedulerFetchAppointments/Models/SchedulingDataClasses.cs)
* [SchedulerPartial.cshtml](./CS/DevExpressMvcSchedulerFetchAppointments/Views/Home/SchedulerPartial.cshtml)
<!-- default file list end -->
# Scheduler - How to implement the FetchAppointment event functionality


<p>This example illustrates how to utilize the FetchAppointments delegate method to dynamically limit the number of appointments loaded into the Scheduler storage. This can be useful when working with a large amount of data, and only a small part of it needs to be loaded at one time. </p><p>The visible time interval is passed via the <strong>DevExpress.XtraScheduler.</strong><strong>FetchAppointmentsEventArgs</strong><strong>.Interval</strong> property. You can use it to calculate a new time interval to query a data source for appointments. Fetched appointments are returned to the caller to populate the Scheduler.  <br />
There is nothing wrong in calling the FetchAppointment delegate multiple times. This happens because of PostData loading and interval changes. Generally, it is dependent on various factors, such as the current view type, the visibility of the <a href="http://documentation.devexpress.com/#AspNet/CustomDocument11673"><u>Navigation buttons</u></a> and the presence of the <a href="http://documentation.devexpress.com/#AspNet/CustomDocument11676"><u>DateNavigator control</u></a> bound to the Scheduler.<br />
The FetchAppointment technique was primarily introduced in XtraScheduler and ASPxScheduler controls. The Scheduler MVC extension implements the <strong>FetchAppointment delegate</strong> because its ASP counterpart, the ASPxScheduler, provides the FetchAppointments event. </p><p>For more information, review the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_FetchAppointmentstopic"><u>ASPxScheduler.FetchAppointments</u></a> event reference.</p>

<br/>


