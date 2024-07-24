# HotDeskBookingSystem
## Description
Hot desk booking system is a system which should designed to automate the reservation of desk in
office through an easy online booking system.
## Requirements
Administration:
- Manage locations (add/remove, can't remove if desk exists in location)
- Manage desk in locations (add/remove if no reservation/make unavailable)
- 
Employees
- Determine which desks are available to book or unavailable.
- Filter desks based on location
- Book a desk for the day.
- Allow reserving a desk for multiple days but now more than a week.
- Allow to change desk, but not later than the 24h before reservation.
- Administrators can see who reserves a desk in location, where Employees can see only that specific
desk is unavailable.
Start with API but if you feel you can do also frontend 'go for it'
API must be written in .Net
Create at least one unit test
There are no restrictions in mythology/libraries etc

## Pre-added values to the database 
Employee admin = new Employee
{

    Id = 1,
    Role = "admin",
    Email = "admin@admin.com",
    Password = "Admin*123",
};

Employee normalUser = new Employee
{

    Id = 2,
    Role = "user",
    Email = "test@test.com",
    Password = "Test*123",
};

Location unusedDesks = new Location 
{ 

    Id = 1, Name = "Unused desks" 
    
};

## Available operations in the UI
Admin:
- add new desk
- add new location
- delete desk
- delete location
- show desks(with reservation information)

User:
- make reservation
- change desk in reservation
- show desks(only with own reservation information)
