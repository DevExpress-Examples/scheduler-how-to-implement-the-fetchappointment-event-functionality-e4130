@ModelType FetchAppointmentsTest.SchedulerDataObject
    
@Code
    ViewData("Title") = "Home Page"
End Code

<h2>Home</h2>

@Html.Partial("SchedulerPartial", Model)