# HAIR SALON

#### A web app that manages a list of stylists and clients

#### By Minh Phuong

## Description
* A website in Nancy for a hair salon. The owner is be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.

## Setup/Installation Requirements
* Clone From GitHub
* Open a cmd program
* Navigate to downloaded folder
* Type in cmd window "dnx kestrel"
* Enter this url in desired browser http://localhost:5004

## Specification
* The program will determine that a stylist input text is entered, not number
  * Input: "4546"
  * Output: False
* The program will take the input name of the stylist type and output it into a dropdown selection menu
  * Input: "Stylist John", "Stylist Kate"
  * Output: Select "Stylist John", "Stylist Kate"
* The program brings the user to a client form for each stylist
  * Input: "Stylist John"
  * Output: Add new client in "Stylist John"
* The program brings the user to a client form for each stylist
  * Input: "Stylist John"
  * Output: Add new client in western stylist
* The user can enter request, booking date, and name of client
  * Input: "Mary", "curly", 2016-3-3
  * Output: "Mary", "curly", 2016-3-3
* The user can enter request, booking date, and name of client. This information is linked to stylist class through stylist_id
  * Input: "Mary", "curly", 2016-3-3
  * Output: "Mary", "curly", 2016-3-3, 1
* The user can enter request, booking date, and name of many clients, each in linked individually through stylist_id
  * Input: "Mary", "curly", 2016-3-3, "Stylist John" ; "Kate","straight hair", 2-30-2016,"Kimberly"
  * Output: "Mary", "curly", 2016-3-3, 1, "Kate","burrito", 2-30-2016,2
* The user can enter request, booking date, and name of many clients, each in linked individually through stylist_id and could be displayed in a list
  * Input: "Mary", "curly", 2016-3-3, "Stylist John" ; "Kate","straight hair", 2-30-2016,"Kimberly"
  * Output: "Mary","Kate"
* When the user click on each individual client on the list, the program will return the information stored in the database for that client
  * Input: Select "Mary"
  * Output: "Mary", "curly", 2016-3-3, "Stylist John"
* When the user click on "Delete All" in the list of all clients, the information of clients in the database will be cleared  
  * Input: Select "Delete All"
  * Output: "Your client list is empty "
* When the user click on "Delete This client" in the individual client page, the information of that particular client in the database will be cleared  
  * Input: Select "Delete This client"
  * Output: client List
* When the user click on each individual stylist on the list, the program will return the client list for that stylist
  * Input: Select "Stylist John"
  * Output: "Mary", "Kourtney"
* When the user clicks on "Delete All" in the list of all stylists, the information of stylists in the database will be cleared  
  * Input: Select "Delete All"
  * Output: "Your stylist list is empty "
* When the user click on "Delete This stylist" in the individual stylist page, the information of that particular stylist in the database will be cleared  
  * Input: Select "Delete This stylist"
  * Output: stylist List
* "Edit This stylist" button will allow user to change name of the individual stylist  
  * Input: Select "Edit This stylist", "Stylist John" -> "Stylist Lee"
  * Output: "Lee"
* "Edit This client" button will allow user to change name of the individual client  
  * Input: Select "Edit This client", "Kate" -> "Mary"
  * Output: "Mary"
* "Edit This client" button will allow user to change name, request, booking date, and stylistId of the individual client  
  * Input: Select "Edit This client", "Kim", 2016-3-3, "Stylist John" -> "Mary", "curly"
  * Output: "Mary", "curly", 2016-3-3, "Stylist John"
* Search for a stylist type by exact name to view all the stylist's clients
  * Input: Search "Stylist John"
  * Output: "Mary", "Wendys", "Jumper"
* Search for a stylist type by exact name, disregarding capitalization, to view all the stylist's clients
  * Input: Search "Stylist John"
  * Output: "Mary", "Wendy", Jumper"
* User can add a written review for an individual client
  * Input: "Kim", "very good....would come again"
  * Output: Submit
* Individual client pages display written review
  * Input: "Kim"
  * Output: "very good....would come again",

## Known Bugs

No known bugs

## Support and contact details

Contact me at mphuong@kent.edu.

## Technologies Used

HTML, CSS, C#, Nancy, Razor.

### License

Copyright (c) 2017 *Minh Phuong and Kaz Matthews*
