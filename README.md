The following are instructions on running a local instance of the website on the computer labs or ones personal machine. Guidance for installing pre-requisities on ones personal machine is not provided.

Initial set up:
 - Download the zip file of the code from Github, ensure you are on the main branch.
 - Unzip the file
 - In the unzipped file navigate to "Managing-a-farms-workforce--Main" folder then to "ManageFarm" folder then open the "ManageFarm.sln" file with visual studio
 - In the "this project was downloaded from the web" popup click on the "Trust and Continue" button if it appears.

Before running the project we first need to set up a local server for the database:
 - Open SQL Server Management Studio 20
 - In the "Connect to Server" window type "(local)\SQLEXPRESS" (without quotes) into the Server name field replacing the existing text present.
 - Tick the "Trust server certificate" box
 - Click on the "Connect" button

After setting up the server:
 - Run the project with https (the filled in arrow)
 - Trust the self signed certificate by clicking on "Yes" if a popup appears
 - Install the certificate by clicking on "Yes" if a popup appears
 - Your browser should open possibly with a warning about a security risk.
  - On the lab machines firefox is opened, on firefox click "Advanced..." then "Accept the Risk and Continue"

     For other browsers the process may differ.
 - The password for the login page is "computing" (without quotes), after inputting the password click on Login

You have successfully entered the website.
Utilise the tabs in the ribbon to navigate around the website by clicking on them.

The following is specific advice for certain tabs:

MAP:
 - In the map tab you may be met with a message that "This page can't load Google Maps correctly"
    - This is caused by a development purposes only version of google maps being used as billing is not configured, in a production environment it is assumed billing would be set up which would make this error not applicable.
 - The map is still visible and fully functional if you click on the "OK" button albeit watermarked.
    - You can pan with left click/right click and drag.
    - To zoom in hold the left/right mouse button or CRTL and use the scroll wheel whilst your cursor is over the map.

The database is already pre-populated with sample data.

The various database tabs are intuitive however it should be noted that fields/machines or staff currently part of an assignment or a field that a machine is currently on cannot be deleted.

To delete the row first delete all assignment or machines using the row in question or edit them to no longer require the row in question then one may delete the desired row.

If you try to delete a row currently in use a message will appear warning you of this and you will be stopped from doing so.
